using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int penetration;
    public float bulletSpeed;

    Rigidbody2D bulletRB;

    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
    }

    // 대미지, 관통력, 방향을 변수로 받는 함수 => 생성된 총알이나 근접무기의 대미지와 관통력, 투사체의 경우 발사 방향을 설정해 줌
    public void Init(float _damage, int _penetration, Vector3 dir)
    {
        damage = _damage;
        penetration = _penetration;

        // 관통력이 -1보다 클 경우, 총알의 속력은 방향 * 총알속도
        if (penetration > -1)
        {
            bulletRB.velocity = dir * bulletSpeed;
        }
    }

    // 투사체가 몬스터에 맞을 때 관통력을 1씩 감소시키고, -1에 도달했을 때 비활성화
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy") || penetration == -1)
            return;

        penetration--;

        if (penetration == -1)
        {
            bulletRB.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }


}
