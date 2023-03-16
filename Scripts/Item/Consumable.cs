using System;
public class Consumable : Item
{
	public int HP { get; set; }
    public int MP { get; set; }


    public Consumable(int iD, string name, ItemType type, ItemQuality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite, int hP, int mP)
        : base(iD, name, type, quality, description, capacity, buyPrice, sellPrice, sprite)
    {
        this.HP = hP;
        this.MP = mP;
    }

    public override string ToString()
    {
        string s = "";
        s += ID.ToString();
        s += Type;
        s += Quality;
        s += Description;
        s += Capacity;
        s += BuyPrice;
        s += SellPrice;
        s += Sprite;
        s += HP;
        s += MP;
        return s;
    }


    //public override string GetToolTipText()
    //{
    //    string text = base.GetToolTipText();
    //    string newText = string.Format("{0}\n\n<color = blue>加血：{1}\n" +
    //        "加蓝：{2}</color>",
    //        text, HP, MP);
    //    return newText;
    //}

    //public override string GetToolTipText()
    //{
    //    string text = base.GetToolTipText();

    //    string newText = string.Format("{0}\n\n<color=blue>加血：{1}\n加蓝：{2}</color>", text, HP, MP);

    //    return newText;
    //}

    /*
   public override string GetToolTipText()
   {
       string text = base.GetToolTipText();

       string newText = string.Format("{0}\n<color=blue>加血：{1}\n加蓝：{2}</color>", text, HP, MP);
       return text;

       //不行

       string text = string.Format("<color={4}>{0}</color>\n" +
           "<size=10><color=green>购买价格：{1}  出售价格：{2}</color></size>\n"
           + "<size=10><color=yellow>{3}</color></size>",
           Name, BuyPrice, SellPrice, Description, color);
       return text;
       */


    //可以
    //离谱啊，多加一个空行就不行了
    /*
    string text = string.Format("<color={4}>{0}</color>\n" +
        "<size=10><color=green>购买价格：{1}  出售价格：{2}</color></size>\n<size=10><color=yellow>{3}</color></size>\n",
        Name, BuyPrice, SellPrice, Description, color);
    return text;
    */


    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();

        string newText = string.Format("{0}\n<color=blue>加血：{1}\n加蓝：{2}</color>", text, HP, MP);

        return newText;
    }


    }

