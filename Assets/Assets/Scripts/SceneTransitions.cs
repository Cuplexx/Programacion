using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
    public static SceneTransitions Singleton;
    private void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    [SerializeField] private CanvasGroup fadeImage;

    void Start()
    {
         
    }

    public void FadeIn()
    {
        //Inicializar el alfa a 0 por si acaso
        fadeImage.alpha = 0;
        //Activar la imagen y hacer un Tween para aumentar su alfa a 1
        fadeImage.LeanAlpha(1f, 0.5f);
        fadeImage.gameObject.SetActive(true);
    }
    public void FadeOut()
    {
        //Inicializar el alfa a 0 por si acaso.
        fadeImage.alpha = 1;
        fadeImage.LeanAlpha(0f, 0.5f).setOnComplete(() => fadeImage.gameObject.SetActive(false));
    }
}
