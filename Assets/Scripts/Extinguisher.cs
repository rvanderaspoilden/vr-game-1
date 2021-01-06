using UnityEngine;

public class Extinguisher : MonoBehaviour {
    [SerializeField]
    private ParticleSystem particles;

    private void Awake() {
        this.particles = GetComponentInChildren<ParticleSystem>();
        this.particles.Stop();
    }

    public void Use() {
        this.particles.Play();
    }

    public void StopUsing() {
        this.particles.Stop();
    }
}