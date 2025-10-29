using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Tilemaps.TilemapRenderer;


public enum SortOrder
{
    Ascending,
    Descending
}


public class Inventory : MonoBehaviour
{
    public List<ItemObject> items = new List<ItemObject>();
    public GameManager manager;
    public SortOrder sort_order = SortOrder.Ascending;

    public void addItem(ItemObject item)
    {
        if(manager.current_game_state == GameManager.GameState.GAMEPLAY)
        {
            items.Add(item);
        }
        
        
    }

    public void removeItem(ItemObject item)
    {
        if (manager.current_game_state == GameManager.GameState.GAMEPLAY)
        {
            items.Remove(item);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindAnyObjectByType<GameManager>();
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ItemObject collisionItem = hit.gameObject.GetComponent<ItemObject>();

        if (collisionItem != null && collisionItem.pickupable)
        {
            addItem(collisionItem);
            collisionItem.pickupable = false;
            collisionItem.gameObject.SetActive(false);
            //Destroy(collisionItem.gameObject);
            //items.Sort();
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    addItem("manually_added_item");
        //}

        if (Input.GetKeyDown(KeyCode.B))
        {
            bubbleSort(items);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            insertionSort(items);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            toggleSortOrder();
        }


    }

    void bubbleSort(List<ItemObject> list)
    {
        int n = list.Count;
        bool swapped;

        for (int i = 0; i < n - 1; i++)
        {
            swapped = false;

            for (int j = 0; j < n - 1; j++)
            {
                if (sort_order == SortOrder.Ascending)
                {
                    if (string.Compare(list[j].name, list[j + 1].name) > 0)
                    {
                        ItemObject temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                        swapped = true; 

                    }
                }
                else
                {
                    if (string.Compare(list[j].name, list[j + 1].name) < 0)
                    {
                        ItemObject temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                        swapped = true;
                    }
                }
            }
            if (swapped == false)
            {
                break;
            }
        }
    }

    void insertionSort(List<ItemObject> list)
    {
        int n = list.Count;

        for (int i = 1; i < n; i++)
        {
            ItemObject key = list[i];
            int j = i - 1;

            if (sort_order == SortOrder.Ascending)
            { 
                while (j >= 0 && items[j].rarity > key.rarity)
                {
                    list[j + 1] = list[j];
                    j = j - 1;
                }
                list[j + 1] = key;
            }
            else if (sort_order == SortOrder.Descending)
                {
                while (j >= 0 && items[j].rarity < key.rarity)
                {
                    list[j + 1] = list[j];
                    j = j - 1;
                }
                list[j + 1] = key;
            }
        }
    }
    void toggleSortOrder()
    {
        switch (sort_order)
        {
            case SortOrder.Ascending:
                sort_order = SortOrder.Descending;
                break;
            case SortOrder.Descending:
                sort_order = SortOrder.Ascending;
                break;
        }
    }
}
