using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/**
 * Usage:
 * 1) Create SoundManager object in the scene
 * 2) Add SoundManager script to the object
 * 3) Edit this script to create fields for the sounds (jump, hit, etc.) and the musics (gameplayMusic, etc.) 
 * 4) In the editor, select the SoundManager object, go to the Inspector and under SoundManager set the Audio Clips with your audio files (usually mp3s)
 * 5) To play for example the "hit" sound:
 * SoundManager.PlaySound(SoundManager.instance.hit);
 * 6) To play for example the "gameplayMusic" music:
 * SoundManager.PlayMusic(SoundManager.instance.gameplayMusic);
 * 7) To Disable, Enable or Toggle Music or Sounds: 
 * SoundManager.instance.DisableSounds(), etc.
 */

public class SoundManager : ReusableSoundManager
{


    private const float MUSIC_VOLUME = 0.5f;

    // SFX (Set them in the editor)
    // Public because they can be accessed by other classes that call PlaySound()    
    public AudioClip paddleHit;
    public AudioClip blockHit;

    // Musics (Set them in the editor)
    public AudioClip gameplayMusic;    

    static public SoundManager instance;

    private Dictionary<AudioClip, float> _volumesByAudioClip;

    override protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
            base.Awake();
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    static public AudioSource PlaySound(AudioClip audioClip)
    {
        if (instance == null)
        {
            Debug.LogError("SoundManager instance is null. Make sure to create a SoundManager object in the scene and add the SoundManager script to it.");
            return null;
        }
        return instance.PlaySound_(audioClip);
    }

    private void Init()
    {        
        _volumesByAudioClip = new Dictionary<AudioClip, float>();
        _volumesByAudioClip[paddleHit] = 1;
        
    }

    protected override float GetMusicVolume()
    {
        return MUSIC_VOLUME;
    }

    protected override AudioSource PlaySound_(AudioClip audioClip)
    {
        var audioSource = base.PlaySound_(audioClip);
        
        if (_volumesByAudioClip.ContainsKey(audioClip))
        {            
            audioSource.volume = _volumesByAudioClip[audioClip];
        }

        return audioSource;
    }

    private void ApplyRandomPitch(AudioSource audioSource, float minAddedPitch, float maxAddedPitch)
    {
        audioSource.pitch += Random.Range(minAddedPitch, maxAddedPitch);
    }

    private IEnumerator EnableMusicCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        EnableMusic();
    }


}
