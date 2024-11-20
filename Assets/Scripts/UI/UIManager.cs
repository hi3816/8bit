using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject liveCanavs;

    private GameObject startUI;
    private GameObject liveUI;
    private GameObject inGameUI;

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
        startUI = Instantiate(startCanvas);
        startUI.SetActive(true);

        liveUI = Instantiate(liveCanavs);
        liveUI.SetActive(false);

        inGameUI = Instantiate(inGameCanvas);
        inGameUI.SetActive(false);
    }

    public void ShowGame()
    {
        ShowLiveCountUI();
        StartCoroutine(HideLiveCountAfterDelay(3f)); // 3초 후에 UI 비활성화
    }
    private IEnumerator HideLiveCountAfterDelay(float delay)
    {
        // delay 시간만큼 기다린 후
        yield return new WaitForSeconds(delay);

        HideLiveCountUI();
        ShowInGameUI();
        GameManager.Instance.SetGameRunning();
        GameManager.Instance.ResetTimeUp();
    }

    public void HideGame()
    {
        // 모든 게임 관련 UI를 비활성화
        HideLiveCountUI();
        HideInGameUI();
        ShowStartUI(); // Start UI를 다시 표시
    }

    public void ShowLiveCountUI()
    {
        liveUI.SetActive(true);
    }
    public void HideLiveCountUI()
    {
        liveUI.SetActive(false);
    }

    public void ShowInGameUI()
    {
        inGameUI.SetActive(true);
    }
    public void HideInGameUI()
    {
        inGameUI.SetActive(false);
    }

    public void ShowStartUI()
    {
        startUI.SetActive(true);
    }
    public void HideStartUI()
    {
        startUI.SetActive(false);
    }


}