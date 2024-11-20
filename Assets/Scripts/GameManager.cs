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

    public event Action OnPlayerDie;
    public event Action OnGameStart;

    public int startingLives = 3;
    private int lives;
    public float timeLimit;

    private bool isGameRunning = false;
    private bool isTimeUp = false;

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
                isTimeUp = true;
            }
        }
           
    }

    public void StartGame()
    {
        // 초기화
        lives = startingLives;
        timeLimit = 60f; // 원하는 제한시간
        isGameRunning = true;

        // 초기 UI 업데이트
        OnUpdateLiveCount?.Invoke(lives);

        // 게임 시작 이벤트 호출
        OnGameStart?.Invoke();

        UIManager.Instance.ShowGame();
    }

    void ReduceLife()
    {
        lives--;
        OnUpdateLiveCount?.Invoke(lives);
    }

    public void HandlePlayerDeath()
    {
        Debug.Log("플레이어가 죽으면 이 함수를 호출해주세요");
        
        //목숨 카운트 -1
        ReduceLife();
        OnPlayerDie?.Invoke();

        UIManager.Instance.ShowGame();

        //게임 초기화(시간, 플레이어 위치 초기화)
        //ResetGame();
    }
    
}
