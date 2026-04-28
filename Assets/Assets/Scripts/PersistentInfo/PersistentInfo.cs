using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentInfo : MonoBehaviour
{
    public static PersistentInfo Singleton;

    //Lista con las ID de todos los cofres abiertos
    [SerializeField] private List<uint> openChests = new List<uint>();
    //Guarda la ID del punto en el que se haya que spawnear en ese momento
    public string currentSpawnPointID;

    private void Awake()
    {
        //Cuando no hay nadie como Singleton, se asigna y se marca para que no se destruya
        if(Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
            //Añadir una funcion al callback de datos cargados
            //Este codigo tan feo es una funcion anonima. Es como una funcion normal pero se crea en el momento de añadirla al callback
            //Entre los parentesis hay que añadir un SaveData porque el callback lo usa como parametro.
            SaveManager.OnLoadedData += (SaveData saveData) =>
            {
                openChests = new List<uint>(saveData.openChestsIDs);
            };

        }
        //Si al iniciar ya hay un Singleton, este objeto debe destruirse para que no haya duplicados
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Añadir la funcion OnSaveData
        SaveManager.OnSaveData += Save;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F6))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void AddOpenChest(uint chestID)
    {
        //Si la ID no esta en la lista, la añade
        if(openChests.Contains(chestID) == false)
        {
            openChests.Add(chestID);
            //Guardar los cofres
            SaveManager.Save();
        }
    }

    //Se añade al callback de guardar info
    void Save(SaveData saveData)
    {
        //Actualizamos los datos de guardado con la lista de cofres abiertos
        saveData.openChestsIDs = new List<uint>(openChests);
    }

    public bool IsChestOpen(uint chestID)
    {
        //Devuelve true o false en funcion de si el cofre está en la lista de abiertos
        return openChests.Contains(chestID);
    }
}
