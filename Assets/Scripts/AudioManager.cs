using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    public AudioClip backgroundMusic; // 배경음악 클립
    public AudioClip jumpSound; // 점프 효과음
    public AudioClip trapBlockSound; // 투명 블럭 생성 효과음
    public AudioClip trapSpikeSound; // 가시 생성 효과음
    public AudioClip monsterDieSound; // 적 밟히는 효과음
    public AudioClip playerDieSound; // 플레이어 사망 효과음
    public AudioClip stoneDownSound; // 바위 떨어지는 효과음

    [Range(0f, 1f)]
    public float backgroundMusicVolume = 0.1f;
    [Range(0f, 1f)]
    public float effectsVolume = 0.1f;

    private AudioSource backgroundMusicSource;
    private AudioSource effectsSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        effectsSource = gameObject.AddComponent<AudioSource>();

        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = backgroundMusicVolume;
        PlayBackgroundMusic();

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
        backgroundMusicVolume = volume;
    }

    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = volume;
        effectsVolume = volume;
    }

    public void PlayJumpSound()
    {
        effectsSource.PlayOneShot(jumpSound);
    }

    public void PlayTrapBlockSound()
    {
        effectsSource.PlayOneShot(trapBlockSound);
    }

    public void PlayTrapSpikeSound()
    {
        effectsSource.PlayOneShot(trapSpikeSound);
    }

    public void PlayMonsterDieSound()
    {
        effectsSource.PlayOneShot(monsterDieSound);
    }

    public void PlayPlayerDieSound()
    {
        effectsSource.PlayOneShot(playerDieSound);
    }

    public void PlayStoneDownSound()
    {
        effectsSource.PlayOneShot(stoneDownSound);
    }
}
