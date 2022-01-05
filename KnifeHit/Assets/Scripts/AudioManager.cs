using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource source;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        PlayAudio(source.clip, source);
    }
    
    public void PlayAudio(AudioClip clip, AudioSource source)
    {
        source.PlayOneShot(clip);
    }
}
