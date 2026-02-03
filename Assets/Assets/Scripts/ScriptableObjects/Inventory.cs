using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemInfo ItemInfo;


    void Start()
    {
        Debug.Log($"Name: {ItemInfo.Name}");
        Debug.Log($"Discartable: {ItemInfo.isDiscardable}");
        Debug.Log($"Stackable: {ItemInfo.Stackable}");
        Debug.Log($"Description: {ItemInfo.description}");
    }
}
