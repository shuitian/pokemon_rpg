using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    void UpdatePlayerInformation()
    {
        if (player && playerHpText)
        {
            playerHpText.text = "" + player.hp;
        }
    }

    void UpdateMonsterInformation()
    {
        if (monster && monsterHpText) 
        {
            monsterHpText.text = "" + monster.hp;
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
                playerHpText.text = "" + player.hp;
            }
            if (playerAttackText)
            {
                playerAttackText.text = "" + player.attack;
            }
            if (monsterNameText)
            {
                monsterNameText.text = player.defence + "%";
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
                monsterHpText.text = "" + monster.hp;
            }
            if (monsterAttackText)
            {
                monsterAttackText.text = "" + monster.attack;
            }
            if (monsterDefenceText)
            {
                monsterDefenceText.text = monster.defence + "%";
            }
        }
    }

    public void battle(Player player, Monster monster)
    {
        this.player = player;
        this.monster = monster;
        ShowInfo();
        info.text = "战斗开始\n";
        isPlayerTurn = false;
    }

    bool isPlayerTurn;
    bool click = false;
    void Update()
    {
        if (click)
        {
            if (Input.anyKeyDown)
            {
                click = false;
                gameObject.SetActive(false);
            }
        }
        if(Time.time - lastTime<battleSpcae)
        {
            return;
        }
        lastTime = Time.time;
        if (player && monster)
        {
            if (player.hp > 0 && monster.hp > 0)
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
                UpdateMonsterInformation();
                UpdatePlayerInformation();
                if (player.hp <= 0 && info)
                {
                    info.text += "战斗失败\n";
                    click = false;
                }
                else if (monster.hp <= 0 && info)
                {
                    info.text += "战斗胜利，获得金币" + monster.gold + "\n";
                    click = true;
                }
            }
        }
    }
}
