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
   
    GameManager gameManager;
    Transform worldItemsTransform;

    public SortOrder sort_order = SortOrder.Ascending;


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

    //public void removeItem()
    //{
    //    if (gameManager.current_game_state == GameManager.GameState.GAMEPLAY && items.Count > 0)
    //    {
    //        Debug.Log("overloaded function ran");

    //        //checks we can remove an item from our inventory
    //        ItemObject item = items[0];

    //        // get the properties for where we want to spawn
    //        Vector3 currentPosition = transform.position;
    //        Vector3 forward = transform.forward;

    //        Vector3 newPosition = currentPosition + forward;
    //        newPosition += new Vector3(0, 1, 0);

    //        Quaternion currentRotation = transform.rotation;
    //        Quaternion newRotation = currentRotation * Quaternion.Euler(0, 0, 180);

    //        // Instantiate a copy of the held item
    //        GameObject newItem = Instantiate(item.gameObject, newPosition, newRotation, worldItemsTransform);
    //        newItem.SetActive(true);

    //        //clean up existing item
    //        items.Remove(item);
    //        Destroy(item.gameObject);
           
    //    
    //}



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
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    addItem("manually_added_item");
        //}
        /// CHANGE THESE TO THE NEW INPUT SYSTEM
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
                    if (string.Compare(list[j].item_name, list[j + 1].item_name) > 0)
                    {
                        ItemObject temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                        swapped = true; 

                    }
                }
                else
                {
                    if (string.Compare(list[j].item_name, list[j + 1].item_name) < 0)
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
