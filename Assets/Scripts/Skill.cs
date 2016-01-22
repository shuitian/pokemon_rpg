using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

public class Skill : MonoBehaviour {

    public Dictionary<SkillTriggerState, LuaFunction> skills = new Dictionary<SkillTriggerState, LuaFunction>();
    int round;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
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