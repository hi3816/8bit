using TMPro;
using UnityEngine;

public class LiveUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI liveCountTxt;

    private void Awake()
    {
        GameManager.Instance.OnUpdateLiveCount += UpdateLiveCount;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnUpdateLiveCount -= UpdateLiveCount;
    }

    private void UpdateLiveCount(int count)
    {
        liveCountTxt.text = $"x {count}";
    }
}
