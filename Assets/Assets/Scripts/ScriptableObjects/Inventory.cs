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
    public UnityAction<ItemInfo, uint> onAddedItem;
    //Callback que se ejectua cuando se elimine un objeto
    public UnityAction<ItemInfo, uint> onRemovedItem;

    //Crear una instancia pbulica para este script
    public static Inventory Instance;

    

    private void Awake()
    {
        Instance = this;
        //Añadir funcion al callback de cargar info
        SaveManager.OnLoadedData += LoadItems;
    }

    void Start()
    {
        SaveManager.OnSaveData += SaveItems;
    }

    //private void Update()
    //{
    //    foreach(var item in items)
    //    {
    //        Debug.Log($"{item.Key} Quantity: {item.Value}");
    //    }
    //}

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
        onAddedItem?.Invoke(item, items[item.Name]);
        //Llamar a la funcion de guardar
        SaveManager.Save();
    }
    public void RemoveItem(ItemInfo item)
    {
        //Si el objeto no estuviera en el inventario no hace nada.
        if (items.ContainsKey(item.Name) == false)
        {
            return;
        }
        //Para indicar si al finaol de la función hay que eliminar el objeto del diccionario
        bool removeItem = false;
        //Si el objeto está en el inventario, hay que quitarlo
        
        //Si el objeto se puede stackear, se resta 1 a la cantidad que tengamos
        if (item.Stackable == true)
        {
            //Accedemos al valor a través del nombre del objetos
            //Como el nombre es la Key, se usa para acceder a cada objeto por separado
            items[item.Name] -= 1;
            //En cuanto se gasta hay que comprobar si aun nos quedan objetos de ese tipo
            //si no quedan se elimina del inventario
            if (items[item.Name] <= 0)
            {
                removeItem = true;
            }
        }
        //Si el objeto NO se puede stackear, lo añade de nuevo al diccionario.
        else
        {
            removeItem = true;
            //Forzar a que la cantidad del objeto sea 0
            items[item.Name] = 0;
        }
        onRemovedItem?.Invoke(item, items[item.Name]);
        //Se comprueba si hay que eliminar el obejto del inventario o no
        if(removeItem == true)
        {
            items.Remove(item.Name);
        }
    }

    //Devuelve true o false en funcion de si se tiene el objeto especificado o no
    public bool HasItem(ItemInfo itemToFind)
    {
        return items.ContainsKey(itemToFind.Name);
    }

    void SaveItems(SaveData saveData)
    {
        //Crear lista de objetos a guardar
        List<ItemSaveData> itemsToSave = new List<ItemSaveData>();
        //Por cada objeto que haya en el inventario, se crea un objeto de info
        foreach (var item in items)
        {
            ItemSaveData itemData = new ItemSaveData(item.Key, item.Value);
            itemsToSave.Add(itemData);
        }
        //Hay que guardar la lista creada en los datos de guardado
        //La guardamos como una copia, no se iguala directamente
        saveData.items = new List<ItemSaveData>(itemsToSave);
    }

    void LoadItems(SaveData loadedData)
    {
        //Por cada objeto guardado en la lista, creamos y añadimos uno nuevo al diccionario
        foreach(var item in loadedData.items)
        {
            items.Add(item.name, item.amount);
            Debug.Log($"Added { item.name} { item.amount}");
            //Buscar el ScriptableObject con este nombre
            ItemInfo itemInfo = ItemDataBase.FindItem(item.name);
            //Llamar al callback de objeto añadido con el objeto cargado
            onAddedItem?.Invoke(itemInfo, item.amount);

        }
    }
}
