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
    Text textName;
    Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = itemData.itemIcon;
        textLevel = GetComponentInChildren<Text>();

        Text[] texts = GetComponentsInChildren<Text>();

        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = itemData.itemName;
    }

    private void OnEnable()
    {
        textLevel.text = string.Format("Lv.{0:D2}", (itemLevel));

        switch (itemData.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(itemData.itemDesc, itemData.damages[itemLevel] * 100, itemData.counts[itemLevel]);
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(itemData.itemDesc, itemData.damages[itemLevel] * 100);
                break;

            case ItemData.ItemType.Useable:
                textDesc.text = string.Format(itemData.itemDesc);
                break;
        }

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
