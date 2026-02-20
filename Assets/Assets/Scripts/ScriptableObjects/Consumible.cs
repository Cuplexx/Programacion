using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Consumible")]
public class Consumible : ItemInfo
{
    public int HealthAmount = 0;
    public float MoveSpeedAmount = 0;
    public float Duration = 0;

    //Sobreescribir la funcion de Use de la clase base para que gaste este objeto al usuario
    public override void Use()
    {
        Inventory.Instance.RemoveItem(this);
    }
}
