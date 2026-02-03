using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemInfo itemInfo;
    private void Start()
    {
        Inventory.Instance.AddItem(itemInfo);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(itemInfo);
        }
    }
}
