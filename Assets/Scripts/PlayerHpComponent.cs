using UnityEngine;
using System.Collections;
using UnityTool.Libgame;

public class PlayerHpComponent : HpComponent {

    public override void LoseHp(float p_hpLost)
    {
        base.LoseHp(p_hpLost);
        if (GetCurrentHp() <= 0)
        {
            //StateMachine.ChangeState("Round", "BattleEnd");
            //StateMachine.ChangeState("Battle", "Fail");
            Game.Instance().Lose();
        }
    }
}
