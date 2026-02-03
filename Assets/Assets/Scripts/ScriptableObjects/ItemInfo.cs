using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemsInfo")] //El / hace que se cree un submenu
public class ItemInfo : ScriptableObject
{
    public new string Name;
    public bool Stackable;
    public string description;
    public bool isDiscardable;

}
