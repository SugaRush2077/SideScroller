using UnityEngine.Audio;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;
    //public Sound[] MusicList;
    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }
    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void PlayNextSong()
    {
        audioSource.clip = GetRandomClip();
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }
    }
    
}
