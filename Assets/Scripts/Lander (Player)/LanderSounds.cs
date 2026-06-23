using UnityEngine;

public class LanderSounds : MonoBehaviour {
    [SerializeField] private AudioSource thrustersSound;

    private Lander lander;

    private void Awake() {
        lander = GetComponent<Lander>();
    }


    private void Start() {
        lander.OnBeforeForce += Lander_OnBeforeForce;
        lander.OnRightForce += Lander_OnRightForce;
        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnUpForce += Lander_OnUpForce;
        SoundManager.Instance.OnSoundVolumeChanged += SoundManager_OnSoundVolumeChanged;

        thrustersSound.Pause();
    }

    private void SoundManager_OnSoundVolumeChanged(object sender, System.EventArgs e) {
        thrustersSound.volume = SoundManager.Instance.GetSoundVolumeNormalized();
    }

    private void Lander_OnUpForce(object sender, System.EventArgs e) {
        if (!thrustersSound.isPlaying) {
            thrustersSound.Play();
        }
    }

    private void Lander_OnLeftForce(object sender, System.EventArgs e) {
        if (!thrustersSound.isPlaying) {
            thrustersSound.Play();
        }
    }

    private void Lander_OnRightForce(object sender, System.EventArgs e) {
        if (!thrustersSound.isPlaying) {
            thrustersSound.Play();
        }
    }

    private void Lander_OnBeforeForce(object sender, System.EventArgs e) {
        thrustersSound.Pause();
    }
}
