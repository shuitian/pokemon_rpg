using UnityEngine;
using System.Collections;

public class Player : Character {

    void Awake()
    {
        player = this;
    }
    static Player player;
    public static Player Instance()
    {
        return player;
    }

    #region 资源组件
    /// <summary>
    /// 当前金币
    /// </summar>
    public float gold = 0;

    /// <summary>
    /// 获取当前金钱
    /// </summary>
    /// <returns>当前金钱</returns>
    public float GetCurrentGold()
    {
        return gold;
    }

    /// <summary>
    /// 增加金钱
    /// </summary>
    /// <param name="p_gold">金钱增加量</param>
    public void AddGold(float p_gold)
    {
        if (p_gold < 0)
        {
            p_gold = 0;
        }
        this.gold += p_gold;
    }

    /// <summary>
    /// 无条件扣除相应金钱
    /// </summary>
    /// <param name="p_gold">金钱扣除量</param>
    public void LoseGold(float p_gold)
    {
        if (p_gold < 0)
        {
            p_gold = 0;
        }
        this.gold -= p_gold;
    }

    /// <summary>
    /// 试着扣除相应金钱
    /// </summary>
    /// <param name="p_gold">金钱扣除量</param>
    /// <returns>成功返回真，否则返回假</returns>
    public virtual bool TryToLoseGold(float p_gold)
    {
        if (p_gold < 0)
        {
            p_gold = 0;
        }
        if (this.gold >= p_gold)
        {
            this.gold -= p_gold;
            return true;
        }
        return false;
    }
    #endregion

    public string playerName = "水天";

    /// <summary>
    /// 角色死亡
    /// </summary>
    protected override void die()
    {
        base.die();
        Game.Lose();
    }
}
