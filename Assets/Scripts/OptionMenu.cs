using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    private Slider backgroundMusicSlider;
    private Slider effectsSlider;

    private void Start()
    {
        // 슬라이더 오브젝트를 이름으로 찾기
        backgroundMusicSlider = GameObject.Find("BackgroundMusicSlider").GetComponent<Slider>();
        effectsSlider = GameObject.Find("EffectsSlider").GetComponent<Slider>();

        if (AudioManager.instance != null)
        {
            // 슬라이더 초기값 설정
            backgroundMusicSlider.value = AudioManager.instance.backgroundMusicVolume;
            effectsSlider.value = AudioManager.instance.effectsVolume;

            // 슬라이더 이벤트 연결
            backgroundMusicSlider.onValueChanged.AddListener(SetBackgroundMusicVolume);
            effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
        }
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        AudioManager.instance.SetBackgroundMusicVolume(volume);
    }

    public void SetEffectsVolume(float volume)
    {
        AudioManager.instance.SetEffectsVolume(volume);
    }
}
