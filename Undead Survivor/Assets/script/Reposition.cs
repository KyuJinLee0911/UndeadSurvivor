using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D collider;

    private void Awake() 
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Area"))
            return;

        Vector3 _playerPos = GameManager.Instance().player.transform.position;
        Vector3 _myPos = transform.position;
        float _distX = Mathf.Abs(_playerPos.x - _myPos.x);
        float _distY = Mathf.Abs(_playerPos.y - _myPos.y);

        Vector3 _playerDir = GameManager.Instance().player.inputVec;
        float _dirX = (_playerDir.x < 0) ? -1 : 1;
        float _dirY = (_playerDir.y < 0) ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if(_distX > _distY) 
                    transform.Translate(Vector3.right * _dirX * 40);
                else if (_distY > _distX)
                    transform.Translate(Vector3.up * _dirY * 40);
                else
                    transform.Translate(_dirX * 40, _dirY*40, 0);
                break;

            case "Enemy" :
                if(!collider.enabled)
                    return;
                float _randFloat = Random.Range(-3f, 3f);
                transform.Translate(_playerDir * 20 + new Vector3(_randFloat,_randFloat,0));
                break;

        }

    }
}
