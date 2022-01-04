using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    AudioSource source;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
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
