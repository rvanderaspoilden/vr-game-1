using UnityEngine;

public class Cable : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private GameObject cutCableObject;
    [SerializeField] private GameObject fullCableObject;

    [SerializeField] private MeshRenderer[] meshRenderers;

    private CableState _state = CableState.FULL;
    
    private Color _color;

    public delegate void OnStateChanged(Cable cable);

    public event OnStateChanged OnCut;

    public void Setup(Color color) {
        this._color = color;

        foreach (MeshRenderer meshRenderer in this.meshRenderers) {
            Material material = new Material(meshRenderer.material);
            material.color = this._color;
            meshRenderer.material = material;
        }

        this.fullCableObject.SetActive(true);
        this.cutCableObject.SetActive(false);
    }

    public void Cut() {
        this._state = CableState.CUT;
        this.fullCableObject.SetActive(false);
        this.cutCableObject.SetActive(true);
        OnCut?.Invoke(this);
    }

    public Color Color => _color;

    public CableState State => _state;
}

public enum CableState {
    FULL,
    CUT
}