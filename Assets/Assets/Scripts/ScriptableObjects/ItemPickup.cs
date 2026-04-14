using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, Interactable
{
    public ItemInfo itemInfo;
    
    public void Interact()
    {
        Inventory.Instance.AddItem(itemInfo);
    }
}
