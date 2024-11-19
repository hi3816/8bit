using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LiveUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI liveCountTxt;

    private void Awake()
    {
        GameManager.Instance.OnUpdateLiveCount += UpdateLiveCount;
    }

    private void UpdateLiveCount(int count)
    {
        liveCountTxt.text = $"x {count}";
    }
}
