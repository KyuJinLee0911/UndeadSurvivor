using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    enum Achivement { UnlockMan2, UnlockWoman2 }
    Achivement[] achives;

    public GameObject uiNotice;

    WaitForSecondsRealtime wait;

    private void Awake()
    {
        achives = (Achivement[])(Enum.GetValues(typeof(Achivement)));
        wait = new WaitForSecondsRealtime(5f);

        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        foreach (Achivement achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for (int index = 0; index < lockCharacter.Length; index++)
        {
            string achiveName = achives[index].ToString();

            bool isUnlock = (PlayerPrefs.GetInt(achiveName) == 1);
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach (Achivement achive in achives)
        {
            ChechAchive(achive);
        }
    }

    void ChechAchive(Achivement achive)
    {
        bool isAchived = false;

        switch (achive)
        {
            case Achivement.UnlockMan2:
                isAchived = GameManager.Instance().killCount >= 10;
                break;

            case Achivement.UnlockWoman2:
                isAchived = GameManager.Instance().gameTime == GameManager.Instance().maxGameTIme;
                break;
        }

        if (isAchived && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for (int index = 0; index < uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive;
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }
            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        AudioManager.Instance().PlaySfx(AudioManager.Sfx.LevelUp);

        yield return wait;

        uiNotice.SetActive(false);
    }
}
