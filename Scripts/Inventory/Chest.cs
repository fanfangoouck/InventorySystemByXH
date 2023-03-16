using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Inventory
{
    #region 单例模式
    private static Chest _instance;
    public static Chest Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.Find("ChestPanel").GetComponent<Chest>();//获取脚本
            }
            return _instance;
        }
    }
    #endregion

    //不加这个的话，会报错，stackoverflow, 一开始获取不到_instance
}
