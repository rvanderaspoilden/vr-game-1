using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrabInteractable : XRGrabInteractable {
    public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();

    private XRBaseInteractor secondHandInteractor;

    // Start is called before the first frame update
    void Start() {
        foreach (XRSimpleInteractable point in this.secondHandGrabPoints) {
            point.onSelectEntered.AddListener(this.OnSecondHandGrab);
            point.onSelectExited.AddListener(this.OnSecondHandRelease);
        }
    }

    public void OnSecondHandGrab(XRBaseInteractor interactor) {
        this.secondHandInteractor = interactor;
    }

    public void OnSecondHandRelease(XRBaseInteractor interactor) {
        this.secondHandInteractor = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase) {
        if (selectingInteractor && this.secondHandInteractor) {
            Quaternion rotation = Quaternion.LookRotation(this.secondHandInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
            selectingInteractor.attachTransform.rotation = Quaternion.Euler(rotation.eulerAngles.x, this.selectingInteractor.transform.rotation.eulerAngles.y,
                this.selectingInteractor.transform.rotation.eulerAngles.z);
        }

        base.ProcessInteractable(updatePhase);
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor) {
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor) {
        base.OnSelectExited(interactor);
        this.secondHandInteractor = null;
    }

    // Update is called once per frame
    void Update() {
    }

    /*public override bool IsSelectableBy(XRBaseInteractor interactor) {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }*/
}