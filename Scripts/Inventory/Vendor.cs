using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Vendor : Inventory
{

    #region 单例模式
    private static Vendor _instance;
    public static Vendor Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("VendorPanel").GetComponent<Vendor>();
            }
            return _instance;
        }
    }
    #endregion

    public int[] itemArray = new int[5] {1, 2, 3, 4, 5}; // 初始化item队列
   // public int[] itemArray;
    private Test playerTest;

    public override void Start()
    {
        base.Start();
        InitShop(); // 初始化商品
        itemArray = new int[2] {1, 2}; //放这不管用？
        playerTest = GameObject.Find("Test").GetComponent<Test>();
        Hide();
    }

    private void InitShop()
    {
       
            foreach(int itemID in itemArray)
            {
                Debug.Log("itemID is " + itemID);
                StoreItem(itemID);
            }
        
        
    }


    public void Consume(Item item)
    {
        int buyPrice = item.BuyPrice;
        if (playerTest.ConsumeCoin(buyPrice)) // 卖成功才行
        {
            Knapsack.Instance.StoreItem(item);
        }
        
    }
    //出售
    //出售的是knapsack里的东西,pickeditem上面的
    public void Pawn()
    {
        int sellAmount = 1;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            sellAmount = 1;
        }
        else
        {
            sellAmount = InventoryManager.Instance.PickedItem.Amount;
        }

        int sellPrice = sellAmount * InventoryManager.Instance.PickedItem.Item.BuyPrice;
        playerTest.EarnCoin(sellPrice); // 改变金钱
        InventoryManager.Instance.RemoveItem(sellAmount); //改变pickeditem (前提，点击vendorpanel)
    }

}
