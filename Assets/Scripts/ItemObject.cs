using System.Runtime.CompilerServices;
using UnityEngine;

public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}


public class ItemObject : MonoBehaviour
{
    public string item_name;
    public bool pickupable = false;
    //public bool visibility = true;
    public ItemRarity rarity;

}
