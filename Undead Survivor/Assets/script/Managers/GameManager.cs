using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Result uiResult;
    public GameObject enemyCleaner;

    [Header("player Info")]
    public int playerId;
    public float hp;
    public float maxHp = 100;
    public int level;
    public int killCount;
    public int exp;
    public int[] levelUpExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
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

    public void GameStart(int id)
    {
        playerId = id;
        hp = maxHp;

        player.gameObject.SetActive(true);

        levelUpUI.Select(playerId % 2);

        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());

    }

    IEnumerator GameOverRoutine()
    {
        isAlive = false;
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Pause();
        Time.timeScale = 1f;
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isAlive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Pause();
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (!isAlive)
            return;
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTIme)
        {
            gameTime = maxGameTIme;
            GameVictory();

        }
    }

    public void GetExp()
    {
        if (!isAlive)
            return;

        exp++;

        if (exp == levelUpExp[Mathf.Min(level, levelUpExp.Length - 1)])
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
