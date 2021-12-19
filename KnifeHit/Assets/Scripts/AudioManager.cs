using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    AudioSource source;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PlayAudio(source.clip, source);
    }
    
    public void PlayAudio(AudioClip clip, AudioSource source)
    {
        source.PlayOneShot(clip);
    }
}
