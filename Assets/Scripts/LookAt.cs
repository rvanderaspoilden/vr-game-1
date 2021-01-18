using UnityEngine;

public class LookAt : MonoBehaviour {
    [SerializeField]
    private Transform origin;

    [SerializeField]
    private Transform target;
    

    // Update is called once per frame
    void Update() {
        this.origin.rotation = Quaternion.LookRotation(Vector3.forward, this.target.transform.position - this.origin.transform.position);
    }
}
