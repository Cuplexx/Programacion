using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public ItemInfo ItemInfo;

    //Aquí se guardan todos los objetos que tengaoms y su cantidad.
    public Dictionary<string, uint> items = new Dictionary<string, uint>();

    //Callback que se ejecuta cuando se añada el objeto
    //Pasa como parametro la info del objeto añadido
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
            if(item.Stackable == true)
            {
                //Accedemos al valor a través del nombre del objetos
                //Como el nombre es la Key, se usa para acceder a cada objeto por separado
                items[item.Name] += 1;
            }
            //Si el objeto NO se puede stackear, lo añade de nuevo al diccionario.
            else
            {
                items.Add(ItemInfo.Name, 1);
            }
        }
        //Ejecutar el callback que se ha añadido un objeto, pasando su infomacion
        //El operador ? comprueba que haya algo en el callback para ejecutarlo.
        onAddedItem?.Invoke(item);
    }
}
