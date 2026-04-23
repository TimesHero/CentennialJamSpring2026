using UnityEngine;
using Yarn.Unity;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class characterKeyValue
{
    public string key;
    public Character value;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private List<characterKeyValue> characters; //List of key and value pairs, workaround to be able to make the dictionary from the inspector
    Dictionary<string, Character> characterDict = new Dictionary<string, Character>();
    private List<string> characterKeys = new List<string>(); //Used to grab random characters

    [SerializeField] private SpriteRenderer portrait;
    [SerializeField] private Animator animator;
    private bool someoneLikes = false;
    private Character pickedChar;
    private Coroutine fadeRoutine;

    void Awake()
    {
        foreach (characterKeyValue pair in characters)
        {
            characterDict.Add(pair.key, pair.value);
            characterKeys.Add(pair.key);
        }
    }

    [YarnCommand("GetRandCharacter")]
    public void GetCharacter()
    {
        if(characterKeys.Count <= 0)
        {
            if(someoneLikes) StartCoroutine(StartDialogue("GameClosingGood"));
            else StartCoroutine(StartDialogue("GameClosingBad"));
        }
        else
        {
            int charIndex = Random.Range(0, characterKeys.Count);
            pickedChar = characterDict[characterKeys[charIndex]];
            characterKeys.RemoveAt(charIndex);
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

    [YarnCommand("GetCharacter")]
    public void GetCharacter(string key)
    {
        try{pickedChar = characterDict[key];} catch (System.Exception){throw;}
    }

    [YarnCommand("FadeScreen")]
    public void FadeScreen(bool fadeIn, float duration)
    {
        if(fadeRoutine != null) StopCoroutine(fadeRoutine);

        if(fadeIn) fadeRoutine = StartCoroutine(FadeScreenIn(duration));
        else fadeRoutine = StartCoroutine(FadeScreenOut(duration));
    }

    private IEnumerator FadeScreenIn(float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(1, 0, elapsed/duration);
            yield return null;
        }
        fadeCanvas.alpha = 0;
    }

    private IEnumerator FadeScreenOut(float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0, 1, elapsed/duration);
            yield return null;
        }
        fadeCanvas.alpha = 1;
    }
}
