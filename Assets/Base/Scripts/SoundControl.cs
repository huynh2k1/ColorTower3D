using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public static SoundControl I;

    [Header("Music")]
    public AudioSource music_source;
    public AudioClip music_sound;

    [Header("Sound")]
    public AudioSource[] sound_sources;
    private Queue<AudioSource> queue_sources;

    [Header("SOUNDS")]
    public AudioClip Lose;
    public AudioClip Click;
    public AudioClip BlockPlaced;
    public AudioClip Merge10;

    private void Awake()
    {
        I = this;
        queue_sources = new Queue<AudioSource>(sound_sources);
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic(float volume = 0.7f)
    {
        music_source.clip = music_sound;
        music_source.volume = volume;   
        music_source.Play();
    }

    public void PlaySoundByType(SoundType type)
    {
        switch (type)
        {
            case SoundType.MERGE10:
                PlaySound(Merge10);
                break;
            case SoundType.LOSE:
                PlaySound(Lose);
                break;
            case SoundType.CLICK:
                PlaySound(Click);
                break;
            case SoundType.BLOCKPLACED:
                PlaySound(BlockPlaced);
                break;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        var source = queue_sources.Dequeue();
        if (source == null)
            return;
        source.PlayOneShot(clip);
        queue_sources.Enqueue(source);
    }

}
public enum SoundType
{
    LOSE,
    MERGE10,
    CLICK,
    MUSIC,
    WIN,
    BLOCKPLACED,
}
