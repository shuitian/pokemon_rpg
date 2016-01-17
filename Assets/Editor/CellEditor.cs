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
        if (cell.number == (int)CellType.ROAD)
        {
            cell.GetComponent<SpriteRenderer>().sprite = Level.roads[Random.Range(0, Level.roads.Length)];
        }
        else if (cell.number == (int)CellType.WALL)
        {
            cell.GetComponent<SpriteRenderer>().sprite = Level.walls[Random.Range(0, Level.walls.Length)];
        }
        else if (cell.number >= 1 && cell.number <= Level.maxData)
        {
            cell.GetComponent<SpriteRenderer>().sprite = Level.pics[cell.number - 1];
        }
    }
}
