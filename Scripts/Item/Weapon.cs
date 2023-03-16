using System;
public class Weapon : Item
{
    public int Damage { get; set; }

    public WeaponType WpType { get; set; }

    public Weapon(int iD, string name, ItemType type, ItemQuality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite,
        int damage, WeaponType wpType)
        : base(iD, name, type, quality, description, capacity, buyPrice, sellPrice, sprite)
    {
        this.Damage = damage;
        this.WpType = wpType;
    }

    public enum WeaponType
    {
        None,
        OffHand,
        MainHand
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        string textWpType = "";

        switch (WpType)
        {
            //不是直接OffHand
            case WeaponType.OffHand:
                textWpType = "副手";
                break;

            case WeaponType.MainHand:
                textWpType = "主手";
                break;
        }

        return string.Format("{0}\n<color=blue>武器类型：{1}\n 攻击力：{2}\n</color>", text, textWpType,Damage);


    }
}

