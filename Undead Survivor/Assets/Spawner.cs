using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public SpawnData[] spawnData;

    public int level;
    float timer = 0f;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
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
        GameObject _randMonster = GameManager.Instance().monsterPool.Get(0);
        _randMonster.transform.position = spawnPoints[Random.Range(1,spawnPoints.Length)].position;
        _randMonster.GetComponent<Monster>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTerm;
    public int monsterType;
    public int monsterHP;
    public float monsterSpeed;
}
