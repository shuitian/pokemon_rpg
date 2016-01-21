using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Battle :MonoBehaviour{

    public Player player;
    public Monster monster;
    float lastTime;
    public float battleSpcae;
    public Text playerNameText;
    public Text playerHpText;
    public Text playerAttackText;
    public Text playerDefenceText;
    public Image monsterImage;
    public Text monsterNameText;
    public Text monsterHpText;
    public Text monsterAttackText;
    public Text monsterDefenceText;
    public AudioSource battleSound;

    void UpdatePlayerInformation()
    {
        if (player && playerHpText)
        {
            playerHpText.text = "" + player.GetCurrentHp();
        }
    }

    void UpdateMonsterInformation()
    {
        if (monster && monsterHpText) 
        {
            monsterHpText.text = "" + monster.GetCurrentHp();
        }
    }

    public Text info;

    public void ShowInfo()
    {
        if (player)
        {
            if (playerNameText)
            {
                playerNameText.text = player.characterName;
            }
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
    }

    public void battle(Player player, Monster monster)
    {
        isStart = false;
        pass = false;
        strs = new string[strs.Length];
        temp = 0;
        tip.text = "按空格键 开始战斗\n按P键 快速战斗\n按ESC    逃离战斗";
        this.player = player;
        this.monster = monster;
        ShowInfo();
        float a = monster.GetCurrentHp() / (1 - monster.GetDefence() / 100) / player.GetAttack();
        if(a != (float)(int)a)
        {
            a = Mathf.Floor(a) + 1;
            
        }
        a = a * monster.GetAttack() * (1 - player.GetDefence() / 100);
        AddInfo("是否开始战斗，预计损耗生命" + a + "\n");
        isPlayerTurn = false;
    }

    bool isStart;
    bool isPlayerTurn;
    bool click = false;
    bool fail = false;
    bool pass;
    public Text tip;
    void Update()
    {
        if (click)
        {
            if (fail)
            {
                tip.text = "按任意键\n重新开始游戏";
            }
            else
            {
                tip.text = "按任意键返回游戏";
            }
            if (Input.anyKeyDown)
            {
                if (fail)
                {
                    Game.Instance().Restart();
                }
                else
                {
                    click = false;
                    gameObject.SetActive(false);
                }
            }
        }else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isStart = true;
                tip.text = "按P  跳过战斗过程\n按ESC    逃离战斗";
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                monster = null;
                AddInfo("你逃离了战斗，战斗结束\n");
                click = true;
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                pass = true;
                isStart = true;
                tip.text = "按P  跳过战斗过程\n按ESC    逃离战斗";
            }
        }
        if ((!pass && Time.time - lastTime < battleSpcae) || !isStart) 
        {
            return;
        }
        lastTime = Time.time;
        if (player && monster)
        {
            if (player.GetCurrentHp() > 0 && monster.GetCurrentHp() > 0)
            {
                if (isPlayerTurn)
                {
                    player.Attack(monster);
                    isPlayerTurn = false;
                }
                else
                {
                    monster.Attack(player);
                    isPlayerTurn = true;
                }
                if (battleSound && Game.sound && !pass)
                {
                    battleSound.Play();
                }
                UpdateMonsterInformation();
                UpdatePlayerInformation();
                if (player.GetCurrentHp() <= 0)
                {
                    AddInfo("战斗失败\n");
                    click = true;
                    fail = true;
                }
                else if (monster.GetCurrentHp() <= 0)
                {
                    AddInfo("战斗胜利，获得金币" + monster.gold + "\n");
                    click = true;
                }
            }
        }
    }

    string[] strs = new string[6];
    int temp;
    public void AddInfo(string str)
    {
        strs[temp] = str;
        string s = "";
        for(int i = temp + 1; i < strs.Length; i++)
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
}
