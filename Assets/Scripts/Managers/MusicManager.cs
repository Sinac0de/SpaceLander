using System;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    private const int MUSIC_VOLUME_MAX = 10;

    public static MusicManager Instance { get; private set; }

    public event EventHandler OnMusicVolumeChanged;


    private static int musicVolume = 5;
    private static float musicTime;

    private AudioSource musicAudioSource;

    private void Awake() {
        Instance = this;

        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.time = musicTime;

    }


    private void Update() {
        musicTime = musicAudioSource.time;
    }



    public int GetMusicVolume() {
        return musicVolume;
    }

    public float GetMusicVolumeNormalized() {
        return (float)musicVolume / MUSIC_VOLUME_MAX;
    }

    public void ChangeMusicVolume() {
        musicVolume = (musicVolume + 1) % MUSIC_VOLUME_MAX;
        musicAudioSource.volume = GetMusicVolumeNormalized();
        OnMusicVolumeChanged?.Invoke(this, EventArgs.Empty);
    }


}
