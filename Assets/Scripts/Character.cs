using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HpComponent))]
public class Character : MonoBehaviour {

    public HpComponent hpComponent
    {
        get
        {
            if(_hpComponent == null)
            {
                _hpComponent = gameObject.GetComponent<HpComponent>();
                if (!_hpComponent)
                {
                    _hpComponent = gameObject.AddComponent<HpComponent>();
                }
            }
            return _hpComponent;
        }
    }
    protected HpComponent _hpComponent;
    public string characterName = "水天";
    protected void Awake()
    {
        _hpComponent = GetComponent<HpComponent>();
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

    public virtual bool Attack(Character target)
    {
        Damage damage = new Damage(this, target, attack * (1 - target.defence / 100));
        Damage.AddDamage(damage);
        //target.LoseHp(attack * (1 - target.defence / 100));
        return true;
    }
}
