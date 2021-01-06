using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour {
    [Header("Settings")]
    [SerializeField]
    private ActionBasedController actionBasedController;

    [SerializeField]
    private XRDirectInteractor interactor;

    [SerializeField]
    private Animator animator;

    private void Awake() {
        this.actionBasedController = GetComponent<ActionBasedController>();
        this.interactor = GetComponent<XRDirectInteractor>();
    }

    private void OnEnable() {
        this.actionBasedController.activateAction.action.performed += this.Activate;
        this.actionBasedController.selectAction.action.performed += this.Grip;

        StartCoroutine(this.GetHand());
    }

    private void OnDisable() {
        this.actionBasedController.activateAction.action.performed -= this.Activate;
        this.actionBasedController.selectAction.action.performed += this.Grip;
        
        StopAllCoroutines();
    }

    private void Grip(InputAction.CallbackContext ctx) {
        this.animator.SetFloat("Grip", ctx.ReadValue<float>());

        if (this.interactor.selectTarget && ctx.ReadValue<float>() < 0.99f) {
            this.interactor.allowSelect = false;
        } else if (!this.interactor.selectTarget && ctx.ReadValue<float>() >= 0.95f) {
            this.interactor.allowSelect = true;
        }
    }
    
    private void Activate(InputAction.CallbackContext ctx) {
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
