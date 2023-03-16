using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formula
{
      public int Item1ID { get;private set; }
      public int Item1Amount { get;private set; }
      public int Item2ID { get;private set; }
      public int Item2Amount { get;private set; }

      public int ResID { get;private set; }//锻造结果的物品

      private List<int> needIdList = new List<int>();//所需要的物品的id

      public List<int> NeedIdList
       {
            get
            {
                return needIdList;
            }
       }

      public Formula(int item1ID, int item1Amount, int item2ID, int item2Amount, int resID)
       {
            this.Item1ID = item1ID;
            this.Item1Amount = item1Amount;
            this.Item2ID = item2ID;
            this.Item2Amount = item2Amount;
            this.ResID = resID;

          //生成所需材料的id list ,用于匹配
          //之前是写在下面的，优化后写在这里，只用初始化一次
           for (int i = 0; i < Item1Amount; i++) 
            {
                needIdList.Add(Item1ID);
            }
            for (int i = 0; i < Item2Amount; i++)
            {
                needIdList.Add(Item2ID);
            }
       }

      public bool Match(List<int> idList)
      {
           //为了不改变原idlist的值，赋予了一个temp值过渡
           List<int> tempidList = new List<int>(idList); //记一下这种写法
           foreach(int idItem in needIdList)
           {
               bool isMatch = tempidList.Remove(idItem);
                if (!isMatch)
                {
                    return false;
                }
           }
           return true;
      }

}
