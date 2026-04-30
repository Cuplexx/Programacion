using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Para poder crear y leer archivos
using System.IO;
using UnityEngine.Events;

//Si se va a guardar con Json, hay que marcar la estructura como serializable
//Usamos una class para poder modificar su valor desde distintos scripts y que se guarde
[System.Serializable]
public class SaveData
{   
    //Lista de cofres ya abiertos
    public List<uint> openChestsIDs;
    //Inventario: lista de informacion de los objetos que tengamos
    public List<ItemSaveData> items;

    public SceneInfo sceneInfo;

}

//Como no se pueden serializar los diccionarios usamos esta estructura para guardar en una lista la infor de los objetos: nombre y cantidad
[System.Serializable]
public class ItemSaveData
{
    public string name;
    public uint amount;

    //Metodo constructor
    public ItemSaveData(string _name, uint _amount)
    {
        name = _name;
        amount = _amount;
    }
}

[System.Serializable]
public struct SceneInfo
{
    public string name;
    public Vector3 lastPosition;

    public SceneInfo(string _name, Vector3 _lastPosition)
    {
        name = _name;
        lastPosition = _lastPosition;
    }

}

public class SaveManager
{
    static string fileName = "ReadMe.younaiti";
    static SaveData saveData = new SaveData();

    //Callback que se llama cuando carga la informacion
    public static UnityAction<SaveData> OnLoadedData;
    //Callback para guardar la informacion
    public static UnityAction<SaveData> OnSaveData;

    public static void Save()
    {
        //Callback para que todos los objetos guarden su informacion en el SaveData
        OnSaveData?.Invoke(saveData);
        //Transformar el SaveData en una string con formato Json
        string dataJson = JsonUtility.ToJson(saveData);
        //Generar la ruta del archivo con persistentDataPath y el nombre que queramos
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        //Encriptar la informacion en formato Json
        dataJson = XOREncryption.EncryptDecrypt(dataJson);
        //Crear el archivo de guardado en una ruta con un nombre y los datos Json
        File.WriteAllText(filePath, dataJson);
    }

    //Esto es para que Unity llame a esta funcion cuando se inicie la escena, como si fuera un start
    //Por defecto, se llama después de awake
    [RuntimeInitializeOnLoadMethod]

    public static void Load()
    {
        //Generar la ruta del archivo con PersistentDataPath y el nombre que queremos
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        //Si no hay informacion guardada no carga ningun dato.
        if(File.Exists(filePath) == false)
        {
            return;
        }
        //Leer los archivos de guardado en formato Json
        string dataJson = File.ReadAllText(filePath);
        //Encriptar la informacion en formato Json
        dataJson = XOREncryption.EncryptDecrypt(dataJson);
        //Transformar los datos en formato Json en una struct SaveData
        SaveData saveData = JsonUtility.FromJson<SaveData>(dataJson);
        //Una ve esta todo cargado se llama al callback pasando esta informacion
        OnLoadedData?.Invoke(saveData);
    }
}
