using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 0;
    [SerializeField] private GameObject door;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == true)
        {
            //Tween para rotar el obejto hacia -116.364 en el eje Y
            //Con EaseOutBack se cambiala curva de anmimacion que usa
            //Con setOnComplete se le añade una funecion para que la llame cuando termine el Tween
            door.LeanRotateY(-116.364f, 2f).setEaseOutBack().setOnComplete(()=> SceneManager.LoadScene(sceneIndex));
            SceneTransitions.Singleton.FadeIn();
        }
    }
}
