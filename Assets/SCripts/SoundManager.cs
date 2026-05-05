using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource myAudioSource;

    [Header("Game Sounds")]
    public AudioClip jumpSFX;
    public AudioClip shootSFX;
    public AudioClip enemyDeathSFX;
    public AudioClip coinSFX;
    public AudioClip playerDeathSFX;
    public AudioClip levelCompleteSFX;
    public AudioClip waterSplashSFX;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        myAudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        myAudioSource.PlayOneShot(clip);
    }
}