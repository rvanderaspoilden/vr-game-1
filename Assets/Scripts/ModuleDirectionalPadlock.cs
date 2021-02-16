using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ModuleDirectionalPadlock : MonoBehaviour {
    [Header("Settings")]
    [SerializeField]
    private List<PadlockDirection> targetCode;

    [SerializeField]
    private ActionButton upButton;

    [SerializeField]
    private ActionButton downButton;

    [SerializeField]
    private ActionButton leftButton;

    [SerializeField]
    private ActionButton rightButton;

    [SerializeField]
    private PadlockDirection leftPadlockDirection;

    [SerializeField]
    private PadlockDirection rightPadlockDirection;

    [SerializeField]
    private PadlockDirection upPadlockDirection;

    [SerializeField]
    private PadlockDirection downPadlockDirection;

    [SerializeField]
    private Image[] padlockImageSlots;

    [Header("Debug")]
    [SerializeField]
    private List<PadlockDirection> currentCode;

    private void Awake() {
        this.currentCode = new List<PadlockDirection>();
    }

    private void Start() {
        this.ClearPadlockScreen();
    }

    public void AddDirection(PadlockDirection padlockDirection) {
        if (this.currentCode.Count < this.targetCode.Count) {
            this.currentCode.Add(padlockDirection);
            this.UpdateScreen(this.currentCode.Count - 1, padlockDirection.GetSprite());
            this.CheckValidity();
        }
    }

    public void Reset() {
        this.currentCode.Clear();
        this.ClearPadlockScreen();
    }

    private void UpdateScreen(int pos, Sprite sprite) {
        this.padlockImageSlots[pos].sprite = sprite;
        this.padlockImageSlots[pos].enabled = true;
    }

    private void ClearPadlockScreen() {
        foreach (Image slot in this.padlockImageSlots) {
            slot.enabled = false;
        }
    }

    private void CheckValidity() {
        if (targetCode.Count == currentCode.Count) {
            Debug.Log(targetCode.Intersect(currentCode).Count() == targetCode.Count ? "CODE VALID" : "CODE INVALID");
        }
    }
}