using UnityEngine;
using System.Collections;

/// <summary>
/// 生命值组件
/// </summary>
public class HpComponent : MonoBehaviour
{

    void OnEnable()
    {
#if MAX_HP
        CalculateCurrentMaxHp();
        ResetHp();
#endif
#if HP_RECOVER
        CalculateCurrentHpRecover();
        StartCoroutine(RecoverHpPerSecond());
#endif
    }

    void OnDestroy()
    {
#if HP_RECOVER
        StopAllCoroutines();
#endif
    }

    /// <summary>
    /// 当前生命值
    /// </summary>
    public float hp;

    public void SetHp(float hp)
    {
        this.hp = hp;
    }
    /// <summary>
    /// 获得生命值
    /// </summary>
    /// <param name="p_hpObtained">生命值获得量</param>
    /// <returns>实际增加的生命值</returns>
    public float AddHp(float p_hpObtained)
    {
        if (p_hpObtained < 0)
        {
            p_hpObtained = 0;
        }
        float hpTemp = hp;
        hp += p_hpObtained;
#if MAX_HP
        if (hp > maxHp)
        {
            hp = maxHp;
        }
#endif
        return hp - hpTemp;
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
    }

    /// <summary>
    /// 试着扣除相应生命值
    /// </summary>
    /// <param name="p_hpLost">生命值扣除量</param>
    /// <returns>扣除成功，返回真，否则返回假</returns>
    public bool TryToLoseHp(float p_hpLost)
    {
        if (p_hpLost < 0)
        {
            p_hpLost = 0;
        }
        if (hp > p_hpLost)
        {
            LoseHp(p_hpLost);
            return true;
        }
        return false;
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

    #region MAX_HP
#if MAX_HP
    /// <summary>
    /// 基础最大生命值
    /// </summary>
    [SerializeField]
    protected float baseMaxHp = 0;

    /// <summary>
    /// 基础最大生命值增加值
    /// </summary>
    public float maxHpAddedValue = 0;

    /// <summary>
    /// 最大生命值百分比
    /// </summary>
    public float maxHpRate = 1;
    /// <summary>
    /// 最大生命值
    /// </summary>
    [HideInInspector]
    public float maxHp;

    /// <summary>
    /// 最小最大生命值
    /// </summary>
    public float minMaxHp;

    /// <summary>
    /// 最大最大生命值
    /// </summary>
    public float maxMaxHp;


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
    /// 增加最大生命值
    /// </summary>
    /// <param name="p_maxHpAddedValue">增加的基础最大生命值增加值</param>
    /// <param name="p_maxHpRate">增加的最大生命值百分比</param>
    /// <returns></returns>
    public bool AddMaxHp(float p_maxHpAddedValue, float p_maxHpRate)
    {
        return ChangeMaxHp(this.maxHpAddedValue + p_maxHpAddedValue, this.maxHpRate + p_maxHpRate);
    }

    /// <summary>
    /// 改变最大生命值
    /// </summary>
    /// <param name="p_maxHpAddedValue">新的基础最大生命值增加值</param>
    /// <param name="p_maxHpRate">新的最大生命值百分比</param>
    /// <returns>改变是否成功</returns>
    public bool ChangeMaxHp(float p_maxHpAddedValue, float p_maxHpRate)
    {
        this.maxHpAddedValue = p_maxHpAddedValue;
        this.maxHpRate = p_maxHpRate;
        CalculateCurrentMaxHp();
        return true;
    }

    /// <summary>
    /// 根据基础最大生命值、基础最大生命值增加值和最大生命值增加百分比，计算最大生命值
    /// </summary>
    protected void CalculateCurrentMaxHp()
    {
        maxHp = (baseMaxHp + maxHpAddedValue) * maxHpRate;
        CheckMaxHp();
    }

    /// <summary>
    /// 限定最大生命值在一定范围内
    /// </summary>
    public void CheckMaxHp()
    {
        if (maxHp > maxMaxHp)
        {
            maxHp = maxMaxHp;
        }
        if (maxHp < minMaxHp)
        {
            maxHp = minMaxHp;
        }
    }
#endif
    #endregion
    #region HP_RECOVER
#if HP_RECOVER
    /// <summary>
    /// 基础每秒生命恢复值
    /// </summary>
    [SerializeField]
    protected float baseHpRecover = 0;

    /// <summary>
    /// 基础每秒生命恢复值增加量
    /// </summary>
    public float hpRecoverAddedValue = 0;

    /// <summary>
    /// 每秒生命恢复增加百分比
    /// </summary>
    public float hpRecoverRate = 1;

    /// <summary>
    /// 当前每秒生命恢复值
    /// </summary>
    public float hpRecover;

    /// <summary>
    /// 增加每秒生命恢复值
    /// </summary>
    /// <param name="p_hpRecoverAddedValue">增加的每秒生命恢复增加值</param>
    /// <param name="p_hpRecoverRate">增加的每秒生命恢复百分比</param>
    /// <returns>是否增加成功</returns>
    public bool AddHpRecover(float p_hpRecoverAddedValue, float p_hpRecoverRate)
    {
        return ChangeHpRecover(this.hpRecoverAddedValue + p_hpRecoverAddedValue, this.hpRecoverRate + p_hpRecoverRate);
    }

    /// <summary>
    /// 改变每秒生命恢复值
    /// </summary>
    /// <param name="p_hpRecoverAddedValue">新的基础每秒生命恢复增加值</param>
    /// <param name="p_hpRecoverRate">新的每秒生命恢复增加百分比</param>
    /// <returns>是否改变成功</returns>
    public bool ChangeHpRecover(float p_hpRecoverAddedValue, float p_hpRecoverRate)
    {
        this.hpRecoverAddedValue = p_hpRecoverAddedValue;
        this.hpRecoverRate = p_hpRecoverRate;
        CalculateCurrentHpRecover();
        return true;
    }

    /// <summary>
    /// 根据基础每秒生命恢复值、基础每秒生命恢复增加值和每秒生命恢复增加百分比，计算每秒生命恢复值
    /// </summary>
    protected void CalculateCurrentHpRecover()
    {
        hpRecover = (baseHpRecover + hpRecoverAddedValue) * hpRecoverRate;
    }

    /// <summary>
    /// 协程，生命恢复
    /// </summary>
    /// <returns></returns>
    IEnumerator RecoverHpPerSecond()
    {
        while (true)
        {
            if (hpRecover > 0)
            {
                AddHp(hpRecover * Time.deltaTime);
            }
            else if (hpRecover < 0)
            {
                LoseHp(-hpRecover * Time.deltaTime);
            }
            yield return 0;
        }
    }

    /// <summary>
    /// 暂停生命恢复协程
    /// </summary>
    public void PauseHpRecover()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// 重新开始生命恢复协程
    /// </summary>
    public void ResumeHpRecover()
    {
        PauseHpRecover();
        StartCoroutine(RecoverHpPerSecond());
    }
#endif
    #endregion
}