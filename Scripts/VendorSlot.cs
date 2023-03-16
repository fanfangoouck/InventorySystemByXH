using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendorSlot : Slot
{
 
    //改写slot这块的交互，商店只能卖出去，不能放东西
    public override void OnPointerDown(PointerEventData eventData)
    {
        //鼠标右键  picked item没有东西
        if(eventData.button == PointerEventData.InputButton.Right  && InventoryManager.Instance.IsPickedItem == false)
        {
            if(transform.childCount > 0) //有东西
            {
                Item vendorItem = transform.GetChild(0).GetComponent<ItemUI>().Item;
                //消费商品
                Vendor.Instance.Consume(vendorItem);
            }
        }

        //鼠标左键点击panel, 且picked item有东西
       if(eventData.button == PointerEventData.InputButton.Left  && InventoryManager.Instance.IsPickedItem == true)
        {
             if(transform.childCount == 0)
            {
                Vendor.Instance.Pawn();
            }
        }


    }
}
