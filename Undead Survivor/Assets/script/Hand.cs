using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriteRenderer;

    SpriteRenderer playerSprite;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 reverseRightPos = new Vector3(0.15f, -0.15f, 0);
    Quaternion leftRotation = Quaternion.Euler(0,0,-35);
    Quaternion reverseLeftRotation = Quaternion.Euler(0,0,-135);

    private void Awake()
    {
        playerSprite = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate() 
    {
        bool isReverse = playerSprite.flipX;

        if(isLeft) // 근접무기
        {
            transform.localRotation = isReverse ? reverseLeftRotation : leftRotation;
            spriteRenderer.flipY = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 4 : 6;
        }
        else // 원거리 무기
        {
            transform.localPosition = isReverse ? reverseRightPos : rightPos ;
            spriteRenderer.flipX = isReverse; 
            spriteRenderer.sortingOrder = isReverse ? 6 : 4;
        }
    }


}
