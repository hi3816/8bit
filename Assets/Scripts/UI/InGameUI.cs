using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreTxt;
    [SerializeField] TextMeshProUGUI StageTxt;
    [SerializeField] TextMeshProUGUI TimeTxt;

    private void Awake()
    {
        GameManager.Instance.OnUpdateScoreTxt += UpdateScoreTxt;
        GameManager.Instance.OnTimeLimitChanged += UpdateTimeTxt;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnUpdateLiveCount -= UpdateScoreTxt;
        GameManager.Instance.OnTimeLimitChanged -= UpdateTimeTxt;
    }


    private void UpdateScoreTxt(int score)
    {
        ScoreTxt.text = $"Score\n{score}";
    }
    private void UpdateStageTxt(int stage)
    {
        ScoreTxt.text = $"Stage\n{stage}";
    }
    private void UpdateTimeTxt(float timeLimit)
    {
        TimeTxt.text = $"Time\n{Mathf.FloorToInt(timeLimit)}";
    }
}
