using UnityEngine;

public class Extinguisher : Props {
    [SerializeField]
    private ParticleSystem particles;

    protected void Awake() {
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