using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour {
    [Header("Settings")]
    [SerializeField]
    private ActionBasedController actionBasedController;

    [SerializeField]
    private Animator animator;

    private void Awake() {
        this.actionBasedController = GetComponent<ActionBasedController>();
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
