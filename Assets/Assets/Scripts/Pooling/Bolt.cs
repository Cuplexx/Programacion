using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bolt : MonoBehaviour
{
    //El pool al que pertenece este objeto
    public ObjectPool<Bolt> pool;
    [SerializeField] private Rigidbody rb;

    public void Shoot (Vector3 force)
    {
        rb.AddForce(force, ForceMode.VelocityChange);
    }

    //Al usar el pooling, se tiene que reiniciar la velocidad del Rigidbody que se conserva cuando es desactivado

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Cuando choca contra algo, se devuelve a si mismo al pool
        pool.Release(this);
    }

}
