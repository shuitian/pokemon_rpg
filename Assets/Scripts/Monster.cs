using UnityEngine;
using System.Collections;

public class Monster : Character
{ 
    public int id;
    public int gold;
    protected override void die()
    {
        base.die();
        id = (int)CellType.ROAD;
        GetComponent<SpriteRenderer>().sprite = null;
        Player.Instance().AddGold(gold);
    }
}

public class MonsterData
{
    public int id;
    public float hp;
    public float attack;
    public float defence;
    public int gold;
}
