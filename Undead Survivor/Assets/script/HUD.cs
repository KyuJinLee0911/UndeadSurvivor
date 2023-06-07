using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Exp, Level, KillCount, Time, Health
    }
    public InfoType infoType;
    Text myText;
    Slider mySlider;

    private void Awake() 
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate() 
    {
        switch(infoType)
        {
            case InfoType.Exp:
                float currentExp = GameManager.Instance().exp;
                float maxExp = GameManager.Instance().levelUpExp[Mathf.Min(GameManager.Instance().level, GameManager.Instance().levelUpExp.Length - 1)];

                float percentage = currentExp/maxExp;
                mySlider.value = percentage;
                break;

            case InfoType.Level:
                myText.text = $"Lv.{GameManager.Instance().level}";
                break;

            case InfoType.KillCount:
                myText.text = $" : {GameManager.Instance().killCount}";
                break;

            case InfoType.Time:
                float remainTime = GameManager.Instance().maxGameTIme - GameManager.Instance().gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;

            case InfoType.Health:
                mySlider.value = GameManager.Instance().hp / GameManager.Instance().maxHp;
                break;

            default:
                break;
        }
    }

}
