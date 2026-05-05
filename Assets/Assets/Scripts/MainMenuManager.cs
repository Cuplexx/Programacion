using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Vector3 lastPlayerPosition;

    void Awake()
    {
        SaveManager.OnDataLoaded += LoadSceneInfo;

        void LoadSceneInfo(SaveData saveData)
        {
            sceneToLoad = saveData.sceneSaveData.name;
            lastPlayerPosition = saveData.sceneSaveData.lastPosition;
        }
    }

    public void Play()
    {
        //Guardar la última posición como posición de spawn inicial
        PersistentInfo.Singleton.initialSpawnPosition = lastPlayerPosition;

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }

}