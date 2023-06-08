using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // 캐릭터 0 = 이동 속도 증가
    public static float Speed
    {
        get { return GameManager.Instance().playerId == 0 ? 1.1f : 1f; }
    }

    // 캐릭터 1 = 근거리 무기의 회전 속도 증가
    public static float WeaponSpeed
    {
        get { return GameManager.Instance().playerId == 1 ? 1.1f : 1f; }
    }

    // 캐릭터 1 = 원거리 무기의 발사 속도 증가
    public static float WeaponRate
    {
        get { return GameManager.Instance().playerId == 1 ? 0.9f : 1f; }
    }

    // 캐릭터 2 = 대미지 증가
    public static float Damage
    {
        get { return GameManager.Instance().playerId == 2 ? 1.2f : 1f; }
    }

    // 캐릭터 3 = 관통력 증가
    public static int Count
    {
        get { return GameManager.Instance().playerId == 3 ? 1 : 0; }
    }
}
