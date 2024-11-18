using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // 배경음악 클립
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; // 반복 재생 설정
        audioSource.Play();
    }
}
