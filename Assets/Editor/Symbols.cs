﻿using UnityEngine;
using UnityEditor;

public class Symbols
{
    static public bool IsDefineSymbolInSymbols(string str, string symbols)
    {
        if (symbols == null || symbols == "" || str.Length > symbols.Length)
        {
            return false;
        }
        if (symbols.IndexOf(";" + str + ";") >= 0 || symbols.IndexOf(str + ";") == 0)
        {
            return true;
        }
        if (str.Length == symbols.Length && symbols == str)
        {
            return true;
        }
        if (str.Length < symbols.Length && symbols.Substring(symbols.Length - str.Length - 1) == ";" + str)
        {
            return true;
        }
        return false;
    }

    static public void AddDefineSymbol(string str, string symbols)
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, ((symbols == "") ? (symbols + ";") : "") + str);
    }

    static public void DeleteDefineSymbol(string str, string symbols)
    {
        int t;
        if ((t = symbols.IndexOf(";" + str + ";")) >= 0)
        {
            string str1 = symbols.Substring(0, t);
            string str2 = symbols.Substring(t + str.Length + 1);
            string str3 = str1 + str2;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, str3);
        }
        else if (symbols.IndexOf(str + ";") == 0)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, symbols.Substring(str.Length + 1));
        }
        else if (str.Length == symbols.Length && symbols == str)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "");
        }
        else if (str.Length < symbols.Length && symbols.Substring(symbols.Length - str.Length - 1) == ";" + str)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, symbols.Substring(0, symbols.Length - str.Length - 1));
        }
    }
}