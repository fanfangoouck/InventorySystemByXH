using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using UnityEngine;
using UnityEngine.UI;

public class Forge : Inventory
{
    #region 单例模式
    private static Forge _instance;
    public static Forge Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("ForgePanel").GetComponent<Forge>();
            }
            return _instance;
        }
    }
    #endregion

    private List<Formula> formulaList; //解析json 合成秘方,目前是2个

    public override void Start()
    {
        base.Start();
        ParseFormulaJson();
    }

    void ParseFormulaJson()
    {
        formulaList = new List<Formula>();
        TextAsset formulasText = Resources.Load<TextAsset>("Formulas");
        string formulasJson = formulasText.text;//配方信息的Json数据
        JSONObject jo = new JSONObject(formulasJson);
        foreach (JSONObject temp in jo.list)
        {
            int item1ID = (int)temp["Item1ID"].intValue;
            int item1Amount = (int)temp["Item1Amount"].intValue;
            int item2ID = (int)temp["Item2ID"].intValue;
            int item2Amount = (int)temp["Item2Amount"].intValue;
            int resID = (int)temp["ResID"].intValue;
            Formula formula = new Formula(item1ID, item1Amount, item2ID, item2Amount, resID);
            formulaList.Add(formula);
        }
    }

    public void ForgeItem()
    {
        Transform IsFillText = transform.Find("IsFillText");
        
        Debug.Log("Knapsack.Instance.KnapIsFill()"+ Knapsack.Instance.KnapIsFill());
        if (Knapsack.Instance.KnapIsFill()) //背包已经满了
        {
            //GameObject IsFillText = GameObject.Find("IsFillText");
            //Text IsFillText = transform.GetComponentInChildren<Text>();
            Debug.Log("是否走到这一步");
            IsFillText. gameObject.SetActive(true);               
        }

        else
        {
           // IsFillText. gameObject.SetActive(false);          
            Debug.Log("两步");
        List<int> haveMaterialIDList = new List<int>();//存储当前ForgePanel拥有的材料的id
        foreach(Slot slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                int amount = slot.transform.GetChild(0).GetComponent<ItemUI>().Amount;
                int id = slot.transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
                for(int i = 0; i < amount; i++)
                {
                     haveMaterialIDList.Add(id); //生成ForgePanel拥有的材料的id list
                }
            }
        }

        Formula matchedFormula = null;
        //是否有秘方匹配
        foreach(Formula  formual in formulaList)
        {
            //第一个秘方生成id序列，和有的材料序列比
            bool isMatch = formual.Match(haveMaterialIDList);
            if (isMatch)//匹配上了
            {
                matchedFormula = formual ;// 记录秘籍;
                //生成锻造武器
                Knapsack.Instance.StoreItem(matchedFormula.ResID);

                //销毁所用的材料
                    //通过id找到对应的item,item数量 -1
                foreach(int i in matchedFormula.NeedIdList)
                {
                    foreach(Slot slot in slotList)
                    {
                        if(slot.transform.childCount > 0)
                        {
                            ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                            if(itemUI.Item.ID == i)
                            {
                                itemUI.ReduceAmount(1);
                                if(itemUI.Amount <= 0 )
                                {
                                    DestroyImmediate(itemUI.gameObject);
                                }
                                break;
                            }
                        }
                    }
                }
            }

            
        }

        }
        

    }







}
