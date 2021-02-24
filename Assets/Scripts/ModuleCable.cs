using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCable : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private List<Screw> screws;

    [SerializeField] private Rigidbody cap;

    private List<Screw> _screwsRemoved;

    private void Awake() {
        this._screwsRemoved = new List<Screw>();
        this.cap.isKinematic = true;
    }

    // Start is called before the first frame update
    void Start() {
        this.screws.ForEach(screw => screw.OnUnscrew += OnUnscrew);
    }

    private void OnDestroy() {
        this.screws.ForEach(screw => screw.OnUnscrew -= OnUnscrew);
    }

    private void OnUnscrew(Screw screw) {
        this._screwsRemoved.Add(screw);

        if (this.screws.Count == this._screwsRemoved.Count) {
            this.cap.transform.parent = null;
            this.cap.isKinematic = false;
        }
    }
}