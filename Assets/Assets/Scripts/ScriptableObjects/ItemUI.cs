using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    //El obejto asociado a este objeto de la UI del inventario
    [SerializeField] private ItemInfo item;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text text;

    public void SetItem(ItemInfo info)
    {
        ItemInfo = info;
        icon.sprite = itemInfo.Icon;
        if(info.Stackable == false)
        {
            text.gameObject.SetActive(false);
        }
    }

}
