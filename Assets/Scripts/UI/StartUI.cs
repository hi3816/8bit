using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        // Play 버튼에 GameManager의 StartGame() 연결
        playButton.onClick.AddListener(GameManager.Instance.StartGame);
    }
}
