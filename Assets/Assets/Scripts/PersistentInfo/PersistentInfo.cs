using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentInfo : MonoBehaviour
{
    public static PersistentInfo Singleton;

    [SerializeField] private List<uint> openChests = new List<uint>();

    private void Awake()
    {
        //Cuando no hay nadie como Singleton, se asigna y se marca para que no se destruya
        if(Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        //Si al iniciar ya hay un Singleton, este objeto debe destruirse para que no haya duplicados
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        //Añadir una funcion al callback de datos cargados
        //Este codigo tan feo es una funcion anonima. Es como una funcion normal pero se crea en el momento de añadirla al callback
        //Entre los parentesis hay que añadir un SaveData porque el callback lo usa como parametro.
        SaveManager.OnLoadedData += (SaveData saveData) =>
        {
            openChests = new List<uint>(saveData.openChestsIDs);
        };
        //Llamar a la funcion de cargar datos
        SaveManager.Load();
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
            SaveManager.Save(openChests);
        }
    }

    public bool IsChestOpen(uint chestID)
    {
        return openChests.Contains(chestID);
    }
}
