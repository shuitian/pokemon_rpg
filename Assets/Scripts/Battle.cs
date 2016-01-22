﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityTool.Libgame;

[RequireComponent(typeof(BattleShow))]
public class Battle :MonoBehaviour{

    static Battle _battle;
    public Player player;
    public Monster monster;
    public BattleShow battleShow;
    public static Battle Instance()
    {
        return _battle;
    }
    void Awake()
    {
        _battle = this;
        StateMachine.RegediStateCallBack("Battle", "Round", StateCallBackType.TypeOnEnter, BattleRoundEnter);
        StateMachine.RegediStateCallBack("Round", "RoundStart", StateCallBackType.TypeOnEnter, BattleStartEnter);
        StateMachine.RegediStateCallBack("Round", "BeforeDamage", StateCallBackType.TypeOnEnter, RoundBeforeDamageEnter);
        Message.RegeditMessageHandle<string>("QuickBattle", SetPassFlag);
    }

    void OnDestroy()
    {
        StateMachine.UnregediStateCallBack("Battle", "Round", StateCallBackType.TypeOnEnter, BattleRoundEnter);
        StateMachine.UnregediStateCallBack("Round", "RoundStart", StateCallBackType.TypeOnEnter, BattleStartEnter);
        StateMachine.UnregediStateCallBack("Round", "BeforeDamage", StateCallBackType.TypeOnEnter, RoundBeforeDamageEnter);
        Message.UnregeditMessageHandle<string>("QuickBattle", SetPassFlag);
    }

    void BattleRoundEnter(string stateName)
    {
        isPlayerTurn = false;
        StateMachine.ChangeState("Round", "RoundStart");
    }

    void BattleStartEnter(string statename)
    {
        lastRoundTime = UnityEngine.Time.time;
    }

    void RoundBeforeDamageEnter(string stateName)
    {
        //print(stateName);
    }

    bool pass;
    public AudioSource battleSound;
    void SetPassFlag(string messageName, object sender, string e)
    {
        pass = true;
    }

    public void battle(Player player, Monster monster)
    {
        this.player = player;
        this.monster = monster;
        pass = false;
        //float a = monster.GetCurrentHp() / (1 - monster.GetDefence() / 100) / player.GetAttack();
        //if(a != (float)(int)a)
        //{
        //    a = Mathf.Floor(a) + 1;

        //}
        //a = a * monster.GetAttack() * (1 - player.GetDefence() / 100);
        gameObject.SetActive(true);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        StateMachine.ChangeState("Battle", "Init");
    }

    bool isPlayerTurn;
    public float roundSpace;
    float lastRoundTime;
    void Update()
    {
        switch (StateMachine.GetCurrentStateName("Round"))
        {
            case "RoundStart":
                if (isPlayerTurn)
                {
                    isPlayerTurn = false;
                }
                else
                {
                    isPlayerTurn = true;
                }
                StateMachine.ChangeState("Round", "BeforeAttack");
                break;
            case "BeforeAttack":
                StateMachine.ChangeState("Round", "Attack");
                break;
            case "Attack":
                bool ret;
                if (isPlayerTurn)
                {
                    ret = player.Attack(monster);
                }
                else
                {
                    ret = monster.Attack(player);
                }
                if (battleSound && Game.sound && !pass)
                {
                    battleSound.Play();
                }
                if (ret)
                {
                    StateMachine.ChangeState("Round", "BeforeDamage");
                }
                else
                {
                    StateMachine.ChangeState("Round", "AfterAttack");
                }
                break;
            case "BeforeDamage":
                StateMachine.ChangeState("Round", "Damaged");
                break;
            case "Damaged":
                Damage.AciveAllDamages();
                StateMachine.ChangeState("Round", "AfterDamage");
                break;
            case "AfterDamage":
                StateMachine.ChangeState("Round", "AfterAttack");
                break;
            case "AfterAttack":
                if (monster.GetCurrentHp() <= 0)
                {
                    StateMachine.ChangeState("Round", "BattleEnd");
                    StateMachine.ChangeState("Battle", "Win");
                }
                else if (player.GetCurrentHp() <= 0)
                {
                    StateMachine.ChangeState("Round", "BattleEnd");
                    StateMachine.ChangeState("Battle", "Fail");
                }
                else
                {
                    StateMachine.ChangeState("Round", "RoundEnd");
                }
                break;
            case "RoundEnd":
                if (UnityEngine.Time.time - lastRoundTime > roundSpace || pass)
                {
                    StateMachine.ChangeState("Round", "RoundStart");
                }
                else
                {
                    StateMachine.ChangeState("Round", "RoundWait");
                }
                //StateMachine.ChangeState("Round", "BattleEnd");
                break;
            case "RoundWait":
                if (UnityEngine.Time.time - lastRoundTime > roundSpace)
                {
                    StateMachine.ChangeState("Round", "RoundStart");
                }
                break;
            default:
                break;
        }
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        isStart = true;
        //        tip.text = "按P  跳过战斗过程\n按ESC    逃离战斗";
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        monster = null;
        //        AddInfo("你逃离了战斗，战斗结束\n");
        //        click = true;
        //    }
        //    else if (Input.GetKeyDown(KeyCode.P))
        //    {
        //        pass = true;
        //        isStart = true;
        //        tip.text = "按P  跳过战斗过程\n按ESC    逃离战斗";
        //    }
        //}
        //if ((!pass && Time.time - lastTime < battleSpcae) || !isStart) 
        //{
        //    return;
        //}
        //lastTime = Time.time;
        //if (player && monster)
        //{
        //    if (player.GetCurrentHp() > 0 && monster.GetCurrentHp() > 0)
        //    {
        //        if (isPlayerTurn)
        //        {
        //            player.Attack(monster);
        //            isPlayerTurn = false;
        //        }
        //        else
        //        {
        //            monster.Attack(player);
        //            isPlayerTurn = true;
        //        }
        //        if (battleSound && Game.sound && !pass)
        //        {
        //            battleSound.Play();
        //        }
        //        UpdateMonsterInformation();
        //        UpdatePlayerInformation();
        //        if (player.GetCurrentHp() <= 0)
        //        {
        //            AddInfo("战斗失败\n");
        //            click = true;
        //            fail = true;
        //        }
        //        else if (monster.GetCurrentHp() <= 0)
        //        {
        //            AddInfo("战斗胜利，获得金币" + monster.gold + "\n");
        //            click = true;
        //        }
        //    }
        //}
    }
}
