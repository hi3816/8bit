using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // 배경음악 클립
    public AudioClip jumpSound; // 점프 효과음
    public AudioClip trapBlockSound; // 투명 블럭 생성 효과음
    public AudioClip trapSpikeSound; // 가시 생성 효과음
    public AudioClip monsterDieSound; // 적 밟히는 효과음
    public AudioClip playerDieSound; // 플레이어 사망 효과음
    public AudioClip stoneDownSound; // 바위 떨어지는 효과음

    [Range(0f, 1f)]
    public float backgroundMusicVolume = 0.1f; // 배경음악 볼륨
    [Range(0f, 1f)]
    public float effectsVolume = 0.1f; // 효과음 볼륨

    private AudioSource backgroundMusicSource;
    private AudioSource effectsSource;

    public Slider backgroundMusicSlider; // 배경음악 슬라이더
    public Slider effectsSlider; // 효과음 슬라이더

    void Start()
    {
        // 두 개의 AudioSource 컴포넌트를 추가
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        effectsSource = gameObject.AddComponent<AudioSource>();

        // 배경음악 설정
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = backgroundMusicVolume;
        PlayBackgroundMusic();

        // 효과음 기본 설정
        effectsSource.volume = effectsVolume;
    }

    public void InitSliders(Slider backgroundSlider, Slider sfxSlider)
    {
        this.backgroundMusicSlider = backgroundSlider;
        this.effectsSlider = sfxSlider;
        
        // 슬라이더 초기값 설정
        backgroundMusicSlider.value = backgroundMusicVolume;
        effectsSlider.value = effectsVolume;

        // 슬라이더 이벤트 연결
        backgroundMusicSlider.onValueChanged.AddListener(SetBackgroundMusicVolume);
        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
    }

    public void PlayBackgroundMusic()
    {
        if (!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusicSource.volume = volume;
    }

    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = volume;
    }

    public void PlayJumpSound()
    {
        effectsSource.PlayOneShot(jumpSound); // 점프 효과음 재생
    }

    public void PlayTrapBlockSound()
    {
        effectsSource.PlayOneShot(trapBlockSound); // 투명 블럭 효과음 재생
    }

    public void PlayTrapSpikeSound()
    {
        effectsSource.PlayOneShot(trapSpikeSound); // 가시 생성 효과음 재생
    }

    public void PlayMonsterDieSound()
    {
        effectsSource.PlayOneShot(monsterDieSound); // 적 밟히는 효과음 재생
    }

    public void PlayPlayerDieSound()
    {
        effectsSource.PlayOneShot(playerDieSound); // 플레이어 사망 효과음 재생
    }

    public void PlayStoneDownSound()
    {
        effectsSource.PlayOneShot(stoneDownSound); // 바위 떨어지는 효과음 재생
    }
}
