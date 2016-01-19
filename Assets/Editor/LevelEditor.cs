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
        }
        if (GUILayout.Button("水蓝"))
        {
            level.SetColor(new Color(105 / 255.0F, 129 / 255.0F, 242 / 255.0F));
        }
        if (GUILayout.Button("暗红"))
        {
            level.SetColor(new Color(255 / 255.0F, 94 / 255.0F, 94 / 255.0F));
        }
    }
}

