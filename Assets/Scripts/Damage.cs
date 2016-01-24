using UnityEngine;
using System.Collections;
using System;
using UnityTool.Libgame;

public class Damage {

    public Damage(Character damager, Character damaged,float damage)
    {
        this.damager = damager;
        this.damaged = damaged;
        this.damage = damage;
        isDamaged = false;
    }

    public Character damager;
    public Character damaged;
    public float damage;
    public bool isDamaged;
    static ArrayList damages = new ArrayList();
    static public ArrayList GetDamages()
    {
        return damages;
    }

    static public void ClearDamages()
    {
        damages = new ArrayList();
    }

    static public void AddDamage(Damage damage)
    {
        damages.Add(damage);
    }

    static public void AciveAllDamages()
    {
        foreach (Damage damageStruct in Damage.GetDamages())
        {
            damageStruct.damaged.hpComponent.LoseHp(damageStruct.damage);
            Message.RaiseOneMessage<string>("AddBattleInfo", null, damageStruct.damager.characterName + "对" + damageStruct.damaged.characterName + " 造成" + damageStruct.damage + "点伤害\n");
        }
        ClearDamages();
    }
}
