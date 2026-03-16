using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Sistema para porder usar el sistema de Pooling de Unity
using UnityEngine.Pool;

public class PlayerShoot : MonoBehaviour
{
    //El prefab que utilizara para crear los objetos del pool
    [SerializeField] private Bolt boltPrefab;

    [SerializeField] private Transform shootOrigin;

    [SerializeField] private float shootForce = 6;
    //El pool de proyectiles
    private ObjectPool<Bolt> boltPool;

    void Start()
    {
        boltPool = new ObjectPool<Bolt>(CreateBolt, GetBolt, ReleaseBolt);
    }

    //Esta funcion se llama al crear el pool tantas veces como objetos pueda tener
    //Por ejemplo, se se especifica un tamaño de 20 para el pool, llama a la funcion 20 veces

    private Bolt CreateBolt()
    {
        //Crear un nuevo proyectil
        Bolt bolt = Instantiate(boltPrefab);
        //Asignar el pool del proyectil
        bolt.pool = boltPool;
        //Desactivar el proyectil para que esté oculto
        bolt.gameObject.SetActive(false);
        return bolt;
    }

    //Se llama cada vez que se coja un proyectil del pool
    private void GetBolt(Bolt bolt)
    {
        //Al sacar un objeto del pool, lo principal es activarlo
        bolt.gameObject.SetActive(true);
        //Mover el proyecttil al punto de origen de disparo
        bolt.transform.position = shootOrigin.position;
        //Añadir fuerza al proyectil
        bolt.Shoot(shootOrigin.forward * shootForce);
    }

    //Se llama cada vez que un proyectil vuelva al pool
    private void ReleaseBolt(Bolt bolt)
    {
        //Reiniciar sus valores de velocidad
        bolt.ResetVelocity();
        //Desactivar el objeto al devolverlo al pool
        bolt.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("dispara puto");
            //Coger un proyectil de los que haya en el pool
            boltPool.Get();
        }
    }
}
