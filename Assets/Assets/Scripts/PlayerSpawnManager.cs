using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SpawnPoint
{
    public string id;
    public Transform point;
}
public class PlayerSpawnManager : MonoBehaviour
{
    //Lista con todos los puntos de Spawn de una única escena
    [SerializeField] private List<SpawnPoint> spawnPoints;
    [SerializeField] private string spawnID;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        if (PersistentInfo.Singleton.gameStarted)
        {
            player.position = PersistentInfo.Singleton.initialSpawnPosition;
            PersistentInfo.Singleton.gameStarted = false;
            return;
        }

        //Guardar el valor de la ID de Spawn asignada en Persistent info
        spawnID = PersistentInfo.Singleton.currentSpawnPointID;

        //Buscar el punto con la ID guardada
        Transform spawnPoint = GetSpawnPoint(spawnID);

        if (spawnPoint != null)
        {
            //Modificar posicion y rotación del player
            player.position = spawnPoint.position;
            player.rotation = spawnPoint.rotation;
        }

    }

    Transform GetSpawnPoint(string idToGet)
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnPoints[i].id == idToGet)
            {
                return spawnPoints[i].point;
            }
        }
        return null;
    }

    private void OnApplicationQuit()
    {
        //Quitar todas las funciones del callback de guardado para que de tiempo a guardar la info de la escena
        SaveManager.OnDataSaved = null;

        //Añadir al callback la función de guardar info de la escena
        SaveManager.OnDataSaved += SaveSceneInfo;

        SaveManager.Save();

        void SaveSceneInfo(SaveData saveData)
        {
            //Se actualiza la escena guardada con el nombre de la escena actual y la posición en la que se encuentre el player
            saveData.sceneSaveData = new SceneSaveData(SceneManager.GetActiveScene().name, player.position);
        }
    }
}