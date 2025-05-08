using System;
using UnityEngine;

[Serializable]
public class AisleItem
{
    public string name;
    public Sprite image;

    [NonSerialized]
    public Aisle aisle; // The aisle this item belongs to

}
