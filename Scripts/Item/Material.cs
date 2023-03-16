using System;
public class Material : Item
{
    public Material(int iD, string name, ItemType type, ItemQuality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite)
        : base(iD, name, type, quality, description, capacity, buyPrice, sellPrice, sprite)
    {
    }
}

