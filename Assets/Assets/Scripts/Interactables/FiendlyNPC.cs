using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiendlyNPC : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    //Este script, al interactuar mediante la interfaz muestra el dialogo asignado

    public void Interact()
    {
        DialogueManager.singleton.BeginDialogue(dialogue);
    }

    //DEBUGGING: Se cambiara por interactuar con el NPC
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact();
        }
    }
}
