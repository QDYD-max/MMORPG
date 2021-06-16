using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

[CSharpCallLua]
public class XLuaManager : Singleton<XLuaManager>
{
    private LuaEnv luaEnv;
    private string luaScriptsFolder = "LuaScripts";

    public void Init()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(LuaScriptsLoader);
        luaEnv.DoString(String.Format("require 'main'"));
    }

    private byte[] LuaScriptsLoader(ref string filepath)
    {
        filepath = filepath.Replace(".", "/") + ".lua.txt";
        string scriptPath = string.Empty;
        scriptPath = Path.Combine(Application.dataPath, luaScriptsFolder);
        scriptPath = Path.Combine(scriptPath, filepath);
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(scriptPath));
    }
    
    public void Startup()
    {
    }

    public override void Dispose()
    {
        //throw new NotImplementedException();
    }
}