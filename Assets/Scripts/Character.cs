using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public string characterName = "水天";
    #region 角色组件
    /// <summary>
    /// 生命值
    /// </summary>
    [SerializeField]
    float hp;

    public virtual void SetHp(float hp)
    {
        this.hp = hp;
    }

    /// <summary>
    /// 获得生命值
    /// </summary>
    /// <param name="p_hpObtained">生命值获得量</param>
    public virtual void AddHp(float p_hpObtained)
    {
        if (p_hpObtained < 0)
        {
            p_hpObtained = 0;
        }
        hp += p_hpObtained;
    }

    /// <summary>
    /// 无条件扣除相应生命值
    /// </summary>
    /// <param name="p_hpLost">生命值扣除量</param>
    public virtual void LoseHp(float p_hpLost)
    {
        if (p_hpLost < 0)
        {
            p_hpLost = 0;
        }
        hp -= p_hpLost;
        if (hp <= 0)
        {
            hp = 0;
            die();
        }
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
    /// 生命值清0
    /// </summary>
    public void ClearHp()
    {
        this.hp = 0;
    }

    protected virtual void die()
    {
    }

    [SerializeField]
    float attack;
    [Range(0, 100)]
    [SerializeField]
    float defence;

    public float GetAttack()
    {
        return attack;
    }

    public virtual void SetAttack(float at)
    {
        attack = at;
    }

    public virtual void AddAttack(float at)
    {
        attack += at;
    }

    public virtual void AddDefence(float de)
    {
        defence += de;
        if (defence > 100)
        {
            defence = 100;
        }
        if (defence < 0)
        {
            defence = 0;
        }
    }

    public virtual void SetDefence(float de)
    {
        defence = de;
    }

    public float GetDefence()
    {
        return defence;
    }

    public virtual void Attack(Character target)
    {
        target.LoseHp(attack * (1 - target.defence / 100));
    }
    #endregion
}
