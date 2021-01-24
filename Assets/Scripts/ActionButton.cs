using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ActionButton : MonoBehaviour {
    [SerializeField]
    private AudioClip pressSound;

    private AudioSource audioSource;

    public UnityEvent OnActivate;

    private bool canBePressed = true;

    private void Awake() {
        this.audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {

        if (other.collider.CompareTag("Finger") && this.canBePressed) {
            Debug.Log("Touch : " + this.name + " by " + other.collider.name);
            
            this.canBePressed = false;
            
            if (this.pressSound && this.audioSource) {
                this.audioSource.PlayOneShot(this.pressSound);
            }

            OnActivate?.Invoke();
            
            StartCoroutine(this.pressDelay());
        }
    }

    private IEnumerator pressDelay() {
        yield return new WaitForSeconds(5);
        this.canBePressed = true;
    }
}