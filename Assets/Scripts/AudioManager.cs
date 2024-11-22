using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip backgroundMusic;
    public AudioClip jumpSound;
    public AudioClip trapBlockSound;
    public AudioClip trapSpikeSound;
    public AudioClip monsterDieSound;
    public AudioClip playerDieSound;
    public AudioClip stoneDownSound;

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
