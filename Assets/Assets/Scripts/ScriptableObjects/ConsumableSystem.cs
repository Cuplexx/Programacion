using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConsumableSystem : MonoBehaviour
{
    //Todos los consumibles que queramos llevar equipados
    [SerializeField] private List<ConsumibleSlot> slots;

    public static UnityAction<ItemInfo> onConsumibleUsed;

    private void Update()
    {
        //Comprobar si se ha pulsado la tecla de alguno de los slots
        for(int i = 0; i < slots.Count; i++)
        {
            if(Input.GetKeyDown(slots[i].key))
            {
                slots[i].Use();
                onConsumibleUsed?.Invoke(slots[i].consumible);
            }
        }
    }
}

[System.Serializable]
public struct ConsumibleSlot
{
    public ItemInfo consumible;
    public KeyCode key;

    public void AssignConsumible(ItemInfo item)
    {
        consumible = item;
    }

    public void Use()
    {
        Debug.Log($"Used slot with item {consumible.Name}");
        consumible.Use();
    }
}
