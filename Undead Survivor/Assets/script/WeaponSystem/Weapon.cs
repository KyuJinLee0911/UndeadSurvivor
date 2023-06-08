using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;

    // 개수랑 관통력으로 세분화 할 수는 없을까? -> 일단은 그러면 LevelUp 함수에 문제가 생김
    public int count; // 개수 혹은 관통력

    // 연사속도랑 회전속도가 똑같이 speed 변수를 쓰던걸 나눴는데 혹시 문제가 있는지 추후에 개발하면서 계속 체크할 것
    // 어차피 스크립트 내에서만 쓸거같아서 public -> private으로 바꿨음
    // 기어에서 문제가 생겨서 다시 public float speed로 바꿈

    public float speed; // 총알 연사속도 혹은 근접무기 회전 속도

    private float timer;
    private Player player;

    private void Awake()
    {
        player = GameManager.Instance().player;

    }

    void Update()
    {
        if(!GameManager.Instance().isAlive)
            return;
            
        switch (id)
        {
            case 0: // 근접무기
                // 플레이어를 중심으로 회전하도록
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;

            default:


                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }

    public void LevelUp(float _damage, int _count)
    {
        damage = _damage *  Character.Damage;
        count += _count;

        switch (id)
        {
            case 0:
                ArrangeMelee();
                break;

            case 1:
                break;

            default:
                break;
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // 기본 세팅
        name = $"Weapon {data.itemId}";
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

        for (int index = 0; index < GameManager.Instance().poolManager.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.Instance().poolManager.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }
        // 프로퍼티 세팅
        switch (id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed; // 회전속도
                ArrangeMelee();
                break;

            default:
                speed = 0.3f * Character.WeaponRate; // 연사속도
                break;
        }

        // Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriteRenderer.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    // 근접무기의 초기 배치 관련된 함수
    void ArrangeMelee()
    {
        for (int index = 0; index < count; index++)
        {
            Transform _bulletTransform;

            if (index < transform.childCount)
            {
                _bulletTransform = transform.GetChild(index); // 오브젝트 풀에 있으면 일단 있는거 먼저 사용
            }
            else
            {
                _bulletTransform = GameManager.Instance().poolManager.Get(prefabId).transform; // 없으면 새로 생성
                _bulletTransform.parent = transform;
            }

            // 근접무기 생성시 위치 초기화 -> 플레이어가 계속해서 이동하므로, 기존 위치에 생성되는 것을 막기 위함
            _bulletTransform.localPosition = Vector3.zero;
            _bulletTransform.localRotation = Quaternion.identity;

            // 몇 번째 근접무기냐에 따라 생성 위치가 달라지게 함
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            _bulletTransform.Rotate(rotVec);
            _bulletTransform.Translate(_bulletTransform.up * 1.5f, Space.World);
            // 근접무기 생성 -> 근접무기는 내구도가 없음(관통력이 없음). 따라서 관통력 -1, 발사 방향이 따로 없으므로 Vector3.zero
            _bulletTransform.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is infinity penetration


        }
    }

    // 총알의 발사와 관련된 함수
    void Fire()
    {
        // 몬스터가 없으면 리턴
        if (!player.scanner.nearestTarget)
            return;

        // 타겟은 가장 가까운 몬스터
        Vector3 _targetPos = player.scanner.nearestTarget.position;
        Vector3 _dir = (_targetPos - transform.position).normalized;

        // 오브젝트 풀을 통해 총알 생성
        Transform _bulletTransform = GameManager.Instance().poolManager.Get(prefabId).transform;
        _bulletTransform.position = transform.position;
        _bulletTransform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

        // 총알 생성 -> 방향은 가장 가까운 몬스터를 향해, 여기서 count는 관통력
        _bulletTransform.GetComponent<Bullet>().Init(damage, count, _dir);
    }
}
