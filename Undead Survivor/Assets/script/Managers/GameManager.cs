using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public float gameTime;
    public float maxGameTIme = 20f;
    public Player player;
    public PoolManager monsterPool;
    // Start is called before the first frame update

    public static GameManager Instance()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<GameManager>();

            if (_instance == null)
                Debug.Log("No Singleton Obj");
        }
        return _instance;
    }

    private void Awake()
    {

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTIme)
        {
            gameTime = maxGameTIme;
        }
    }
}
