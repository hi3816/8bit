using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform playerStartPosition;

    public event Action<int> OnUpdateLiveCount;
    public event Action<int> OnUpdateScoreTxt;
    public event Action<float> OnTimeLimitChanged;

    private int lives;
    public int startingLives = 3;
    private float timeLimit;
    public float defaultTimeLimit = 10;
    private int score;

    [Header ("GameState")]
    public bool isGameRunning = false;
    public bool isTimeUp = true;

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
        lives = startingLives;
        Debug.Log($"startingLives : {startingLives}");
        Debug.Log($"lives : {lives}");
        Debug.Log($"defaultTimeLimit : {defaultTimeLimit}");
        Debug.Log($"timeLimit : {timeLimit}");
    }

    private void Update()
    {
        if (isTimeUp) return;
        {
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
        Debug.Log($"timeLimit :  {timeLimit} ");
        Debug.Log($"lives :  {lives} ");
        OnUpdateLiveCount?.Invoke(lives);

        UIManager.Instance.ShowGame();
    }

    void ReduceLife()
    {
        lives--;
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

    public void ScoreUP()
    {
        score += 100;
    }

}
