using System.Collections;
using UnityEngine;

public abstract class Props : MonoBehaviour {
    [SerializeField]
    protected Transform spawnerTransform;

    [SerializeField]
    protected Vector3 startPosition;

    [SerializeField]
    protected Quaternion startRotation;

    private Coroutine respawnCoroutine;

    protected virtual void Start() {
        this.startPosition = this.transform.position;
        this.startRotation = this.transform.rotation;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Ground") && this.respawnCoroutine == null) {
            this.respawnCoroutine = StartCoroutine(this.RespawnCheck());
        }
    }

    private void StopRespawnCoroutine() {
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