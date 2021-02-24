using System;
using DG.Tweening;
using UnityEngine;

public class Screw : MonoBehaviour {
    [SerializeField] private float unscrewRotateSpeed;

    [SerializeField] private Vector3 targetUnscrewPosition;

    private Rigidbody _rigidbody;

    private bool _isUnScrewing;

    private bool _isUnscrewed;

    public delegate void Unscrewed(Screw screw);

    public event Unscrewed OnUnscrew;

    private void Awake() {
        this._rigidbody = GetComponent<Rigidbody>();
        this._rigidbody.isKinematic = true;
    }

    private void Update() {
        if (_isUnScrewing) {
            this.transform.Rotate(Vector3.forward * Time.deltaTime * this.unscrewRotateSpeed);
        }
    }

    public bool IsUnScrewing => _isUnScrewing;

    public void Unscrew() {
        if (_isUnScrewing || _isUnscrewed) return;

        this._isUnScrewing = true;
        transform.DOLocalMove(this.targetUnscrewPosition, 1f).OnComplete(() => {
            this.transform.parent = null;
            this._rigidbody.isKinematic = false;
            this._isUnScrewing = false;
            this._isUnscrewed = true;
            OnUnscrew?.Invoke(this);
        });
    }
}