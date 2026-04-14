using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiendlyNPC : MonoBehaviour, Interactable
{
    [SerializeField] private Dialogue dialogue;

    //Este script, al interactuar mediante la interfaz muestra el dialogo asignado

    public void Interact()
    {
        DialogueManager.singleton.BeginDialogue(dialogue);
    }
}
