using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemsInfo")] //El / hace que se cree un submenu
public class ItemInfo : ScriptableObject
{
    public new string Name;
    public bool Stackable;
    public string description; //Si se puede apilar el objeto o no
    public bool isDiscardable; //Si es de un solo uso
    public Sprite Icon; //El icono que se ve dentro del inventario.+
    public ItemType type; //Tipo de obejto que es

    //Para que todos los obejtos tengan una funcion por defecto de
    //usar usamos la palabra clave VIRTUAL para que se pueda sobreescribir esta funcion desde las calses que heredan
    public virtual void Use()
    {
        if(type == ItemType.Consumible)
        {
            Inventory.Instance.RemoveItem(this);
        }
    }
}

public enum ItemType
{
    Consumible, Equipable, Junk
}
