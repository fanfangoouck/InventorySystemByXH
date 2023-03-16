using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;
using UnityEngine.EventSystems;
//using UnityEditor.Sprites;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject itemPrefab;
    public GameObject pickedItem;


    /// <summary>
    /// 储存物品
    /// </summary>
    /// <param name="item"></param>
    public void StoreItem(Item item)
    {
        if (transform.childCount == 0)
        {
            //学习下这块语法
            GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
            //设置父对象
            itemGameObject.transform.SetParent(this.transform);
            //设置大小和位置
            itemGameObject.transform.localScale = Vector3.one;
            itemGameObject.transform.localPosition = Vector3.zero;
            //获得itemui脚本里的方法
            Debug.Log("获得itemui脚本里的方法,item is " + item);
            itemGameObject.GetComponent<ItemUI>().SetItem(item);
        }
        else
        {
            //默认添加1个
            transform.GetChild(0).GetComponent<ItemUI>().ChangeAmount();
        }
    }

    /// <summary>
    /// 得到当前物品槽存储的物品类型
    /// </summary>
    /// <returns></returns>
    public Item.ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.Type;
    }

    /// <summary>
    /// 得到物品的id
    /// </summary>
    /// <returns></returns>
    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }

    /// <summary>
    /// 槽位是否已满
    /// </summary>
    /// <returns></returns>
    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount >= itemUI.Item.Capacity;//当前的数量大于等于容量
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //GameObject.Find("PickedItem").SetActive(true);

        if (!InventoryManager.Instance.IsPickedItem)
        {
            //设置一个三秒倒计时
            if (transform.childCount > 0)
            {
                string content = transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
                InventoryManager.Instance.ToolTipShow(content);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            InventoryManager.Instance.ToolTipHide();
        }
    }

    //键盘鼠标都可以
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        //右键，自动装备衣服到equipment panel
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            //如果鼠标上没pick item, slot里也有东西
            if(InventoryManager.Instance.IsPickedItem == false && transform.childCount > 0) 
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
            
                // 符合装备的条件的话，把slot里删掉一个东西，放到装备里
                if(currentItemUI.Item is Equipment || currentItemUI.Item is Weapon)
                {
                    Debug.Log("PutOn(item)外");
                    Item item = currentItemUI.Item;
                    if (EquipmentPanel.Instance.PutOn(item))
                    {
                         Debug.Log("PutOn(item) 里");
                         currentItemUI.ReduceAmount(1);
                        if (currentItemUI.Amount <= 0)
                        {
                            DestroyImmediate(currentItemUI.gameObject);
                            InventoryManager.Instance.ToolTipHide(); //  gameObject都没了， 隐藏tips有必要吗？ 这个和tip有啥关系
                        }
                    } //放到装备里
                }

            }
        }



        if (eventData.button != PointerEventData.InputButton.Left) return;

        //GameObject.Find("PickedItem").SetActive(true);
        int amountPicked = InventoryManager.Instance.PickedItem.Amount;
        

        if (transform.childCount == 0)//Slot为空
        {
            if (InventoryManager.Instance.IsPickedItem)//有东西
            {
                if (Input.GetKey(KeyCode.LeftControl)) // 按ctrl
                {
                    //只放一个item            
                    this.StoreItem(InventoryManager.Instance.PickedItem.Item);//前面+this 的区别？
                    InventoryManager.Instance.RemoveItem(); // Pickeditem取掉一个

                    //不用我自己写的了，用原方法。看问题在哪
                    //InventoryManager.Instance.PickedItem_ShowOrHide(true, -1); //pickedItem -1

                }
                else //全部放入slot
                {
                    //item.amount是否小于capacity
                    if (amountPicked > InventoryManager.Instance.PickedItem.Item.Capacity)
                    {
                        amountPicked = InventoryManager.Instance.PickedItem.Item.Capacity;
                    }

                    for (int i = 1; i <= amountPicked; i++)
                    {
                        Debug.Log("02");
                        this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                    }
                    InventoryManager.Instance.RemoveItem(amountPicked);
                    //InventoryManager.Instance.PickedItem_ShowOrHide(true, -amountPicked);
                }
            }
        }
        else//Slot不为空
        {
            Debug.Log("0");
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();

            if (InventoryManager.Instance.IsPickedItem)//有东西
            {
                Debug.Log("1");
                if (InventoryManager.Instance.PickedItem.Item.ID == this.GetItemId())//picked item和slot一样
                {
                    Debug.Log("2");
                    if (Input.GetKey(KeyCode.LeftControl)) // 按ctrl
                    {
                        Debug.Log("3");
                        if (!IsFilled()) // 没有填满
                        {
                            Debug.Log("4");
                            InventoryManager.Instance.RemoveItem(); // Pickeditem取掉一个
                            this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                        }
                        else // Slot里的一个个转到item
                        {
                            Debug.Log("new 4");
                            transform.GetChild(0).GetComponent<ItemUI>().ReduceAmount();//Slot减一个
                              //if( transform.GetChild(0).GetComponent<ItemUI>().Amount <= 0)
                              //  {
                              //      Debug.Log("10");
                              //      Destroy(currentItem.gameObject); 
                              //  }
                              InventoryManager.Instance.AddAmount(); // Pickeditem加一个
                        }
                    }
                    else // 没按ctrl
                    {
                        Debug.Log("5");

                        if (!IsFilled()) // 没有填满
                        {
                            //一次性移走最小值
                            Debug.Log("6");
                            int amount = Mathf.Min(currentItem.Item.Capacity - currentItem.Amount, amountPicked);
                            InventoryManager.Instance.RemoveItem(amount); // Pickeditem取掉amount个
                            for (int i = 0; i < amount; i++)
                            {
                                this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                            }
                        }
                        else // Slot里的全都转到item
                        {
                            Debug.Log("new 6");
                            InventoryManager.Instance.PickedItem.ChangeAmount(currentItem.Amount);
                             Destroy(currentItem.gameObject);
                        }
                    }
                }

                else//picked item和slot不一样
                {
                    //交换位置
                    Debug.Log("7");
                    Item item = currentItem.Item;
                    int amount = currentItem.Amount;
                    //都用的是setitem方法，对象要搞好
                    //划重点，记一下
                     //pickeditem的数量 必须得比capacity低
                    if(amountPicked <= InventoryManager.Instance.PickedItem.Item.Capacity)
                    {
                        currentItem.SetItem(InventoryManager.Instance.PickedItem.Item, amountPicked);
                        InventoryManager.Instance.PickedItem.SetItem(item, amount);
                    }
                }
            }

            else//picked没有东西
            //背包往pickitem转移（与上面完全相反）
            {
                Debug.Log("8");
                if (Input.GetKey(KeyCode.LeftControl)) // 按ctrl
                {
                    Debug.Log("9");
                    //this.StoreItem(InventoryManager.Instance.PickedItem.Item);//官方用法
                    transform.GetChild(0).GetComponent<ItemUI>().ReduceAmount();//Slot减一个
                    if( transform.GetChild(0).GetComponent<ItemUI>().Amount <= 0)
                    {
                        Debug.Log("10");
                        Destroy(currentItem.gameObject); 
                    }
                    //InventoryManager.Instance.AddAmount(); // Pickeditem加一个
                    InventoryManager.Instance.ShowPickedItem(currentItem.Item, 1);
                }
                else // slot里的全部放入背包
                {
                    Debug.Log("11");
                    int amount = currentItem.Amount; // 要增加的数量
                    InventoryManager.Instance.ShowPickedItem(currentItem.Item, amount);
                    Destroy(currentItem.gameObject);
                }   
            }
        }
    }
}