using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityTool.Libgame;

/// <summary>
/// 快速创建编辑器类
/// </summary>
public class FastCreateEditor
{
    static GameObject level = Resources.Load("Prefabs/Level") as GameObject;
    static GameObject terrain = GameObject.FindGameObjectWithTag("Terrain");
    /// <summary>
    /// 快速创建角色
    /// </summary>
    [MenuItem("FastCreate/生成地形")]
    public static void CreateTerrain()
    {
        ClearTerrain();
        for (int i = 0; i < Game.maxLevel; i++)
        {
            GameObject obj = ObjectPool.Instantiate(level, Vector3.zero, Quaternion.identity, terrain.transform);
            obj.name = "Level_" + i;
            Game.levels[i] = obj.GetComponent<Level>();
            Game.levels[i].CreateRandomMaze();
        }
        for (int i = 1; i < Game.maxLevel; i++)
        {
            Game.levels[i].gameObject.SetActive(false);
        }
    }

    [MenuItem("FastCreate/清除地形")]
    public static void ClearTerrain()
    {
        for (int i = 0; i < terrain.transform.childCount;)
        {
            Object.DestroyImmediate(terrain.transform.GetChild(i).gameObject);
        }
    }
}
