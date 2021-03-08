using System.Collections.Generic;
using UnityEngine;

public class ModuleCable : MonoBehaviour {
    [Header("Settings")]
    [SerializeField]
    private List<Screw> screws;

    [SerializeField]
    private List<Cable> cables;

    [Tooltip("Represent all colors which could be used for cables")]
    [SerializeField]
    private List<Color> cableColors;

    [SerializeField]
    private Rigidbody cap;

    private List<Color> _colors;

    private List<Screw> _screwsRemoved;

    private void Awake() {
        this._screwsRemoved = new List<Screw>();
        this._colors = new List<Color>(cables.Count);
        this.cap.isKinematic = true;
    }

    // Start is called before the first frame update
    void Start() {
        this.screws.ForEach(screw => screw.OnUnscrew += OnUnscrew);
        this.cables.ForEach(cable => {
            Color color = this.cableColors[Random.Range(0, this.cableColors.Count)];
            cable.Setup(color);
            this._colors.Add(color);
            this.cableColors.Remove(color);
            cable.OnCut += OnCut;
        });
    }

    private void OnDestroy() {
        this.screws.ForEach(screw => screw.OnUnscrew -= OnUnscrew);
        this.cables.ForEach(cable => cable.OnCut -= OnCut);
    }

    private void OnUnscrew(Screw screw) {
        this._screwsRemoved.Add(screw);

        if (this.screws.Count == this._screwsRemoved.Count) {
            this.cap.transform.parent = null;
            this.cap.isKinematic = false;
        }
    }

    private void OnCut(Cable cable) { }
}