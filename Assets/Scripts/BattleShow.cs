using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityTool.Libgame;

public class BattleShow : MonoBehaviour
{

    public Player player { get { return Player.Instance(); } }
    public Monster monster { get { return Battle.Instance().monster; } }
    public Text playerNameText;
    public Text playerHpText;
    public Text playerAttackText;
    public Text playerDefenceText;
    public Image monsterImage;
    public Text monsterNameText;
    public Text monsterHpText;
    public Text monsterAttackText;
    public Text monsterDefenceText;

    public void UpdatePlayerInformation()
    {
        if (playerHpText)
        {
            playerHpText.text = "" + player.GetCurrentHp();
        }
        if (playerAttackText)
        {
            playerAttackText.text = "" + player.GetAttack();
        }
        if (playerDefenceText)
        {
            playerDefenceText.text = player.GetDefence() + "%";
        }
    }

    public void UpdateMonsterInformation()
    {
        if (monsterHpText)
        {
            monsterHpText.text = "" + monster.GetCurrentHp();
        }
        if (monsterAttackText)
        {
            monsterAttackText.text = "" + monster.GetAttack();
        }
        if (monsterDefenceText)
        {
            monsterDefenceText.text = monster.GetDefence() + "%";
        }
    }

    public void ShowBattleInfo()
    {
        if (player)
        {
            if (playerNameText)
            {
                playerNameText.text = player.characterName;
            }
            UpdatePlayerInformation();
        }
        if (monster)
        {
            if (monsterImage)
            {
                monsterImage.sprite = LoadResources.monsters[monster.id - 1];
            }
            if (monsterNameText)
            {
                monsterNameText.text = monster.characterName;
            }
            UpdateMonsterInformation();
        }
    }

    public Text info;
    string[] strs = new string[7];
    int temp;
    public void AddInfo(string messageName, object sender, string str)
    {
        strs[temp] = str;
        string s = "";
        for (int i = temp + 1; i < strs.Length; i++)
        {
            s += strs[i];
        }
        for (int i = 0; i < temp + 1; i++)
        {
            s += strs[i];
        }
        temp = (temp + 1) % strs.Length;
        if (info)
        {
            info.text = s;
        }
    }

    public Text tip;
    void Awake()
    {
        Message.RegeditMessageHandle<string>("AddBattleInfo", AddInfo);
        StateMachine.RegediStateCallBack("Battle", "Init", StateCallBackType.TypeOnEnter, BattleInitEnter);
        StateMachine.RegediStateCallBack("Battle", "Fail", StateCallBackType.TypeOnEnter, BattleFailEnter);
        StateMachine.RegediStateCallBack("Battle", "Win", StateCallBackType.TypeOnEnter, BattleWinEnter);
        StateMachine.RegediStateCallBack("Round", "Damaged", StateCallBackType.TypeOnExit, RoundDamagedExit);
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        Message.UnregeditMessageHandle<string>("AddBattleInfo", AddInfo);
        StateMachine.UnregediStateCallBack("Battle", "Init", StateCallBackType.TypeOnEnter, BattleInitEnter);
        StateMachine.UnregediStateCallBack("Battle", "Fail", StateCallBackType.TypeOnEnter, BattleFailEnter);
        StateMachine.UnregediStateCallBack("Battle", "Win", StateCallBackType.TypeOnEnter, BattleWinEnter);
        StateMachine.UnregediStateCallBack("Round", "Damaged", StateCallBackType.TypeOnExit, RoundDamagedExit);
    }

    void RoundDamagedExit(string stateName)
    {
        UpdatePlayerInformation();
        UpdateMonsterInformation();
    }

    void BattleFailEnter(string stateName)
    {
        tip.text = "按任意键\n重新开始游戏";
    }

    void BattleWinEnter(string stateName)
    {
        tip.text = "按任意键返回游戏";
        Message.RaiseOneMessage<string>("AddBattleInfo", this, "战斗胜利\n");
    }

    void BattleInitEnter(string stateName)
    {
        strs = new string[strs.Length];
        temp = 0;
        tip.text = "按空格键 开始战斗\n按P键 快速战斗\n按ESC    逃离战斗";
        ShowBattleInfo();
        Message.RaiseOneMessage<string>("AddBattleInfo", this, "是否开始战斗?\n");
        StateMachine.ChangeState("Battle", "Start");
    }

    void Update()
    {
        //print(StateMachine.GetCurrentStateName("Battle"));
        switch (StateMachine.GetCurrentStateName("Battle"))
        {
            case "Escape":
                if (Input.anyKeyDown)
                {
                    Camera.main.transform.position = new Vector3(Game.Instance().gamePosition.x, Game.Instance().gamePosition.y, -10);
                    gameObject.SetActive(false);
                    StateMachine.ChangeState("Battle", "End");
                }
                break;
            case "Fail":
                if (Input.anyKeyDown)
                {
                    Game.Instance().Restart();
                }
                break;
            case "Win":
                if (Input.anyKeyDown)
                {
                    Camera.main.transform.position = new Vector3(Game.Instance().gamePosition.x, Game.Instance().gamePosition.y, -10);
                    gameObject.SetActive(false);
                    StateMachine.ChangeState("Round", "BattleEnd");
                }
                break;
            case "Start":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    tip.text = "按P  跳过战斗过程\n按ESC    逃离战斗";
                    StateMachine.ChangeState("Battle", "Round");
                }
                else if (Input.GetKeyDown(KeyCode.P))
                {
                    Message.RaiseOneMessage<string>("QuickBattle", this, "");
                    StateMachine.ChangeState("Battle", "Round");
                    tip.text = "按ESC    逃离战斗";
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Message.RaiseOneMessage<string>("AddBattleInfo", this, "你逃离了战斗，战斗结束\n");
                    tip.text = "按任意键返回游戏";
                    StateMachine.ChangeState("Battle", "Escape");
                }
                break;
            case "Round":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Message.RaiseOneMessage<string>("AddBattleInfo", this, "你逃离了战斗，战斗结束\n");
                    tip.text = "按任意键返回游戏";
                    StateMachine.ChangeState("Battle", "Escape");
                    StateMachine.ChangeState("Round", "BattleEnd");
                }
                else if (Input.GetKeyDown(KeyCode.P))
                {
                    Message.RaiseOneMessage<string>("QuickBattle", this, "");
                    tip.text = "按ESC    逃离战斗";
                }
                break;
            default:
                break;
        }
    }
}
