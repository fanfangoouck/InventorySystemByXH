using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public ItemQuality Quality { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string Sprite { get; set; }


    //必须要有一个默认构造函数，不然其他子类引用时会报错
    public Item()
    {
        this.ID = -1;
    }

    public Item(int iD, string name, ItemType type, ItemQuality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite)
    {
        ID = iD;
        Name = name;
        Type = type;
        Quality = quality;
        Description = description;
        Capacity = capacity;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
        Sprite = sprite;
    }

    // <summary>
    /// 物品类型
    /// </summary>
    public enum ItemType
    {
        Consumable,
        Equipment,
        Weapon,
        Material
    }

    /// <summary>
    ///  物品质量
    /// </summary>
    public enum ItemQuality
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Artifact
    }


    public virtual string GetToolTipText()
    {
        string color = "";
        switch (Quality)
        {
            case ItemQuality.Common:
                color = "white";
                break;
            case ItemQuality.Uncommon:
                color = "lime";
                break;
            case ItemQuality.Rare:
                color = "navy";
                break;
            case ItemQuality.Epic:
                color = "magenta";
                break;
            case ItemQuality.Legendary:
                color = "orange";
                break;
            case ItemQuality.Artifact:
                color = "red";
                break;
        }

        string text = string.Format("<color={4}>{0}</color>\n" +
            "<size=12><color=navy>购买价格:{1}   出售价格: {2} </color></size>\n"
            + "<size=12><color=yellow>{3}</color></size>",
            Name, BuyPrice, SellPrice, Type, color);
        return text;
    }

}
