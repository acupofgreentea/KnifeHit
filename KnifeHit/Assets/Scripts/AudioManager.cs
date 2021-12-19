using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    AudioMixer mixer;

    [SerializeField]
    AudioSource source;
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
