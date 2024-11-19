using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<int> OnUpdateLiveCount;


    private int lives = 3;
    private float timeLimit = 60f;
    

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
        // 제한시간 감소
        timeLimit -= Time.deltaTime;
        if (timeLimit <= 0)
        {
            //죽음();
            ReduceLife();
        }

    }
    void ReduceLife()
    {
        lives--;
        OnUpdateLiveCount?.Invoke(lives);
    }
}
