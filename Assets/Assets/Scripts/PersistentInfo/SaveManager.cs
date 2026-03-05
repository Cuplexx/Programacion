using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Para poder crear y leer archivos
using System.IO;
using UnityEngine.Events;

//Si se va a guardar con Json, hay que marcar la estructura como serializable
[System.Serializable]
public struct SaveData
{   
    //Lista de cofres ya abiertos
    public List<uint> openChestsIDs;
}

public class SaveManager
{
    static string fileName = "ReadMe.younaiti";

    //Callback que se llama cuando carga la informacion
    public static UnityAction<SaveData> OnLoadedData;

    public static void Save(List<uint> openChests)
    {
        //Crear unos datos de guardado nuevos
        SaveData saveData = new SaveData();
        //Asignar a los datos de guardado la lista de cofres abiertos (nueva lista con los mismos valores)
        saveData.openChestsIDs = new List<uint>(openChests);
        //Transformar el SaveData en una string con formato Json
        string dataJson = JsonUtility.ToJson(saveData);
        //Generar la ruta del archivo con persistentDataPath y el nombre que queramos
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        //Encriptar la informacion en formato Json
        dataJson = XOREncryption.EncryptDecrypt(dataJson);
        //Crear el archivo de guardado en una ruta con un nombre y los datos Json
        File.WriteAllText(filePath, dataJson);
    }

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
