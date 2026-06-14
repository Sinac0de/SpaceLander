using UnityEngine;

public class LanderVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticles;
    [SerializeField] private ParticleSystem middleThrusterParticles;
    [SerializeField] private ParticleSystem rightThrusterParticles;

    private Lander lander;

    private void Awake() {
        lander = GetComponent<Lander>();

        lander.OnBeforeForce += Lander_OnBeforeForce;
        lander.OnUpForce += Lander_OnUpForce;
        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnRightForce += Lander_OnRightForce;
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
