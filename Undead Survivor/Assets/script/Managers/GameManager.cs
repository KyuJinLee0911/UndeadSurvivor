using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public float gameTime;
    public float maxGameTIme = 20f;
    public Player player;
    public PoolManager poolManager;
    // 싱글톤 패턴
    public static GameManager Instance()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

            if (_instance == null)
                Debug.Log("No Singleton Obj");
        }
        return _instance;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTIme)
        {
            gameTime = maxGameTIme;
        }
    }
}
