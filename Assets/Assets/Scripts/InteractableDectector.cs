using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDectector : MonoBehaviour
{
    //El objeto interactuable del tipo que se tiene actualmente el rango
    private Interactable currentInteractable;


    private void Update()
    {
        //Al darle a la tecla de interactuar con un objeto guardado, se interactua con el
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            //Llamar a la funcion Interact
            currentInteractable.Interact();
            //Quitar la referencia par aque no se pueda interactuar más de una vez
            currentInteractable = null;
        }
    }

    //Está configurado con layers para que solo pueda detectar interactuables
    private void OnTriggerEnter(Collider other)
    {
        //Guardar la interfaz interactable que tenga ese objeto
        currentInteractable = other.GetComponent<Interactable>();
    }

    private void OnTriggerExit(Collider other)
    {
        currentInteractable = null;
    }
}
