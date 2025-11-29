using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.Tilemaps.TilemapRenderer;

public enum SortOrder
{
    Ascending,
    Descending
}


public class Inventory : MonoBehaviour
{
    public List<ItemObject> items = new List<ItemObject>();
   
    GameManager gameManager;
    Transform worldItemsTransform;
    public InvUI InvUI;

    SortOrder sort_order = SortOrder.Ascending;
    private int last_sort = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // find the game gameManager and reference it
        gameManager = FindAnyObjectByType<GameManager>();

        worldItemsTransform = GameObject.Find("WorldItems").transform;
    }

    public void addItem(ItemObject item)
    {
        if(gameManager.current_game_state == GameManager.GameState.GAMEPLAY)
        {
            items.Add(item);
        }
        
        
    }

    public void removeItem(ItemObject item)
    {

        // world spawn position/rotation
        Vector3 currentPosition = transform.position;
        Vector3 forward = transform.forward;
        Vector3 newPosition = currentPosition + forward + new Vector3(0, 1, 0);
        Quaternion currentRotation = transform.rotation * Quaternion.Euler(0, 0, 180);

        // Instantiate a copy under the WorldItems parent (ensure worldItemsTransform is set in Start)
        GameObject spawned = Instantiate(item.gameObject, newPosition, currentRotation, worldItemsTransform);
        spawned.SetActive(true);

        // turn the spawned game object into an ItemObject and give it pickupable
        ItemObject spawnedItemObj = spawned.GetComponent<ItemObject>();
        if (spawnedItemObj != null)
        {
            spawnedItemObj.pickupable = true;
            // optionally reset other state you want for a world item, e.g. deactivate any "held" flags
        }
        else
        {
            Debug.LogWarning("Spawned object has no ItemObject component.");
        }

        // cleanup exisiting item
        items.Remove(item);
        if (item.gameObject != null)
        {
            Destroy(item.gameObject);
        }
    }


    public void removeItem()
    {
        if (gameManager.current_game_state == GameManager.GameState.GAMEPLAY && items.Count > 0)
        {
            // take the first item from inventory
            ItemObject item = items[0];

            removeItem(item);
        }
    }

    public void removeItem(int index)
    {
        if ( index < items.Count)
        {
            removeItem(items[index]);
        }
    }



    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ItemObject collisionItem = hit.gameObject.GetComponent<ItemObject>();

        if (collisionItem != null && collisionItem.pickupable)
        {
            Debug.Log(collisionItem.item_name);
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


    }
    public void bubbleSort()
    {
        InventorySorter.bubbleSort(items, sort_order);
        InvUI.RefreshInventory();
        last_sort = 1;
    }

    public void insertionSort()
    {
        InventorySorter.insertionSort(items, sort_order);
        InvUI.RefreshInventory();
        last_sort = 2;
    }
    public void toggleSortOrder()
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
        switch (last_sort)
        {
            case 0:
                Debug.Log("No sort selected");
                break;
            case 1:
                bubbleSort();
                break;
            case 2:
                insertionSort();
                break;
        }
    }
}
