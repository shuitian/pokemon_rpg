using UnityEngine;
using System.Collections;
using UnityTool.Libgame;

[RequireComponent(typeof(MonsterHpComponent))]
public class Monster : Character
{ 
    public int id;
    public int gold;
}

public class MonsterData
{
    public int id;
    public string name;
    public float hp;
    public float attack;
    public float defence;
    public int gold;
}
