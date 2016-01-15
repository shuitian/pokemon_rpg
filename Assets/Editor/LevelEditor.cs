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
        if (GUILayout.Button("随机生成地形"))
        {
            level.CreateRandomTerrain();
        }
        if (GUILayout.Button("随机生成迷宫"))
        {
            level.CreateRandomMaze();
        }
        if (GUILayout.Button("清除"))
        {
            level.DestoryTerrain();
        }
    }
}
