using System;
using System.Collections;
using System.Collections.Generic;
using Network;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private void Awake()
    {
        XLuaManager.Instance.Init();
        TcpNetwork test = new TcpNetwork();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
