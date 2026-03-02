using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    [SerializeField] private Material openMat;
    //el identificador que lo diferencia del resto de cofres
    [SerializeField] private uint chestID;

    void Start()
    {
        //Comprueba en la lista de cofres abiertos si este cofre lo está
        if (PersistentInfo.Singleton.IsChestOpen(chestID))
        {
            SetOpen();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && isOpen == false)
        {
            Open();
        }
    }

    void Open()
    {
        isOpen = true;
        GetComponent<Renderer>().material = openMat;
        //Al abrirse, el cofre se añade a la lista de cofres ya abiertos
        PersistentInfo.Singleton.AddOpenChest(chestID);
    }

    //âra solo marcar el cofre como que esta abierto pero sin hacer nada más
    void SetOpen()
    {
        isOpen = true;
        GetComponent<Renderer>().material = openMat;

    }
}
