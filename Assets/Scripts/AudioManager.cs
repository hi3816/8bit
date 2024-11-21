using UnityEngine;

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
    public float defaultVolume = 0.1f; // 기본 볼륨 설정
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = defaultVolume; // 기본 볼륨 설정
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; // 반복 재생 설정
        audioSource.Play();
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound); // 점프 효과음 재생
    }

    public void PlayTrapBlockSound()
    {
        audioSource.PlayOneShot(trapBlockSound); // 투명 블럭 효과음 재생
    }

    public void PlayTrapSpikeSound()
    {
        audioSource.PlayOneShot(trapSpikeSound); // 가시 생성 효과음 재생
    }

    public void PlayMonsterDieSound()
    {
        audioSource.PlayOneShot(monsterDieSound); // 적 밟히는 효과음 재생
    }

    public void PlayPlayerDieSound()
    {
        audioSource.PlayOneShot(playerDieSound); // 플레이어 사망 효과음 재생
    }

    public void PlayStoneDownSound()
    {
        audioSource.PlayOneShot(stoneDownSound); // 바위 떨어지는 효과음 재생
    }
}
