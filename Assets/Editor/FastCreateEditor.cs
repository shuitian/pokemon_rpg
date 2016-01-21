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
        var option = EditorUtility.DisplayDialog("","确认重新生成地形？", "确认", "取消");
        if (!option)
        {
            return;
        }
        ClearTerrain();
        for (int i = 0; i < Game.maxLevel; i++)
        {
            GameObject obj = ObjectPool.Instantiate(level, Vector3.zero, Quaternion.identity, terrain.transform);
            obj.name = "Level_" + i;
            Level l = obj.GetComponentInChildren<Level>();
            l.CreateRandomMaze();
            if (i != 0)
            {
                l.gameObject.SetActive(false);
            }
        }
    }

    [MenuItem("FastCreate/添加地形")]
    public static void AddTerrain()
    {
        for (int i = terrain.transform.childCount; i < Game.maxLevel; i++)
        {
            GameObject obj = ObjectPool.Instantiate(level, Vector3.zero, Quaternion.identity, terrain.transform);
            obj.name = "Level_" + i;
            Level l = obj.GetComponentInChildren<Level>();
            l.CreateRandomMaze();
            l.gameObject.SetActive(false);
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

    [MenuItem("FastCreate/Test")]
    public static void Test()
    {
        MonsterData m = Sql.GetMonsterData(1);
        MonoBehaviour.print(m.id);
        MonoBehaviour.print(m.name);
        MonoBehaviour.print(m.hp);
        MonoBehaviour.print(m.attack);
        MonoBehaviour.print(m.defence);
        MonoBehaviour.print(m.gold);
    }

    [MenuItem("FastCreate/关闭数据库连接")]
    public static void ClosConnection()
    {
        Sql.CloseDB();
    }
}
