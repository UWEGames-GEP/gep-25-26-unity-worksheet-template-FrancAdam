using TMPro;
using UnityEngine;

public class InvButtonUI : MonoBehaviour
{
    public TMP_Text text;
    
    public void SetButton(ItemObject item)
    {
        text.text = item.item_name + " (" + item.rarity + ")";
    }
}
