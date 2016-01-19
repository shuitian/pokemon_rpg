using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

    public Monster monster;
}
public enum CellType
{
    add_attack_10 = -8,
    add_defence_1,
    add_hp_100,
    add_hp_1000,
    add_hp_10000,
    down_floor,
    up_floor,
    WALL = -1,
    ROAD = 0,
}