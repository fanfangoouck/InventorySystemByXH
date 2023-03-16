using UnityEngine;
using System.Collections;

public class Knapsack : Inventory
{
    #region 单例模式
    private static Knapsack _instance;
    public static Knapsack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("KnapsackPanel").GetComponent<Knapsack>();
            }
            return _instance;
        }
    }
    #endregion

    //背包是否满了
    public bool KnapIsFill()
    {
        foreach(Slot slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                return false;
            }
        }
         return true;
    }


    public  void ManagerKnap()
    {
        ManagerKnap2();
        //while (ifHasEmpty())
        //{
        //    ManagerKnap2();
        //}
    }



    public void ManagerKnap2()
    {
        bool ifContinue = ifHasEmpty();
       
            int length = slotList.Length;
            for(int i = length - 1; i > 0; i--)
            {
                //空slot，跳出循环
                if (slotList[i].transform.childCount != 0)
                {
                    //获得这个item
                    ItemUI targetItemUI = slotList[i].transform.GetComponentInChildren<ItemUI>();
            

                    for(int j = 0; j < i; j++)
                    {
                        //没有东西  slotList[i].transform.childCount ===0
                            //直接放
                        //有东西
                            //ID相同 && 容量不满
                                //把当前物品放进去
                        //否则继续找下一个

                        ItemUI putItemUI = slotList[j].transform.GetComponentInChildren<ItemUI>();

                        if (slotList[j].transform.childCount == 0)//没有东西
                        {
                            //直接放
                            for(int k = 0; k < targetItemUI.Amount; k++)
                            {
                                slotList[j].StoreItem(targetItemUI.Item);
                                targetItemUI.ReduceAmount();
                                if(targetItemUI.Amount <= 0)
                                {
                                    DestroyImmediate(targetItemUI.gameObject);
                                }
                            }
                        }
                        else
                        {
                            if(putItemUI.Item.ID == targetItemUI.Item.ID  &&  !slotList[j].IsFilled()) //ID相同 && 容量不满
                            {
                                //直接放
                                int putAmount = Mathf.Min(putItemUI.Item.Capacity - putItemUI.Amount, targetItemUI.Amount) ;
                                for(int k = 0; k <putAmount; k++)
                                {
                                     slotList[j].StoreItem(targetItemUI.Item);
                                     targetItemUI.ReduceAmount();
                                    if(targetItemUI.Amount <= 0)
                                    {
                                        DestroyImmediate(targetItemUI.gameObject);
                                    }
                                }
                            }
                        }

                    }
                }
            }
    }

    public bool ifHasEmpty()
    {
         int length = slotList.Length;
         int lastEmptySlot = 0;
        for(int i = length - 1; i >= 0; i--)
        {
                if (slotList[i].transform.childCount != 0)
                {
                    lastEmptySlot = i;
                    break; // 跳出for循环
                }
              
        }

        for(int j = 0; j < lastEmptySlot; j++)
        {
                {
                    if (slotList[j].transform.childCount != 0)
                    {
                        return true;     // 前面有空的
                    }
                }
        }

        return false; // 前面没空的
                   
    }

}
