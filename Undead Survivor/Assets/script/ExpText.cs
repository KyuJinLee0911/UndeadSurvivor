using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpText : MonoBehaviour
{
    Text expText;
    

    private void Awake() 
    {
        expText = GetComponent<Text>();
    }
    private void LateUpdate() 
    {
        expText.text = $"{GameManager.Instance().exp} / {GameManager.Instance().levelUpExp[GameManager.Instance().level]} (exp)";
    }
}
