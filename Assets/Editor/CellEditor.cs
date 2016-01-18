using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Cell))]
public class CellEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Cell cell = (Cell)target;
        int t = cell.number;
        cell.number = EditorGUILayout.IntField("Number", cell.number);
        if (t == cell.number)
        {
            return;
        }
        if (cell.number == (int)CellType.WALL)
        {
            cell.GetComponent<SpriteRenderer>().sprite = Level.walls[Random.Range(0, Level.walls.Length)];
        }
        else
        {
            cell.GetComponent<SpriteRenderer>().sprite = Level.roads[Random.Range(0, Level.roads.Length)];
        }
        if (cell.number == (int)CellType.ROAD || cell.number == (int)CellType.WALL)
        {
            cell.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (cell.number == (int)CellType.UPSTAIRS)
        {
            cell.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Level.up_floor;
        }
        else if (cell.number == (int)CellType.DOWNSTAIRS)
        {
            cell.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Level.down_floor;
        }
        else if (cell.number >= 1 && cell.number <= Level.maxData)
        {
            cell.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Level.pics[cell.number - 1];
        }
    }
}
