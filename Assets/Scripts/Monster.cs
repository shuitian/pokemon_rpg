using UnityEngine;
using System.Collections;

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
    }

    public override void LoseHp(float p_hpLost)
    {
        base.LoseHp(p_hpLost);
        Game.Instance().battle.AddInfo("受到你的攻击，" + characterName + "失去" + p_hpLost + "点生命\n");
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
