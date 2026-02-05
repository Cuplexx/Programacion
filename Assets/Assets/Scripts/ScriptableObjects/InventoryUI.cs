using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image itemPrefab;
    [SerializeField] private Transform itemLayout; //Todos los objetos se emparentan aquí

    void Start()
    {
        
    }

    public void CreateItem(ItemInfo itemInfo)
    {
        //Crear una nueva imagen y emparentarla al Layout para que lo ponga en su posición
        Image newItem = Instantiate(itemPrefab, itemLayout);
        //Cambiar el sprite de la imagen al icono del objeto
        newItem.sprite = itemInfo.Icon;
    }
}
