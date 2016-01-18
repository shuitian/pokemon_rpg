using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    void Awake()
    {
        player = this;
    }
    static Player player;
    public static Player Instance()
    {
        return player;
    }

    void OnEnable()
    {
        ResetHp();
    }

    #region 生命值组件
    /// <summary>
    /// 生命值
    /// </summary>
    public float hp;

    /// <summary>
    /// 最大生命值
    /// </summary>
    public float maxHp;

    /// <summary>
    /// 获得生命值
    /// </summary>
    /// <param name="p_hpObtained">生命值获得量</param>
    /// <returns>实际增加的生命值</returns>
    public float AddHp(float p_hpObtained)
    {
        if (p_hpObtained < 0 || hp > maxHp)
        {
            p_hpObtained = 0;
        }
        float hpTemp = hp;
        hp += p_hpObtained;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        return hp - hpTemp;
    }

    /// <summary>
    /// 无条件扣除相应生命值
    /// </summary>
    /// <param name="p_hpLost">生命值扣除量</param>
    public void LoseHp(float p_hpLost)
    {
        if (p_hpLost < 0)
        {
            p_hpLost = 0;
        }
        hp -= p_hpLost;
        die();
    }

    /// <summary>
    /// 得到当前生命值
    /// </summary>
    /// <returns>当前生命值</returns>
    public float GetCurrentHp()
    {
        return hp;
    }

    /// <summary>
    /// 重设生命值为最大生命值
    /// </summary>
    /// <returns>如果最大生命值大于0，设置成功，返回真，否则返回假</returns>
    public bool ResetHp()
    {
        if (maxHp <= 0)
        {
            return false;
        }
        hp = maxHp;
        return true;
    }

    /// <summary>
    /// 生命值清0
    /// </summary>
    public void ClearHp()
    {
        this.hp = 0;
    }

    /// <summary>
    /// 改变最大生命值
    /// </summary>
    /// <param name="p_maxHp">新的最大生命值</param>
    public void ChangeMaxHp(float p_maxHp)
    {
        this.maxHp = p_maxHp;
    }
    #endregion

    #region 资源组件
    /// <summary>
    /// 当前金币
    /// </summary>
    public float money = 0;

    /// <summary>
    /// 获取当前金钱
    /// </summary>
    /// <returns>当前金钱</returns>
    public float GetCurrentMoney()
    {
        return money;
    }

    /// <summary>
    /// 增加金钱
    /// </summary>
    /// <param name="p_money">金钱增加量</param>
    public void AddMoney(float p_money)
    {
        if (p_money < 0)
        {
            p_money = 0;
        }
        this.money += p_money;
    }

    /// <summary>
    /// 无条件扣除相应金钱
    /// </summary>
    /// <param name="p_money">金钱扣除量</param>
    public void LoseMoney(float p_money)
    {
        if (p_money < 0)
        {
            p_money = 0;
        }
        this.money -= p_money;
    }

    /// <summary>
    /// 试着扣除相应金钱
    /// </summary>
    /// <param name="p_money">金钱扣除量</param>
    /// <returns>成功返回真，否则返回假</returns>
    public virtual bool TryToLoseMoney(float p_money)
    {
        if (p_money < 0)
        {
            p_money = 0;
        }
        if (this.money >= p_money)
        {
            this.money -= p_money;
            return true;
        }
        return false;
    }
    #endregion

    public string playerName = "水天";
    public float attack;
    [Range(0, 100)]
    public float defence;
    /// <summary>
    /// 角色死亡
    /// </summary>
    void die()
    {
        Game.Lose();
    }
}
