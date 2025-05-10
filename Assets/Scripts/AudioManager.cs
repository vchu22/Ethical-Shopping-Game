using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the AudioManager between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    // Optional: Method to play music
    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }
}
