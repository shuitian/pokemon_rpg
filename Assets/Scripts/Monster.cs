using UnityEngine;
using System.Collections;
using UnityTool.Libgame;

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

    public override void LoseHp(float p_hpLost)
    {
        base.LoseHp(p_hpLost);
        Message.RaiseOneMessage<string>("AddBattleInfo", this, "受到你的攻击，" + characterName + "失去" + p_hpLost + "点生命\n");
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
