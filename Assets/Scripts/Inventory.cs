using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();
    public GameManager manager;

    public void addItem(string item_name)
    {
        if(manager.current_game_state == GameManager.GameState.GAMEPLAY)
        {
            items.Add(item_name);
        }
        
        
    }

    public void removeItem(string item_name)
    {
        if (manager.current_game_state == GameManager.GameState.GAMEPLAY)
        {
            items.Remove(item_name);
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
            addItem(collisionItem.name);
            Destroy(collisionItem.gameObject);
            //items.Sort();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            addItem("manually_added_item");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            bubbleSort(items);
        }


    }

    void bubbleSort(List<string> list)
    {
        int n = list.Count;
        string temp;
        bool swapped;
        for (int i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (int j = 0; j < n - 1; j++)
            {
                if (string.Compare(list[j], list[j + 1]) > 0)
                {
                    temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                    swapped = true; 

                }
            }
            if (swapped == false)
            {
                break;
            }
        }
    }
}
