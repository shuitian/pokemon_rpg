using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// 快速创建编辑器类
/// </summary>
public class FastCreateEditor
{
    /// <summary>
    /// 快速创建角色
    /// </summary>

    [MenuItem("FastCreate/角色/玩家")]
    public static void CreatePlayer()
    {
        GameObject obj = new GameObject("玩家");
    }
}
