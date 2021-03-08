using UnityEngine;

public class Knife : MonoBehaviour {
    private void OnCollisionStay(Collision other) {
        if (other.collider.CompareTag("cable")) {
            Cable cable = other.collider.GetComponentInParent<Cable>();
            
            Debug.Log(other.GetContact(0).point);

            if (cable) {
                cable.Cut();
            }
        }
    }
}