using UnityEngine;
using System.Collections;
using UnityTool.Libgame;

[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerHpComponent))]
public class Player : Character {

    new protected void Awake()
    {
        base.Awake();
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
    [SerializeField]
    float gold = 0;

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
        Message.RaiseOneMessage<Player>("PlayerShow", this, this);
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
        Message.RaiseOneMessage<Player>("PlayerShow", this, this);
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
            Message.RaiseOneMessage<Player>("PlayerShow", this, this);
            return true;
        }
        return false;
    }
    #endregion

    public void GetItem(ItemData item)
    {
        Player.Instance().hpComponent.AddHp(item.addHp);
        Player.Instance().AddAttack(item.addAttack);
        Player.Instance().AddDefence(item.addDefence);
        Player.Instance().AddGold(item.addGold);
        Message.RaiseOneMessage<Player>("PlayerShow", this, this);
        string str = "你获得了" + item.name;
        if (item.addHp > 0)
        {
            str += ",生命值增加了" + item.addHp;
        }
        if (item.addAttack > 0)
        {
            str += ",攻击力增加了" + item.addAttack;
        }
        if (item.addDefence > 0)
        {
            str += ",防御力增加了" + item.addDefence;
        }
        if (item.addGold > 0)
        {
            str += ",金币增加了" + item.addGold;
        }
        str += "。";
        Game.Instance().ShowMessage(str, Game.Instance().gamePosition);
    }

    public override void AddAttack(float at)
    {
        base.AddAttack(at);
        Message.RaiseOneMessage<Player>("PlayerShow", this, this);
    }

    public override void AddDefence(float de)
    {
        base.AddDefence(de);
        Message.RaiseOneMessage<Player>("PlayerShow", this, this);
    }

    public override void SetAttack(float at)
    {
        base.SetAttack(at);
        Message.RaiseOneMessage<Player>("PlayerShow", this, this);
    }

    public override void SetDefence(float de)
    {
        base.SetDefence(de);
        Message.RaiseOneMessage<Player>("PlayerShow", this, this);
    }
}
