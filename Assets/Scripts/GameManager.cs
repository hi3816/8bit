using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Vector3 playerStartPosition;

    public event Action<int> OnUpdateLiveCount;
    public event Action<int> OnUpdateScoreTxt;
    public event Action<float> OnTimeLimitChanged;

    public int startingLives = 3;
    private int lives;
    public float defaultTimeLimit;
    private float timeLimit;

    [Header ("GameState")]
    private bool isGameRunning = false;
    private bool isTimeUp = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 초기화
        lives = startingLives;
    }

    private void Update()
    {
        if (isTimeUp) return;
        {
            // 제한시간 감소
            timeLimit -= Time.deltaTime;
            OnTimeLimitChanged?.Invoke(timeLimit);
            if (timeLimit <= 0)
            {
                timeLimit = 0;
                HandlePlayerDeath();
                Debug.Log("죽음");
                isTimeUp = true;
            }
        }
           
    }

    public void StartGame()
    {
        timeLimit = defaultTimeLimit;

        OnUpdateLiveCount?.Invoke(lives);

        UIManager.Instance.ShowGame();
    }

    void ReduceLife()
    {
        lives--;
        //OnUpdateLiveCount?.Invoke(lives);
    }

    public void HandlePlayerDeath()
    {
        //목숨 카운트 -1
        ReduceLife();
        StartGame();
        
        isGameRunning = false;
    }

    public void ResetTimeUp()
    {
        isTimeUp = false;
    }
    public void SetGameRunning()
    {
        isGameRunning = true;
    }


}
