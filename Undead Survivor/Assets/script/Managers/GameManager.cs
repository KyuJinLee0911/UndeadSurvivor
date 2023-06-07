using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [Header("Game Control")]
    public bool isAlive;
    public float gameTime;
    public float maxGameTIme = 20f;
    [Header("Game Object")]
    public Player player;
    public PoolManager poolManager;
    public LevelUp levelUpUI;

    [Header("player Info")]
    public int hp;
    public int maxHp = 100;
    public int level;
    public int killCount;
    public int exp;
    public int[] levelUpExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600};
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

    private void Start() 
    {
        hp = maxHp;

        // 임시 스크립트
        levelUpUI.Select(0);
    }

    private void Awake() {
        isAlive = true;
    }

    void Update()
    {
        if(!isAlive)
            return;
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTIme)
        {
            gameTime = maxGameTIme;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp == levelUpExp[Mathf.Min(level, levelUpExp.Length - 1)])
        {
            level++;
            exp = 0;
            levelUpUI.Show();
        }
    }

    public void Pause()
    {
        isAlive = false;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isAlive = true;
        Time.timeScale = 1f;
    }
}
