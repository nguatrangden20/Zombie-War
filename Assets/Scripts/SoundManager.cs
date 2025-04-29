using UnityEngine;
public enum SoundType
{
    ShotGun = 0,
    AK47 = 1,
    Explosion = 2
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip[] soundSources;

    private AudioSource sfxSource;

    private void Awake()
    {
        sfxSource = GetComponent<AudioSource>();
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlaySFX(SoundType type)
    {
        sfxSource.PlayOneShot(soundSources[(int)type]);
    }
}
