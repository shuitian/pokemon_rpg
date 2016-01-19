using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

    public Monster monster;
}
public enum CellType
{
    ROAD = 0,
    WALL = -1,
    UPSTAIRS = -2,
    DOWNSTAIRS = -3,
}