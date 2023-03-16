using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using static UnityEditor.Progress;

public class EquipmentSlot : Slot
{
     public Equipment.EquipmentType equipType;
     public Weapon.WeaponType wpType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        //右键
            //如果pickeditem 没东西 && 当前slot有物品
            //  当前物品销毁；把当前物品的item放到knapsack里，

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(InventoryManager.Instance.IsPickedItem == false && transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                Item currentItem = currentItemUI.Item;
                DestroyImmediate(currentItemUI.gameObject); // 为啥不是currentItem.gameObject

                EquipmentPanel.Instance.PutOff(currentItem);
                // Knapsack.Instance.StoreItem(currentItem); 这个可以
                //！！！下面这两个都不对，再看看。。。
                //transform.parent.GetComponent<EquipmentPanel>().PutOff(currentItem);
                //transform.parent.SendMessage("PutOff", currentItem); // 记一下，它的父亲就是equipment panel
                InventoryManager.Instance.ToolTipHide();
            }
        }
     


        if (eventData.button != PointerEventData.InputButton.Left) return; // 不是左键，就返回
        //手上有装备
            //当前装备槽有装备
                //判断手上的设备是否合适
                    //合适  -- 交换
                    //不合适 -- 没反应
            //当前装备槽无装备 -- 装备
                //判断手上的设备是否合适
                    //合适  -- 交换
                    //不合适 -- 没反应

        //手上没东西
            //当前装备槽有装备 -- 放到手上
            //当前装备槽没装备 -- 没反应

        ItemUI pickedItemUI = InventoryManager.Instance.PickedItem;
        

        if(InventoryManager.Instance.IsPickedItem == true)//手上有装备
        {
            if(transform.childCount > 0)//当前装备槽有装备
            {
                ItemUI equipItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                if (isRightItem(InventoryManager.Instance.PickedItem.Item))
                {
                    //当前装备槽里面的物品
                    int amount = equipItemUI.Amount;
                    Item item = equipItemUI.Item;
                    equipItemUI.SetItem(pickedItemUI.Item, pickedItemUI.Amount); // 装备槽
                    InventoryManager.Instance.PickedItem.SetItem(item, amount);//手上
                    EquipmentPanel.Instance.UpdatePropertyText();
                } 
            }
            else //当前装备槽无装备
            {
                if (isRightItem(InventoryManager.Instance.PickedItem.Item))
                {
                    this.StoreItem(InventoryManager.Instance.PickedItem.Item); // 明天看下这里
                    //equipItemUI.SetItem(pickedItemUI.Item, pickedItemUI.Amount); // 装备槽
                    InventoryManager.Instance.RemoveItem(1);
                    EquipmentPanel.Instance.UpdatePropertyText();
                }
            }
        }
        else //手上没东西
        {
            if(transform.childCount > 0)//当前装备槽有装备
            {
                ItemUI equipItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                InventoryManager.Instance.ShowPickedItem(equipItemUI.Item, equipItemUI.Amount);//手上 万一超了怎么办？？
                Debug.Log("走到这了");
                Destroy(equipItemUI.gameObject); // 记一下。 上面的RemoveItem() 可以这样写吗
                EquipmentPanel.Instance.UpdatePropertyText();
            }
        }
        
    }

    public bool isRightItem(Item item)
    {
       //item 是父类, equipment是子类。 判断方法 -- is； 强制转换( Equipment)item
        if(((item is Equipment && ((Equipment)item).EquipType == this.equipType))||
                (item is Weapon && ((Weapon)item).WpType == this.wpType))
        {
            return true;
        }
        return false;
    }
}
