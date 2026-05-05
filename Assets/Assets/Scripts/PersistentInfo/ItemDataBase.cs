using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase
{
    public static Dictionary<string, ItemInfo> allItems;

    //Añadir que se llame automaticamente al empezar el juego
    //El parametro dentro de los parentesis sirve para que se llame antes que Awake y así carga la lista de objetos antes que todo lo demás
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    //Busca en el proyecto todos los objetos que haya e inicializa el diccionario con todos ellos
    void GetItems()
    {
        allItems = new Dictionary<string, ItemInfo>();
        //Busca en la carpeta de Items todos los ItemInfo que hayamos guardado dentro de la carpeta llamada Resources
        ItemInfo[] foundItems = Resources.LoadAll<ItemInfo>("Items");
        //Por cada objeto cargado, hay que añadirlo al diccionario junto a su nombre para indentificarlo
        foreach (ItemInfo foundItem in foundItems)
        {
            allItems.Add(foundItem.name, foundItem);
        }
    }

    public static ItemInfo FindItem(string itemName)
    {
        return allItems[itemName];
    }
}
