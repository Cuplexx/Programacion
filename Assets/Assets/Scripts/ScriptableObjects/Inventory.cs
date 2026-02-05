using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public ItemInfo ItemInfo;

    //Aquí se guardan todos los objetos que tengaoms y su cantidad.
    public Dictionary<string, uint> items = new Dictionary<string, uint>();

    public UnityEvent<ItemInfo> onAddItem;
    public UnityAction<ItemInfo> onAddedItem;



    //Crear una instancia pbulica para este script
    public static Inventory Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Log($"Name: {ItemInfo.Name}");
        Debug.Log($"Discartable: {ItemInfo.isDiscardable}");
        Debug.Log($"Stackable: {ItemInfo.Stackable}");
        Debug.Log($"Description: {ItemInfo.description}");
    }

    private void Update()
    {
        foreach(var item in items)
        {
            Debug.Log($"{item.Key} Quantity: {item.Value}");
        }
    }

    public void AddItem(ItemInfo item)
    {
        //Si el objeto no está en el inventario, lo añade y ya
        if(items.ContainsKey(item.Name) == false)
        {
            items.Add(item.Name, 1);
        }
        //si el objeto ya esta en el inventario...
        else
        {
            //Si el objeto se puede stackear, tiene que añadir 1 a la cantidad que tengamos.
            if(ItemInfo.Stackable == true)
            {
                //Accedemos al valor a través del nombre del objetos
                //Como el nombre es la Key, se usa para acceder a cada objeto por separado
                items[ItemInfo.Name] += 1;
            }
            //Si el objeto NO se puede stackear, lo añade de nuevo al diccionario.
            else
            {
                items.Add(ItemInfo.Name, 1);
            }
        }
    }
}
