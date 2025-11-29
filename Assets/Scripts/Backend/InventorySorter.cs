using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Tilemaps.TilemapRenderer;

public static class InventorySorter
{
    public static void bubbleSort(List<ItemObject> list, SortOrder sort_order)
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

    public static void insertionSort(List<ItemObject> list, SortOrder sort_order)
    {
        int n = list.Count;


        for (int i = 1; i < n; i++)
        {
            ItemObject key = list[i];
            int j = i - 1;

            if (sort_order == SortOrder.Ascending)
            {
                while (j >= 0 && list[j].rarity > key.rarity)
                {
                    list[j + 1] = list[j];
                    j = j - 1;
                }
                list[j + 1] = key;
            }
            else if (sort_order == SortOrder.Descending)
            {
                while (j >= 0 && list[j].rarity < key.rarity)
                {
                    list[j + 1] = list[j];
                    j = j - 1;
                }
                list[j + 1] = key;
            }
        }
    }


}
