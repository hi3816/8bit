using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    private Slider backgroundMusicSlider;
    private Slider effectsSlider;

    private void Start()
    {
        // �����̴� ������Ʈ�� �̸����� ã��
        backgroundMusicSlider = GameObject.Find("BackgroundMusicSlider").GetComponent<Slider>();
        effectsSlider = GameObject.Find("EffectsSlider").GetComponent<Slider>();

        if (AudioManager.Instance != null)
        {
            // �����̴� �ʱⰪ ����
            backgroundMusicSlider.value = AudioManager.Instance.backgroundMusicVolume;
            effectsSlider.value = AudioManager.Instance.effectsVolume;

            // �����̴� �̺�Ʈ ����
            backgroundMusicSlider.onValueChanged.AddListener(SetBackgroundMusicVolume);
            effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
        }
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        AudioManager.Instance.SetBackgroundMusicVolume(volume);
    }

    public void SetEffectsVolume(float volume)
    {
        AudioManager.Instance.SetEffectsVolume(volume);
    }
}
