using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int penetration;

    public void Init(float _damage, int _penetration)
    {
        damage = _damage;
        penetration = _penetration;
    }
}
