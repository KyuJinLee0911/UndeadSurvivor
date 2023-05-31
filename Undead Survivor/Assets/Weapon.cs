using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;

            default:

                break;
        }
    }

    public void LevelUp(float _damage, int _count)
    {
        damage = _damage;
        count += _count;

        if(id == 0)
        {
            Batch();
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150;
                Batch();
                break;

            default:

                break;
        }
    }

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform _bulletTransform;

            if(index < transform.childCount)
            {
                _bulletTransform = transform.GetChild(index);
            }
            else
            {
                _bulletTransform = GameManager.Instance().poolManager.Get(prefabId).transform;
                _bulletTransform.parent = transform;
            }

            _bulletTransform.localPosition = Vector3.zero;
            _bulletTransform.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            _bulletTransform.Rotate(rotVec);
            _bulletTransform.Translate(_bulletTransform.up * 1.5f, Space.World);
            _bulletTransform.GetComponent<Bullet>().Init(damage, -1); // -1 is infinity penetration;


        }
    }
}
