using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;



    private void FixedUpdate() 
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }


    Transform GetNearest()
    {
        Transform _result = null;

        float diff = 100;

        // 플레이어 근처에 있는 몬스터 중에 가장 가까운 몬스터 찾기
        foreach(RaycastHit2D _target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = _target.transform.position;

            float curDist = Vector3.Distance(myPos, targetPos);

            if(curDist < diff)
            {
                diff = curDist;
                _result = _target.transform;
            }
        }

        return _result;
    }
}
