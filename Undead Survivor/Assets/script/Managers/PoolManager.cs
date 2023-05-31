using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    public GameObject[] prefabs;
    
    List<GameObject>[] pools;
    private void Awake() 
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }

        Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject _select = null;

        // 선택한 풀의 비활성화된 게임 오브젝트 접근
            // 발견하면 select 변수에 할당
        foreach(GameObject _item in pools[index])
        {
            if(!_item.activeSelf)
            {
                _select = _item;
                _select.SetActive(true);
                break;
            }
        }
        // 비활성화된 게임 오브젝트를 못찾았으면?
            // 새롭게 생성하고 select 변수에 할당
        if(!_select)
        {
            _select = Instantiate(prefabs[index], transform);
            pools[index].Add(_select);
        }

        return _select;
    }
    
}
