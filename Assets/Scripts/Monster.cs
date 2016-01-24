using UnityEngine;
using System.Collections;
using UnityTool.Libgame;

[RequireComponent(typeof(MonsterHpComponent))]
public class Monster : Character
{ 
    public int id;
    public int gold;
    protected override void die()
    {
        base.die();
        GetComponent<Cell>().id = (int)CellType.ROAD;
        GetComponent<SpriteRenderer>().sprite = null;
        Player.Instance().AddGold(gold);
        if (Game.Instance().winAudioSource && Game.sound)
        {
            Game.Instance().winAudioSource.Play();
        }
    }
}

public class MonsterData
{
    public int id;
    public string name;
    public float hp;
    public float attack;
    public float defence;
    public int gold;
}
