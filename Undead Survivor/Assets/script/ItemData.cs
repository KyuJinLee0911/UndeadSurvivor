using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Melee, Range, Glove, Shoe, Useable
    }

    [Header("Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDisc; // Discription of item
    public Sprite itemIcon;

    [Header("Level Data")]
    public float baseDamage;
    public int baseCount; // 0레벨일 때의 관통력(원거리)/개수(근접)
    public float[] damages;
    public int[] counts;

    [Header("Weapon Data")]
    public GameObject projectile;
    public Sprite hand;
    
}
