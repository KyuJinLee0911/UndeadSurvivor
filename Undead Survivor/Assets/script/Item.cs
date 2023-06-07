using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    public int itemLevel;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = itemData.itemIcon;
        textLevel = GetComponentInChildren<Text>();

        Text[] texts = GetComponentsInChildren<Text>();

        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        textLevel.text = string.Format("Lv.{0:D2}", (itemLevel + 1));
    }

    public void OnClick()
    {
        switch (itemData.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (itemLevel == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(itemData);
                }
                else
                {
                    float nextDamage = itemData.baseDamage;
                    int nextCount = 0;

                    //대미지 계수와 관통력은 나중에 수정해도 됨
                    nextDamage += itemData.baseDamage * itemData.damages[itemLevel];
                    nextCount += itemData.counts[itemLevel];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                itemLevel++;
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (itemLevel == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(itemData);
                }
                else
                {
                    float nextRate = itemData.damages[itemLevel];
                    gear.LevelUp(nextRate);
                }
                itemLevel++;
                break;

            case ItemData.ItemType.Useable:
                GameManager.Instance().hp = GameManager.Instance().maxHp;
                break;
        }



        if (itemLevel == itemData.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}