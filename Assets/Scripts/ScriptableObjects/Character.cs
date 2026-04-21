using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public string name;
    public int minInterest;
    public int maxInterest;
    public Sprite neutralSprite;
}
