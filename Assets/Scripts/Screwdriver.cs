using UnityEngine;

public class Screwdriver : MonoBehaviour {
    [SerializeField] private Transform tipTransform;

    private bool _raycasting;

    private RaycastHit _hit;
    
    // Update is called once per frame
    void Update()
    {
        if (this._raycasting) {
            Debug.DrawRay(this.tipTransform.position, this.tipTransform.forward, Color.magenta);
            if (Physics.Raycast(this.tipTransform.position, this.tipTransform.forward, out _hit, .005f, (1 << 9))) {
                _hit.collider.GetComponent<Screw>().Unscrew();
            }
        }
    }

    public bool Raycasting {
        get => _raycasting;
        set => _raycasting = value;
    }
}
