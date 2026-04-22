using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    //handles making a fun click sound when button is pressed yay yay!

    [HideInInspector] public Button yourButton;
    [HideInInspector] public AudioManager AudioManager;

    void Start()
    {
        AudioManager = FindAnyObjectByType<AudioManager>();

        yourButton = this.gameObject.GetComponent<Button>();
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        AudioManager.PlaySFXRandomPitch(AudioManager.buttonPress);
    }
}
