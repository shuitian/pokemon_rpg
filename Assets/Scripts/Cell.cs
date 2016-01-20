using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

    public int id;
    public Item item;
    public Monster monster;

    public bool IsItem()
    {
        return id <= (int)CellType.add_hp_10000 && id >= (int)CellType.add_attack_10;
    }

    public bool IsMonster()
    {
        return id > 0 && id <= LoadResources.maxMonster;
    }

    public bool IsUpFloor()
    {
        return id == (int)CellType.up_floor;
    }

    public bool IsDownFloor()
    {
        return id == (int)CellType.down_floor;
    }

    public bool IsWall()
    {
        return id == (int)CellType.WALL;
    }

    public bool IsRoad()
    {
        return id == (int)CellType.ROAD;
    }
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