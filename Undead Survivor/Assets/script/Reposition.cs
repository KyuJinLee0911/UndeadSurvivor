using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    new Collider2D collider;

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

        switch (transform.tag)
        {
            case "Ground":
                float _distX = (_playerPos.x - _myPos.x);
                float _distY = (_playerPos.y - _myPos.y);

                float _dirX = (_distX < 0) ? -1 : 1;
                float _dirY = (_distY < 0) ? -1 : 1;

                _distX = Mathf.Abs(_distX);
                _distY = Mathf.Abs(_distY);

                if (_distX > _distY)
                    transform.Translate(Vector3.right * _dirX * 40);
                else if (_distY > _distX)
                    transform.Translate(Vector3.up * _dirY * 40);
                
                break;

            case "Enemy":
                if (!collider.enabled)
                    return;
                float _randFloat = Random.Range(-3f, 3f);
                Vector3 dist = _playerPos - _myPos;
                Vector3 rand = new Vector3(Random.Range(-3,3), Random.Range(-3,3), 0);
                transform.Translate(rand + dist * 2);
                break;

        }

    }
}
