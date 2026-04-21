using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //needed to interact with ui and text elements
using UnityEngine.SceneManagement; //needed to switch between scenes

public class PauseMenuHandler : MonoBehaviour
{
    /*
        Goal of the script:
        Runs the functions of the Pause UI.
        This function does not work when the game is over

        Functions of the script:
        1. Public Variables
            Snap sound effects for clicking a button
            AudioSource for playing those sfxes

            Paused bool, determines whether the game is paused or not
            PauseMenuUI GameObject, determines what the Pause Menu UI is
        
        2. Start Function
            Ensures this knows what it's audio source is
            Ensures the game starts unpaused

        3. Update function
            If the pause menu is not active, presing Esc will activate the Pause function
            If the pause menu is active, allows the player to return to gameplay just by pressing Space

        3. Pause Function
            Activates when escape is pressed
            Pauses the game. Click Sound, then freezes time and activeate the pause menu UI.
            
        4. Button Resume function
            Activates when the ButtonResume is pressed
            Click Sound
            Deactivates the pause menu and returns time to regular speed

        5. Button Main Menu function
            Activates when the ButtonMainMenu is pressed
            Click Sound
            Loads the scene "MainMenu" using SceneManager.LoadScene("MainMenu");

        6. Button Exit function
            Activates when the ButtonExit is pressed
            Click Sound
            Exits Game
     */

    //Button sound effects
    public AudioClip[] buttonClickSounds; //Sound Effect Array, plays when the buttons are clicked
    public AudioSource audioSource; //Plays the sfxes

    public bool paused = false; //Determines whether the game is paused or not
    public GameObject pauseMenuUI; //Determines what the Pause Menu UI is

    // Informs this object of it's AudioSource
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //Ensures the audioSource is the AudioSource of the object

        //Ensures the game starts unpaused
        paused = false; //tells this script the game is not paused
        Time.timeScale = 1f; //resumes time
        pauseMenuUI.SetActive(false); //deactivates the pause menu
    }

    // Update is called once per frame
    void Update()
    {
        if (paused == false) { //If the game is not paused
            if (Input.GetKeyDown(KeyCode.Escape)) //If Cancel (Escape) is pressed, pause the game
            {
                Pause();
                Debug.Log("Pause Pressed");
            }
        }
        else //If the game is paused
        { 

            if (Input.GetKeyDown(KeyCode.Escape)) //If Cancel (Escape) is pressed, return player to the game
            {
               ButtonResume();
               Debug.Log("Resume Pressed");
            }
            if (Input.GetKeyDown(KeyCode.Space)) //If Jump (Space) is pressed, return player to the game
            {
                ButtonResume();
                Debug.Log("Resume Pressed");
            }
        }

    }

    // Activates when the escape key is pressed
    // Pauses the game. Click Sound, then freezes time and activeate the pause menu UI.
    public void Pause() 
    {
        if (paused == false)
        {
            //Click SFX
            int buttonClickSoundsIndex = Random.Range(0, buttonClickSounds.Length); //.Length is exclusive, determines which sfx to play
            if (audioSource != null) audioSource.PlayOneShot(buttonClickSounds[buttonClickSoundsIndex], 0.7F); //Plays a cool sfx
            Debug.Log("Click");

            //Pausing
            paused = true; //tells this script the game is paused
            Time.timeScale = 0.000000f; //stops time
            pauseMenuUI.SetActive(true); //activates the pause menu
            Debug.Log("Paused");
        }
    }

    // Activates when the ButtonResume is pressed, returns the player to the game, and deactivates the pause UI
    public void ButtonResume()
    {
        if (paused == true) {
            //Click SFX
            int buttonClickSoundsIndex = Random.Range(0, buttonClickSounds.Length); //.Length is exclusive, determines which sfx to play
            if (audioSource != null) audioSource.PlayOneShot(buttonClickSounds[buttonClickSoundsIndex], 0.7F); //Plays a cool sfx
            Debug.Log("Clack");

            //Resuming
            Time.timeScale = 1f; //resumes time
            pauseMenuUI.SetActive(false); //deactivates the pause menu
            paused = false; //tells this script the game is not paused
            Debug.Log("Unpaused");
        }
    }

    // Activates when the ButtonMainMenu is pressed, returns the player to the main menu.
    public void ButtonMainMenu()
    {
        //Click SFX
        int buttonClickSoundsIndex = Random.Range(0, buttonClickSounds.Length); //.Length is exclusive, determines which sfx to play
        if (audioSource != null) audioSource.PlayOneShot(buttonClickSounds[buttonClickSoundsIndex], 0.7F); //Plays a cool sfx

        SceneManager.LoadScene("MainMenu"); //Loads the scene titled "MainMenu". uses UnityEngine.SceneManagement;
    }

    //When run, the application will be exited.
    public void ButtonExit()
    {
        //Click SFX
        int buttonClickSoundsIndex = Random.Range(0, buttonClickSounds.Length); //.Length is exclusive, determines which sfx to play
        if (audioSource != null) audioSource.PlayOneShot(buttonClickSounds[buttonClickSoundsIndex], 0.7F); //Plays a cool sfx

#if UNITY_EDITOR //copied from Ford's code, stops the editor running
            UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); //Closes the application
#endif
    }

}
