using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InvUI : MonoBehaviour
{
    public Inventory inventory;
    public List<GameObject> inventoryUIButtons = new List<GameObject>();

    private void OnEnable()
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        //Debug.Log("Refresh Inventory UI");

        // function that turns all buttons off
        foreach (GameObject i in inventoryUIButtons)
        {
            i.SetActive(false);
        }

        for (int i = 0; i < inventory.items.Count; i++)
        {
            // check that the items index is not greater than the number of buttons
            if ( i < inventoryUIButtons.Count )
            {
                //create a reference to the UI button and Item
                InvButtonUI uiButton = inventoryUIButtons[i].GetComponent<InvButtonUI>();
                ItemObject item = inventory.items[i];

                // make sure button is visible and update it with the item information
                uiButton.gameObject.SetActive(true);
                uiButton.SetButton(item);
            }
        }
    }

    public void OnInventoryUIButton(int i)
    {
        inventory.removeItem(i);
        RefreshInventory();
    }
}
