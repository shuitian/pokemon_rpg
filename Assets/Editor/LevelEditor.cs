using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Level level = (Level)target;
        if (GUILayout.Button("随机生成迷宫"))
        {
            level.CreateRandomMaze();
        }
        if (GUILayout.Button("清除"))
        {
            level.ClearTerrain();
        }
        if (GUILayout.Button("草绿"))
        {
            level.SetColor(new Color(125 / 255.0F, 252 / 255.0F, 0 / 255.0F));
            level.GetComponent<SpriteRenderer>().sprite = LoadResources.road;
        }
        if (GUILayout.Button("水蓝"))
        {
            level.SetColor(Color.white);
            level.GetComponent<SpriteRenderer>().sprite = LoadResources.blue;
        }
        if (GUILayout.Button("灰色"))
        {
            level.SetColor(Color.white);
            level.GetComponent<SpriteRenderer>().sprite = LoadResources.road;
        }
        if (GUILayout.Button("大红"))
        {
            level.SetColor(Color.red);
            level.GetComponent<SpriteRenderer>().sprite = LoadResources.road;
        }
    }
}

