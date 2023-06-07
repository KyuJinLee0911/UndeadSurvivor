using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints; // 몬스터의 스폰 위치를 담고 있는 배열
    public SpawnData[] spawnData; // 레벨 별 스폰데이터를 담고 있는 배열

    public int level;
    float timer = 0f;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        if(!GameManager.Instance().isAlive)
            return;
            
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance().gameTime / 10f), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTerm)
        {
            timer = 0f;
            Spawn();
        }

    }

    void Spawn()
    {
        // 프리팹 배열에서 몬스터 선택
        GameObject _randMonster = GameManager.Instance().poolManager.Get(0);
        // 스폰 위치 설정
        _randMonster.transform.position = spawnPoints[Random.Range(1,spawnPoints.Length)].position;
        // 현재 레벨에 맞는 몬스터 데이터 설정
        _randMonster.GetComponent<Monster>().Init(spawnData[level]);
    }
}

[System.Serializable]
// 스폰 정보를 담고 있는 클래스
public class SpawnData
{
    public float spawnTerm;
    public int monsterType;
    public int monsterHP;
    public float monsterSpeed;
}
