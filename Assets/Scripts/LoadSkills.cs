using UnityEngine;
using System.Collections;
using System;
using System.IO;
using LuaInterface;

public class LoadSkills : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LuaScriptMgr mgr = new LuaScriptMgr();
        mgr.Start();
        ArrayList professions = ListFiles(Application.dataPath + "/Lua/");
        if (professions!=null)
        {
            foreach (string path in professions)
            { 
                mgr.DoFile(ReviseLuaPath(path));
                LuaFunction f = mgr.GetLuaFunction("get_acc");
                object[] r = f.Call("Hello");
                foreach(var r1 in r)
                {
                    print(r1);
                }
            }
        }
    }
    public string ReviseLuaPath(string path)
    {
        return "../../Lua/" + path;
    }
    // Update is called once per frame
    void Update () {
	
	}

    public static ArrayList ListFiles(string filePath)
    {
        FileSystemInfo info = new DirectoryInfo(filePath);
        if (!info.Exists) return null;
        DirectoryInfo dir = info as DirectoryInfo;
        //不是目录 
        if (dir == null) return null;
        FileSystemInfo[] files = dir.GetFileSystemInfos();
        ArrayList professions = new ArrayList();
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = files[i] as FileInfo;
            //是文件 
            if (file != null && !file.FullName.EndsWith(".meta") && !file.FullName.EndsWith(".cs"))
            {
                professions.Add(file.Name);
            }
        }
        return professions;
    }
}
