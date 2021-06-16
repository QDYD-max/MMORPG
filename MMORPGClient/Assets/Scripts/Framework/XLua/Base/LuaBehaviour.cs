using System;
using UnityEngine;
using XLua;

[System.Serializable]
public class Injection
{
    public string name;
    public GameObject value;
}

[LuaCallCSharp]
public class LuaBehaviour : MonoBehaviour
{
    public TextAsset luaScript;
    public Injection[] injections;

    internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1; //1 second 

    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;
    private Action luaFixedUpdate;
    private Action luaLateUpdate;
    private Action luaOnDisable;

    private LuaTable scriptEnv;

    void Awake()
    {
        scriptEnv = luaEnv.NewTable();

        // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);
        foreach (var injection in injections)
        {
            scriptEnv.Set(injection.name, injection.value);
        }

        luaEnv.DoString(luaScript.text, env: scriptEnv);

        Action luaAwake = scriptEnv.Get<Action>("awake");
        scriptEnv.Get("start", out luaStart);
        scriptEnv.Get("update", out luaUpdate);
        scriptEnv.Get("ondestroy", out luaOnDestroy);
        scriptEnv.Get("fixedupdate", out luaFixedUpdate);
        scriptEnv.Get("lateupdate", out luaLateUpdate);
        scriptEnv.Get("ondisable", out luaOnDisable);

        luaAwake?.Invoke();
    }

    // Use this for initialization
    void Start()
    {
        luaStart?.Invoke();
    }

    private void FixedUpdate()
    {
        luaFixedUpdate?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        luaUpdate?.Invoke();

        if (Time.time - lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            lastGCTime = Time.time;
        }
    }

    private void LateUpdate()
    {
        luaLateUpdate?.Invoke();
    }

    private void OnDisable()
    {
        luaOnDisable?.Invoke();
    }

    void OnDestroy()
    {
        luaOnDestroy?.Invoke();

        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
        scriptEnv.Dispose();
        injections = null;
    }
}