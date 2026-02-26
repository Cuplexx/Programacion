using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackHitbox : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other)
    {
        //Accede a la interfaz IDamageable y ejecuta su funcion TakeDamage
        other.GetComponent<IDamageable>().TakeDamage(damage);
    }
}
