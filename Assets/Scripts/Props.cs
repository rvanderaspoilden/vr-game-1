using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public abstract class Props : MonoBehaviour {
    [SerializeField]
    protected Transform spawnerTransform;

    [SerializeField]
    protected Vector3 startPosition;

    [SerializeField]
    protected Quaternion startRotation;

    [SerializeField]
    protected XRGrabInteractable xrGrabInteractable;

    private Coroutine respawnCoroutine;

    protected virtual void Awake() {
        this.xrGrabInteractable = GetComponent<XRGrabInteractable>();

        if (this.xrGrabInteractable == null) {
            Debug.LogError("No XRGrabInteractable found on : " + this.name);
        }
    }

    protected virtual void Start() {
        this.startPosition = this.transform.position;
        this.startRotation = this.transform.rotation;
        
        this.xrGrabInteractable.onSelectEntered.AddListener(OnSelectEntered);
        this.xrGrabInteractable.onSelectEntered.AddListener(StopRespawnCoroutine);
        this.xrGrabInteractable.onSelectExited.AddListener(OnSelectExited);
        this.xrGrabInteractable.onActivate.AddListener(OnActivate);
        this.xrGrabInteractable.onDeactivate.AddListener(OnDeactivate);
    }

    protected virtual void OnDestroy() {
        this.xrGrabInteractable.onSelectEntered.RemoveAllListeners();
        this.xrGrabInteractable.onSelectExited.RemoveAllListeners();
        this.xrGrabInteractable.onActivate.RemoveAllListeners();
        this.xrGrabInteractable.onDeactivate.RemoveAllListeners();
    }

    public virtual void OnSelectEntered(XRBaseInteractor xrBaseInteractor) {
        Debug.Log("OnSelectEntered");
    }

    public virtual void OnSelectExited(XRBaseInteractor xrBaseInteractor) {
        Debug.Log("OnSelectExited");
    }

    public virtual void OnActivate(XRBaseInteractor xrBaseInteractor) {
        Debug.Log("OnActivate");
    }

    public virtual void OnDeactivate(XRBaseInteractor xrBaseInteractor) {
        Debug.Log("OnDeactivate");
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Ground") && this.respawnCoroutine == null) {
            this.respawnCoroutine = StartCoroutine(this.RespawnCheck());
        }
    }

    private void StopRespawnCoroutine(XRBaseInteractor xrBaseInteractor) {
        if (this.respawnCoroutine != null) {
            StopCoroutine(this.respawnCoroutine);
            this.respawnCoroutine = null;
        }
    }

    protected void ResetTransform() {
        this.transform.position = this.spawnerTransform ? this.spawnerTransform.position : this.startPosition;
        this.transform.rotation = this.spawnerTransform ? this.spawnerTransform.rotation : this.startRotation;
    }

    private IEnumerator RespawnCheck() {
        yield return new WaitForSeconds(3);
        this.ResetTransform();
        this.respawnCoroutine = null;
    }
}