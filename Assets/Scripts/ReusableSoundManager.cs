using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Usage: Don't use this class directly - Use SoundManager (instructions in that class)
 */

abstract public class ReusableSoundManager : MonoBehaviour
{    
    [SerializeField] private bool _isMusicEnabled = true;
    [SerializeField] private bool _areSoundsEnabled = true;        
    
    private AudioSource _musicAudioSource;

    private List<AudioSource> _playingAudioSources;

    virtual protected void Awake()
    {        
        DontDestroyOnLoad(gameObject);

        GameObject audioSourceObject = new GameObject();
        _musicAudioSource = audioSourceObject.AddComponent<AudioSource>();
        _musicAudioSource.loop = true;
        _musicAudioSource.name = "Music";
        DontDestroyOnLoad(audioSourceObject);

        this._playingAudioSources = new List<AudioSource>();
       

        if (_isMusicEnabled)
        {
            this._musicAudioSource.volume = GetMusicVolume(); 
        }
        else if (this._musicAudioSource != null)
        {
            this._musicAudioSource.volume = 0;
        }

        if (!_areSoundsEnabled)
        {
            DisableSounds();
        }
    }

    virtual protected AudioSource PlaySound_(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            throw new UnityException("Audio clip is null! You probably didn't set the corresponding field of SoundManager in the Editor.");
        }
        
        GameObject audioSourceObject = new GameObject();
        AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.name = "Sound";
        audioSourceObject.transform.parent = transform;
        
        if (!_areSoundsEnabled)
        {
            audioSource.volume = 0;
        }

        audioSource.Play();

        // Destroy the audio source object after playing it
        StartCoroutine(DestroyAudioSourceCoroutine(audioSource, audioClip.length));

        _playingAudioSources.Add(audioSource);

        return audioSource;
    }


    private IEnumerator DestroyAudioSourceCoroutine(AudioSource audioSource, float time)
    {
        yield return new WaitForSeconds(time);
        _playingAudioSources.Remove(audioSource);
        Destroy(audioSource.gameObject);
    }


    private IEnumerator FadeOutCoroutine(AudioSource audioSource, float fadeOutTime)
    {
        float startVolume = audioSource.volume;        

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }


    public void PlayMusic(AudioClip musicAudioClip)
    {
        bool wasAlreadyPlaying = this._musicAudioSource.clip == musicAudioClip && this._musicAudioSource.isPlaying;
        this._musicAudioSource.clip = musicAudioClip;
        if (!wasAlreadyPlaying)
        {
            this._musicAudioSource.Play();
        }
    }


    public void EnableMusic()
    {
        _isMusicEnabled = true;
        this._musicAudioSource.volume = GetMusicVolume();
    }


    public void DisableMusic()
    {
        _isMusicEnabled = false;
        this._musicAudioSource.volume = 0;
    }


    public void EnableSounds()
    {
        _areSoundsEnabled = true;
        foreach (AudioSource audioSource in this._playingAudioSources)
        {
            audioSource.volume = 1;
        }
    }


    public void DisableSounds()
    {
        _areSoundsEnabled = false;
        foreach (AudioSource audioSource in this._playingAudioSources)
        {
            audioSource.volume = 0;
        }
    }


    public bool IsMusicEnabled()
    {
        return _isMusicEnabled;
    }


    public bool AreSoundsEnabled()
    {
        return _areSoundsEnabled;
    }

    
    public void ToggleSounds()
    {
        _areSoundsEnabled = !_areSoundsEnabled;
        if (_areSoundsEnabled)
        {
            EnableSounds();
        }
        else
        {
            DisableSounds();
        }
    }


    public void ToggleMusic()
    {
        _isMusicEnabled = !_isMusicEnabled;
        if (_isMusicEnabled)
        {
            EnableMusic();
        }
        else
        {
            DisableMusic();
        }
    }

    /*
    public AudioSource PlayRandomSoundFromList(List<AudioClip> audioClips)
    {
        AudioClip clip = Utils.GetRandomListElement(audioClips);
        return PlaySound(clip);        
    }
    */


    abstract protected float GetMusicVolume();
}