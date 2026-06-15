using UnityEngine;

public class LanderVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticles;
    [SerializeField] private ParticleSystem middleThrusterParticles;
    [SerializeField] private ParticleSystem rightThrusterParticles;
    [SerializeField] private GameObject landerExplosionVFX;

    private Lander lander;

    private void Awake() {
        lander = GetComponent<Lander>();

        lander.OnBeforeForce += Lander_OnBeforeForce;
        lander.OnUpForce += Lander_OnUpForce;
        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnRightForce += Lander_OnRightForce;
        lander.OnLanded += Lander_OnLanded;

        SetEnabledParticleSystemEmission(leftThrusterParticles, false);
        SetEnabledParticleSystemEmission(middleThrusterParticles, false);
        SetEnabledParticleSystemEmission(rightThrusterParticles, false);
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e) {
        switch (e.landingType) {
            case Lander.LandingType.TooSteep:
            case Lander.LandingType.TooHard:
            case Lander.LandingType.Crashed:
                //CRASH!
                Instantiate(landerExplosionVFX, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                break;
        }
    }

    private void Lander_OnRightForce(object sender, System.EventArgs e) {
        SetEnabledParticleSystemEmission(leftThrusterParticles, true);
    }

    private void Lander_OnLeftForce(object sender, System.EventArgs e) {
        SetEnabledParticleSystemEmission(rightThrusterParticles, true);
    }

    private void Lander_OnUpForce(object sender, System.EventArgs e) {
        SetEnabledParticleSystemEmission(leftThrusterParticles, true);
        SetEnabledParticleSystemEmission(middleThrusterParticles, true);
        SetEnabledParticleSystemEmission(rightThrusterParticles, true);
    }

    private void Lander_OnBeforeForce(object sender, System.EventArgs e) {
        SetEnabledParticleSystemEmission(leftThrusterParticles, false);
        SetEnabledParticleSystemEmission(middleThrusterParticles, false);
        SetEnabledParticleSystemEmission(rightThrusterParticles, false);
    }



    private void SetEnabledParticleSystemEmission(ParticleSystem particle, bool isEnabled) {
        ParticleSystem.EmissionModule emissionModule = particle.emission;
        emissionModule.enabled = isEnabled;
    }
}
