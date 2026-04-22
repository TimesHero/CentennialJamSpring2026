using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public int minInterest;
    public int maxInterest;
    public Sprite neutralSprite;
    public Sprite happySprite;
    public Sprite upsetSprite;
}
