using System;
using Unity.VisualScripting;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogue;

    public void OnButtonPress(string NodeName)
    {
        dialogue.StartDialogue(NodeName);
    }

    public void OnDialogueStarted()
    {
        gameObject.SetActive(false);
    }

    public void OnDialogueEnded()
    {
        gameObject.SetActive(true);
    }
}
