using UnityEngine;
using Yarn.Unity;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private List<Character> characters;
    [SerializeField] private SpriteRenderer portrait;
    [SerializeField] private Animator animator;
    private bool someoneLikes = false;
    private Character pickedChar;


    [YarnCommand("GetCharacter")]
    public void GetCharacter()
    {
        if(characters.Count <= 0)
        {
            if(someoneLikes) StartCoroutine(StartDialogue("GameClosingGood"));
            else StartCoroutine(StartDialogue("GameClosingBad"));
        }
        else
        {
            int charIndex = UnityEngine.Random.Range(0, characters.Count);
            pickedChar = characters[charIndex];
            characters.RemoveAt(charIndex);
            StartCoroutine(StartDialogue(pickedChar));
            portrait.sprite = pickedChar.neutralSprite;
        }
    }

    [YarnCommand("SetLiked")]
    public void WinCharacter()
    {
        someoneLikes = true;
    }

    private IEnumerator StartDialogue(Character character)
    {
        yield return new WaitForSeconds(0.25f); //Small delay to prevent skips
        dialogueRunner.StartDialogue($"{character.name}DialogueStart");
    }
    private IEnumerator StartDialogue(string dialogueName) //Love me an overloaded function
    {
        yield return new WaitForSeconds(0.25f); //Small delay to prevent skips
        dialogueRunner.StartDialogue(dialogueName);
    }

    [YarnCommand("SetPortraitSprite")]
    public void SetPortraitSprite(string emotion)
    {
        if (emotion == "Happy") portrait.sprite = pickedChar.happySprite;
        else if (emotion == "Neutral") portrait.sprite = pickedChar.neutralSprite;
        else if (emotion == "Upset") portrait.sprite = pickedChar.upsetSprite;
    }

    [YarnCommand("TogglePortraitAnim")]
    public void TogglePortraitAnim()
    {
        animator.SetBool("isVisible", !animator.GetBool("isVisible"));
    }
}
