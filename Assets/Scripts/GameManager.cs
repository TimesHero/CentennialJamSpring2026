using UnityEngine;
using Yarn.Unity;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private List<Character> characters;


    [YarnCommand("GetCharacter")]
    public void GetCharacter()
    {
        Character pickedChar = characters[UnityEngine.Random.Range(0, characters.Count)];
        Debug.Log($"Picked {pickedChar.name}");
        StartCoroutine(StartDialogue(pickedChar.name));
    }

    //Added since ending a dialogue and starting a new one in the same frame skips lines
    private IEnumerator StartDialogue(string charName)
    {
        yield return new WaitForSeconds(0.25f);
        dialogueRunner.StartDialogue($"{charName}DialogueStart");
    }
}
