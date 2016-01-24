using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(HpComponent))]
public class HpComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("设置MAX_HP"))
        {
#if UNITY_STANDALONE_WIN
            string s = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
            if (!Symbols.IsDefineSymbolInSymbols("MAX_HP", s))
            {
                Symbols.AddDefineSymbol("MAX_HP", s);
                EditorUtility.DisplayDialog("", "成功添加预定义MAX_HP", "OK");
            }
            else
            {
                Symbols.DeleteDefineSymbol("MAX_HP", s);
                EditorUtility.DisplayDialog("", "成功删除预定义MAX_HP", "OK");
            }
#endif
        }
        if (GUILayout.Button("设置HP_RECOVER"))
        {
#if UNITY_STANDALONE_WIN
            string s = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
            if (!Symbols.IsDefineSymbolInSymbols("HP_RECOVER", s))
            {
                Symbols.AddDefineSymbol("HP_RECOVER", s);
                EditorUtility.DisplayDialog("", "成功添加预定义HP_RECOVER", "OK");
            }
            else
            {
                Symbols.DeleteDefineSymbol("HP_RECOVER", s);
                EditorUtility.DisplayDialog("", "成功删除预定义HP_RECOVER", "OK");
            }
#endif
        }
    }
}
