using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    RectTransform rect;

    private void Awake() 
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate() 
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.Instance().player.transform.position);
    }
}
