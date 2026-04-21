using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    /*
     * Script responsible for handling buttons on game menus, 
     * primarily for switching scenes or bringing up UI elements like the pause screen.
     */

    public GameObject OptionsPanel;
    public GameObject InstructionsPanel;

    //loads any scene.
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR //copied from Ford's code, stops the editor running
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); //Closes the application
#endif
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleOptionsMenu()
    {
        OptionsPanel.SetActive(!OptionsPanel.active);
    }
    public void ToggleInstructionsPanel()
    {
        InstructionsPanel.SetActive(!InstructionsPanel.active);
    }

}
