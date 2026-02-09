using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image itemPrefab;
    [SerializeField] private Transform itemLayout; //Todos los objetos se emparentan aquí

    public ItemInfo itemInfo;

    private void Start()
    {
        //Añadir la funcion CreateItem al callback del inventario cuando se añade un objeto
        //Importante que la funcion reciba un ItemInfo como parametro, o se quejara :C
        Inventory.Instance.onAddedItem += CreateItem;
    }

    public void CreateItem(ItemInfo itemInfo)
    {
        Transform slot = null;
        //Buscar en todos los objetos hijos del layout(huevos)
        for (int i = 0; 1 < itemLayout.childCount; i++)
        {   
            //Si el hueco no tiene objetos hijo, significa que está vacío
            if(itemLayout.GetChild(i).childCount == 0)
            {
                //Se asigna el hueco vacío y se sale del bucle
                slot = itemLayout.GetChild(i);
                break;
            }
        }
        //Crear una nueva imagen y emparentarla al Layout para que lo ponga en su posición
        Image newItem = Instantiate(itemPrefab, itemLayout);
        //Cambiar el sprite de la imagen al icono del objeto
        newItem.sprite = itemInfo.Icon;
    }
}
