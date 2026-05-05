using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerDownHandler
{
    //El objeto asociado a este objeto de la UI del inventario
    public ItemInfo itemInfo;

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountTxt;
    [SerializeField] private Image frame;

    private InventoryUI inventoryUI;
    //Asigna el objeto asociado y actualiza los elementos de la UI

    public void OnPointerDown(PointerEventData evetData)
    {
        inventoryUI.ShowItemInfo(itemInfo);
    }
    public void SetItem(ItemInfo info, InventoryUI ui)
    {
        itemInfo = info;
        //Guardar la referencia a la UI
        inventoryUI = ui;
        icon.sprite = itemInfo.itemIcon;

        if (!info.stackable)
        {
            amountTxt.gameObject.SetActive(false);
        }
        //Cambiar el color del marco segun la rareza del objeto
        frame.color = ItemInfo.RarityToColor(info.rarity);
    }

    //Actualiza el texto con la cantidad de objetos disponibles
    public void UpdateAmount(uint amount)
    {
        amountTxt.text = amount.ToString();
    }
}

internal interface IPointerDownHandler
{

}