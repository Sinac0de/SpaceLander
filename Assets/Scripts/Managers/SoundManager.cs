using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private const int SOUND_VOLUME_MAX = 10;

    public static SoundManager Instance { get; private set; }

    public event EventHandler OnSoundVolumeChanged;


    private static int soundVolume = 5;


    [SerializeField] private AudioClip coinPickupSound;
    [SerializeField] private AudioClip fuelPickupSound;
    [SerializeField] private AudioClip landingSuccessSound;
    [SerializeField] private AudioClip landingCrashSound;



    private void Awake() {
        Instance = this;
    }


    private void Start() {


        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnFuelPickup += Lander_OnFuelPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;


    }


    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e) {
        switch (e.landingType) {
            case Lander.LandingType.Successful:
                AudioSource.PlayClipAtPoint(landingSuccessSound, Camera.main.gameObject.transform.position, GetSoundVolumeNormalized());
                break;
            default:
                AudioSource.PlayClipAtPoint(landingCrashSound, Camera.main.gameObject.transform.position, GetSoundVolumeNormalized());
                break;
        }
    }

    private void Lander_OnFuelPickup(object sender, System.EventArgs e) {
        AudioSource.PlayClipAtPoint(fuelPickupSound, Camera.main.gameObject.transform.position, GetSoundVolumeNormalized());
    }

    private void Lander_OnCoinPickup(object sender, System.EventArgs e) {
        AudioSource.PlayClipAtPoint(coinPickupSound, Camera.main.gameObject.transform.position, GetSoundVolumeNormalized());
    }


    public int GetSoundVolume() {
        return soundVolume;
    }

    public float GetSoundVolumeNormalized() {
        return (float)soundVolume / SOUND_VOLUME_MAX;
    }

    public void ChangeSoundVolume() {
        soundVolume = (soundVolume + 1) % SOUND_VOLUME_MAX;
        OnSoundVolumeChanged?.Invoke(this,EventArgs.Empty);
    }



}
