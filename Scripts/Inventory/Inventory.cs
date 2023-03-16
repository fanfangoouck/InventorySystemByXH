using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class Inventory : MonoBehaviour
{
    protected Slot[] slotList;

    //public static Inventory Instance;
    private int  target = 1; // 隐藏或者显示
    private int  smoothing = 4;
    private CanvasGroup canvasGroup;
    

    public virtual void Start()
    {
        //把所有的孩子放到这个数组里
        slotList = GetComponentsInChildren<Slot>();
        //Instance = GameObject.Find("Inventory").GetComponent<Inventory>();
        canvasGroup = GetComponent<CanvasGroup>();   
    }

    void Update()
    {
        if(canvasGroup.alpha != target)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, target, smoothing * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - target) < 0.02f)
            {
                canvasGroup.alpha = target;
            }
        }
    }

    public void DisplaySwitch()
    {
        if (target == 1)
        {
            target = 0;
            canvasGroup.blocksRaycasts = false; // blocksRaycasts = False, 不能交互
        }
        else
        {
             target = 1;
            canvasGroup.blocksRaycasts = true;
        }
            
    }


    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }
    public bool StoreItem(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("要存储的物品的id不存在");
            return false;
        }
        if (item.Capacity == 1) //容量是1，本身只能找空槽位
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("没有空的物品槽");
                return false;
            }
            else
            {
                slot.StoreItem(item);//把物品存储到这个空的物品槽里面
            }
        }
        else
        {
            Slot slot = FindSameIdSlot(item);
            if (slot != null)
            {
                slot.StoreItem(item);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.StoreItem(item);
                }
                else
                {
                    Debug.LogWarning("没有空的物品槽");
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 这个方法用来找到一个空的物品槽
    /// </summary>
    /// <returns></returns>
    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }


    private Slot FindSameIdSlot(Item item)
    {
        foreach (Slot slot in slotList)
        {
            //挂载的孩子有一个（只能有一个） && 孩子item的类型和当前item类型一样&&槽位没有满
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.ID && slot.IsFilled() == false)
            {
                return slot;
            }
        }
        return null;
    }

    public void TestInventory()
    {
        Debug.Log("TestInventory()可以");
    }

    public void Show()
    {
        canvasGroup.blocksRaycasts = true;
        target = 1;
    }
    public void Hide()
    {
        canvasGroup.blocksRaycasts = false;
        target = 0;
    }


    #region save and load
    public void SaveInventory()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                sb.Append(itemUI.Item.ID + ","+itemUI.Amount+"-");
            }
            else
            {
                sb.Append("0-");
            }
        }
        PlayerPrefs.SetString(this.gameObject.name, sb.ToString());
    }
    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey(this.gameObject.name) == false) return;
        string str = PlayerPrefs.GetString(this.gameObject.name);
        //print(str);
        string[] itemArray = str.Split('-');
        for (int i = 0; i < itemArray.Length-1; i++)
        {
            string itemStr = itemArray[i];
            if (itemStr != "0")
            {
                //print(itemStr);
                string[] temp = itemStr.Split(',');
                int id = int.Parse(temp[0]);
                Item item = InventoryManager.Instance.GetItemById(id);
                int amount = int.Parse(temp[1]);
                for (int j = 0; j < amount; j++)
                {
                    slotList[i].StoreItem(item);
                }
            }
        }
    }
    #endregion
}
