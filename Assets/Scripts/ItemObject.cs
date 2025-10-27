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
    public ItemRarity rarity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
