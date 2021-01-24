using UnityEngine;

[CreateAssetMenu(fileName = "Padlock Direction", menuName = "Configurations/Padlock Direction")]
public class PadlockDirection : ScriptableObject {
    [SerializeField]
    private Direction direction;

    [SerializeField]
    private Sprite sprite;

    public Direction GetDirection() {
        return this.direction;
    }

    public Sprite GetSprite() {
        return this.sprite;
    }
}