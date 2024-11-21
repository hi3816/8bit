using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject odioPrefab; // 프리팹 연결
    private static GameObject odioPrefabInstance;

    public Transform playerStartPosition;

    public event Action<int> OnUpdateLiveCount;
    public event Action<int> OnUpdateScoreTxt;
    public event Action<float> OnTimeLimitChanged;

    public event Action OnDeath;

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
        if (odioPrefabInstance == null) 
        {
            odioPrefabInstance = Instantiate(odioPrefab);
            DontDestroyOnLoad(odioPrefabInstance);
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
                OnDeath?.Invoke();
                Debug.Log("죽음");
                isTimeUp = true;
            }
        }
           
    }

    public void StartGame()
    {
        timeLimit = defaultTimeLimit;
        OnUpdateLiveCount?.Invoke(lives);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
