using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Extinguisher : Props {
    [SerializeField]
    private ParticleSystem particles;

    protected override void Awake() {
        base.Awake();
        
        this.particles = GetComponentInChildren<ParticleSystem>();
        this.particles.Stop();
    }
    
    public override void OnActivate(XRBaseInteractor xrBaseInteractor) {
        this.particles.Play();
    }

    public override void OnDeactivate(XRBaseInteractor xrBaseInteractor) {
        this.particles.Stop();
    }
}