using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemUI itemPrefab;
    [SerializeField] private Transform itemLayout; //todos los objetos se emparentan aqui
    private List<ItemUI> items = new List<ItemUI>();

    //public ItemInfo item;
    void Start()
    {
        //el += es meterlo en la caja
        //añadir la funcion CreateItem al callbacl del inventario cuando se añade un objeto
        //importante que la funcion reciba un ItemIndo como parametro o llora muy fuerte
        Inventory.Instance.onAddedItem += CreateItem;
        //Añadir la funcion DeleteItem al callback del inventario cuando se usa un objeto
        Inventory.Instance.onRemovedItem += DeleteItem;
    }

    public void CreateItem(ItemInfo itemInfo, uint amount)
    {
        //Buscar si el objeto ya está en el inventario
        ItemUI duplicateItem = FindItem(itemInfo);
        //Si hay un duplicado se actualiza la cantidad del objeto
        //Si no hay duplicado se crea un objeto nuevo
        if (duplicateItem == null)
        {
            Transform slot = null;
            //buscar en todos los objetos hijos del layout (huecos)
            for (int i = 0; i < itemLayout.childCount; i++)
            {
                //si el huevo no tien eobjetos hijo, significa que esta vacío
                if (itemLayout.GetChild(i).childCount == 0)
                {
                    //se asigna al hueco vacío y se sale del bucle
                    slot = itemLayout.GetChild(i);
                    break;
                }
            }
            //crear una nueva imagen y emparentarla al Layout para que lo ponga en su posicion
            ItemUI newItem = Instantiate(itemPrefab, slot);
            //asignar al objeto de la UI su objeto al que hace referencia
            newItem.SetItem(itemInfo);
            //Añadir el objeto a la lista
            items.Add(newItem);
        }
        //Si hay un duplicado, se actualiza la cantidad del objeto
        else
        {
            duplicateItem.UpdateAmount(amount);
        }
    }
    private ItemUI FindItem(ItemInfo infoToFind)
    {
        //Buscamos en todos los objetos el que coincida con la info que buscamos
        foreach(ItemUI item in items)
        {
            //Si lo encuentra, lo devuelve
            if(item.itemInfo == infoToFind)
            {
                return item;
            }
        }
        //Si no encuentra objeto que coincida, devuelve NULL
        return null;
    }

    void DeleteItem(ItemInfo item, uint amount)
    {
        //Buscamos el objeto que gastar o eliminar
        ItemUI itemToDelete = FindItem(item);
        //Si queda al menos un objeto, se actualiza la cantidad
        if(amount > 0)
        {
            itemToDelete.UpdateAmount(amount);
        }
        //Si hay 0 de cantidad, el objeto se ha gastado y hay que borrarlo
        else
        {
            items.Remove(itemToDelete);
            Destroy(itemToDelete.gameObject);
        }
    }

}