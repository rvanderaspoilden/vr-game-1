using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour {
    [Header("Settings")]
    
    [SerializeField]
    private XRIDefaultInputActions inputActions;

    [SerializeField]
    private HandType handType;

    private XRDirectInteractor interactor;

    private Animator animator;

    private void Awake() {
        this.interactor = GetComponent<XRDirectInteractor>();
        this.inputActions = new XRIDefaultInputActions();
        this.inputActions.Enable();
    }

    private void OnEnable() {
        if (handType == HandType.RIGHT) {
            this.inputActions.XRIRightHand.Activating.performed += this.OnActivating;
            this.inputActions.XRIRightHand.Selecting.performed += this.OnSelecting;
        } else {
            this.inputActions.XRILeftHand.Activating.performed += this.OnActivating;
            this.inputActions.XRILeftHand.Selecting.performed += this.OnSelecting;
        }

        StartCoroutine(this.GetHand());
    }

    private void OnDisable() {
        if (handType == HandType.RIGHT) {
            this.inputActions.XRIRightHand.Activating.performed -= this.OnActivating;
            this.inputActions.XRIRightHand.Selecting.performed -= this.OnSelecting;
        } else {
            this.inputActions.XRILeftHand.Activating.performed -= this.OnActivating;
            this.inputActions.XRILeftHand.Selecting.performed -= this.OnSelecting;
        }

        StopAllCoroutines();
    }

    private void OnSelecting(InputAction.CallbackContext ctx) {
        this.animator.SetFloat("Grip", ctx.ReadValue<float>());
    }

    private void OnActivating(InputAction.CallbackContext ctx) {
        this.animator.SetFloat("Trigger", ctx.ReadValue<float>());
    }

    private IEnumerator GetHand() {
        while (!this.animator) {
            this.animator = GetComponentInChildren<Animator>();
            yield return null;
            Debug.Log("Retrieving hand animator...");
        }

        Debug.Log(this.name + " is ok !");
    }
}

public enum HandType {
    RIGHT,
    LEFT
}