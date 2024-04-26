using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public bool _musicEnabled = true;
    public bool _fxEnabled = true;

    [Range(0f, 1f)]
    public float _musicVolume = 1.0f;
    [Range(0f, 1f)]
    public float _fxVolume = 1.0f;    

    [SerializeField] AudioSource _musicSource;

    [Header("Sounds")]
    public AudioClip ClearRowSound;
    public AudioClip MoveSound;
    public AudioClip DropSound;
    public AudioClip ErrorSound;
    public AudioClip GameOverSound;
    public AudioClip BackgroundSound;
    public AudioClip GameOverVocalSound;
    public AudioClip LevelUpVocal;
    public AudioClip LevelUp;

    public AudioClip[] VocalPlays;


    [SerializeField] IconToggle _fxIconToggle;
    [SerializeField] IconToggle _musicIconToggle;


    // Start is called before the first frame update
    void Start()
    {
        PlayBackgroundMusic(BackgroundSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (!_musicEnabled || !musicClip || !_musicSource)
        {
            print("Error");
            return;
        }

        _musicSource.Stop();
        _musicSource.clip = musicClip;
        _musicSource.volume = _musicVolume;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void UpdateMusic()
    {
        if(_musicSource.isPlaying != _musicEnabled)
        {
            if(_musicEnabled)
            {
                PlayBackgroundMusic(BackgroundSound);
                return;
            }
            _musicSource.Stop();
        }
    }

    public void ToggleMusic()
    {
        _musicEnabled = !_musicEnabled;
        _musicIconToggle.ToggleIcon(_musicEnabled);
        UpdateMusic();
    }

    public void ToggleFX()
    {
        _fxEnabled = !_fxEnabled;
        _fxIconToggle.ToggleIcon(_fxEnabled);
    }
}
