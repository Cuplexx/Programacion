using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemInfo")]      //Para poder crear el SO haciendo Click Drcho en Assets y que esté agrupado en "Scriptable Objects"

public class ItemInfo : ScriptableObject
{
    public string itemName = "defaultName";

    public bool stackable = true;

    public Sprite itemIcon;

    [TextArea]
    public string description;

    public Rarity rarity;

    //Función de Usar para todos los objetos
    public virtual void Use()    //Al marcar una función como virtual, se puede sobreescribir desde las clases heredadas
    {
        Debug.Log($"Used Standard Item: {itemName}");
    }

    public static Color RarityToColor(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return Color.gray;

            case Rarity.Uncommon:
                return Color.green;

            case Rarity.Rare:
                return Color.blue;

            case Rarity.Epic:
                return Color.magenta;

            case Rarity.Legendary:
                return Color.yellow;

            default:
                return Color.white;
        }
    }
}

public enum Rarity
{
    Common, Uncommon, Rare, Epic, Legendary
}