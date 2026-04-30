using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Vector3 lastPlayerPosition;

    void Awake()
    {
        void LoadSceneInfo(SaveData saveData)
        {
            //Añadir funcion al callback de cargar datos
            SaveManager.OnLoadedData += LoadSceneInfo;

            sceneToLoad = saveData.sceneInfo.name;
            lastPlayerPosition = saveData.sceneInfo.lastPosition;
        }
    }

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
