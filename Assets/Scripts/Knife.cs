using UnityEngine;

public class Knife : Props {
    [Header("Settings")]
    [SerializeField]
    private Transform tipTransform;

    [SerializeField]
    private Transform baseTransform;

    [SerializeField]
    private LayerMask layerMask;

    private RaycastHit[] _hits;

    private bool _used;

    private void Update() {
        if (!_used) return;

        this._hits = Physics.RaycastAll(this.baseTransform.position, this.tipTransform.position - this.baseTransform.transform.position);

        if (!(_hits?.Length > 0)) return;

        foreach (var raycastHit in _hits) {
            Cable cable = raycastHit.collider.GetComponentInParent<Cable>();

            if (cable && cable.State == CableState.FULL) {
                cable.Cut();
            }
        }
    }

    public bool Used {
        get => _used;
        set => _used = value;
    }
}