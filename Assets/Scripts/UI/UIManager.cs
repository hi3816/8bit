using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject liveCanavs;
    [SerializeField] private GameObject optionPopupUI;
    [SerializeField] private Slider backgroundSlider;
    [SerializeField] private Slider effectsSlider;

    private GameObject startUI;
    private GameObject liveUI;
    private GameObject inGameUI;
    AudioManager audioManager;

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
        DontDestroyOnLoad(startUI);
        startUI.SetActive(true);

        liveUI = Instantiate(liveCanavs);
        DontDestroyOnLoad(liveUI);
        liveUI.SetActive(false);

        inGameUI = Instantiate(inGameCanvas);
        DontDestroyOnLoad(inGameUI);
        inGameUI.SetActive(false);
    }

    public void OptionPopupUI()
    {
        optionPopupUI.SetActive(true);
        audioManager.InitSliders(backgroundSlider, effectsSlider);
    }

    public void ShowGame()
    {
        ShowLiveCountUI();
        StartCoroutine(HideLiveCountAfterDelay(3f)); // 3�� �Ŀ� UI ��Ȱ��ȭ
    }
    private IEnumerator HideLiveCountAfterDelay(float delay)
    {
        // delay �ð���ŭ ��ٸ� ��
        yield return new WaitForSeconds(delay);

        HideLiveCountUI();
        ShowInGameUI();
        GameManager.Instance.SetGameRunning();
        GameManager.Instance.ResetTimeUp();
    }

    public void HideGame()
    {
        // ��� ���� ���� UI�� ��Ȱ��ȭ
        HideLiveCountUI();
        HideInGameUI();
        ShowStartUI(); // Start UI�� �ٽ� ǥ��
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