using UnityEngine;
using System.Collections;
using UnityTool.Libgame;

public class MonsterHpComponent : HpComponent {

    public override void LoseHp(float p_hpLost)
    {
        base.LoseHp(p_hpLost);
        if (GetCurrentHp() <= 0)
        {
            die();
        }
    }

    void die()
    {
        GetComponent<Cell>().id = (int)CellType.ROAD;
        GetComponent<SpriteRenderer>().sprite = null;
        Player.Instance().AddGold(GetComponent<Monster>().gold);
        if (Game.Instance().winAudioSource && Game.sound)
        {
            Game.Instance().winAudioSource.Play();
        }
        //StateMachine.ChangeState("Round", "BattleEnd");
        //StateMachine.ChangeState("Battle", "Win");
    }
}
