using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

public class Skill : MonoBehaviour {

    public bool isPassive;
    int round;
	// Use this for initialization
	
}

public enum SkillTriggerState
{
    RoundStart,
    BeforeAttack,
    Attack,
    BeforeDamage,
    Damaged,
    AfterDamage,
    RoundEnd,
}