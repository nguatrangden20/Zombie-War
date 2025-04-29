using UnityEngine;

public class ZombieGrowl : MonoBehaviour
{
    [Header("Growl Settings")]
    public AudioClip[] growlClips;
    public float minDelay = 3f;
    public float maxDelay = 8f;

    private AudioSource audioSource;
    private float nextGrowlTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ScheduleNextGrowl();
    }

    private void Update()
    {
        if (Time.time >= nextGrowlTime)
        {
            PlayRandomGrowl();
            ScheduleNextGrowl();
        }
    }

    void ScheduleNextGrowl()
    {
        float delay = Random.Range(minDelay, maxDelay);
        nextGrowlTime = Time.time + delay;
    }

    void PlayRandomGrowl()
    {
        if (growlClips.Length == 0) return;

        AudioClip clip = growlClips[Random.Range(0, growlClips.Length)];
        audioSource.PlayOneShot(clip);
    }
}
