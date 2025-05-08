using System;
using UnityEngine;

[Serializable]
public class AisleItem
{
    public string name;
    public Sprite image;
    public string productDesciption;
    public string[] positiveEthicCategories;
    public string[] negativeEthicCategories;

    [NonSerialized]
    public Aisle aisle; // The aisle this item belongs to

}
