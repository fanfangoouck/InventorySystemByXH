using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class EquipmentPanel : Inventory
{
     #region 单例模式
    private static EquipmentPanel _instance;
    public static EquipmentPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("EquipmentPanel").GetComponent<EquipmentPanel>();
            }
            return _instance;
        }
    }
    #endregion

    private Text propertyText;
    private Test playerTest;

    public override void Start()
    {
        base.Start();
        //都有find 哦， 静态和非静态
        propertyText = transform.Find("PropertyPanel/PropertyText").GetComponent<Text>(); //  这个只能找子孩子哈，报错
        playerTest = GameObject.FindGameObjectWithTag("Test").GetComponent<Test>();
        Hide();
    }



    /// <summary>
    /// 进来一个item，穿衣，放到equipment面板上的合适位置
    /// </summary>
    /// <param name="item"></param>
    public bool PutOn(Item item) 
    {

        Debug.Log("PutOn");
        bool result = false;
        Item exitItem = null;
        foreach(Slot slot in slotList) //找合适的slot
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot) slot;
            if(equipmentSlot.isRightItem(item)) // item可以放到这个slot里
            {
                
                if(equipmentSlot.transform.childCount > 0) // 槽位已经有东西了
                {
                    //永远是先获得ui,再获得它下面的item
                    ItemUI exitIItemUI = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    if(exitIItemUI.Item.ID != item.ID) // 自己改的，两个id不相同再存
                    {
                        result = true;
                        exitItem = exitIItemUI.Item;
                        equipmentSlot.StoreItem(item); // 进来一个item，放到equipment面板上的合适位置
                        UpdatePropertyText();
                    }
                 
                }
                else// 槽位没东西
                {
                    Debug.Log("PutOn槽位没东西");
                    equipmentSlot.StoreItem(item);//直接存
                    result = true;
                    UpdatePropertyText();
                }
            }
            if(exitItem != null) // 槽位之前有东西,存到exitItem里
            {
                Knapsack.Instance.StoreItem(exitItem);
                result = true;
                UpdatePropertyText();
            }
        }
        Debug.Log("result" + result);
        return result;
    }

    public void PutOff(Item item)
    {
        Knapsack.Instance.StoreItem(item); // 脱下的东西，放到knapsack里
        UpdatePropertyText();
    }

    //这里面算的永远是装备的所有加成，然后再加上玩家原有的基础属性
    public void UpdatePropertyText()
    {
        int strength = 0, intellect = 0, agility = 0, stamina = 0, damage = 0;
        foreach(EquipmentSlot slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                Item currentItem = slot.transform.GetChild(0).GetComponent<ItemUI>().Item; 
                if(currentItem is Equipment)  //  当前物品是装备
                {
                    Equipment e = (Equipment)currentItem;
                    strength += e.Strength;
                    intellect += e.Intellect;
                    agility += e.Agility;
                    stamina += e.Stamina;
                }
                else if(currentItem is Weapon)//  当前物品是武器
                {
                    Debug.Log("((Weapon)currentItem).Damage" + ((Weapon)currentItem).Damage);
                    damage += ((Weapon)currentItem).Damage;
                    Debug.Log("damage " + damage);
                }

            }
        }
        //注意，最后要更新回test 那里
        strength += playerTest.BasicStrength;
        intellect += playerTest.BasicIntellect;
        agility += playerTest.BasicAgility;
        stamina += playerTest.BasicStamina;
        damage += playerTest.BasicDamage;
        Debug.Log("最后的damage " + damage);

        string text = string.Format("力量：{0}\n智力：{1}\n敏捷：{2}\n体力：{3}\n攻击力：{4}\n", strength, intellect, agility, stamina, damage);
        propertyText.text = text;
    }
}
