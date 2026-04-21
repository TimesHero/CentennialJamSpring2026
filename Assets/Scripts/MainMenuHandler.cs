using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //needed to interact with ui and text elements
using UnityEngine.SceneManagement; //needed to switch between scenes
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    /*
        Goal of the script:
        This script will be handled by the MainMenuHandler object, referenced to by the buttons on the main menu
        Run functions of the main menu, including starting and quitting the game.
     
        Functions of the script:
        
        Public Variables:
            Snap sound effects for clicking a button
            AudioSource for playing those sfxes

            None required yet, will eventually need to save score.

        0. Start Script, ensures the audioSource is the AudioSource of the object
        1. If the Start button is pressed, switch to the game's scene (runs SceneManager.LoadScene("MainGame");)
              public void StartGame()
            Also runs updateHighScore
            Also runs the controls or credits button script based on the high score being 0 or not

        1.5 Update Function
            If Space is pressed, that also will load the Main Game
        2. If the Exit button is pressed, exit out of the program (runs Application.Quit();)
              public void ExitGame()

        3. If the Controls button is pressed, display Controls, and activate the Credits button

        4. If the Credits button is pressed, display Credits, and activate the High-Score button

        5. If the High-Score button is pressed, display High Score, and activate the Controls button

        6. updateHighScore updates the high score page appropriately

        7. clearHighScore clears the high score player prefs, then runs updateHighScore



       
     */

    //Button sound effects
    public AudioClip[] buttonClickSounds; //Sound Effect Array, plays when the buttons are clicked
    public AudioSource audioSource; //Plays the sfxes

    //High Score
    public TextMeshProUGUI highScoreText; //Displays the HighScore, HighPlayerName, and HighWave
    public float[] RankBarriers; //Determines the scores to beat for each rank
    //0 = Participation
    //1 = Bronze (W5)
    //2 = Silver (W10)
    //3 = Gold (W15)
    //4 = Dev Score + 1
    public GameObject[] RankSprites; //Determines the game objects displaying your rank

    public float highScoreDefault = 0;
    public int highWavesDefault = 0;
    public string highPlayerNameDefault = "HIGH";
    public string mainGameSceneName = "GamePlayScene";

    //Buttons
    public GameObject HiScoreButton;
    public GameObject HiScorePaper;
    public GameObject ControlsButton;
    public GameObject ControlsPaper;
    public GameObject CreditsButton;
    public GameObject CreditsPaper;

    //Settings
    public bool showWaves = false;


    //happens before the first frame
    //ensures this knows what it's audio source is
    //also updates the high score
    //and activates the appropriate thing
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //Ensures the audioSource is the AudioSource of the object
        updateHighScore();
        if (PlayerPrefs.GetFloat("HighScore") < 1) { LoadControls(); }
        else { LoadHiScore(); }
    }

    //When run, the scene will be switched to the gameplay scene.
    public void StartGame()
    {
        //Click SFX
        int buttonClickSoundsIndex = Random.Range(0, buttonClickSounds.Length); //.Length is exclusive, determines which sfx to play
        audioSource.PlayOneShot(buttonClickSounds[buttonClickSoundsIndex], 0.7F); //Plays a cool sfx

        //Scene Loading
        SceneManager.LoadScene(mainGameSceneName); //Loads the scene titled "MainGame". uses UnityEngine.SceneManagement;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") > 0) //If Jump (Space) is held, start the game
        {
            //Click SFX
            int buttonClickSoundsIndex = Random.Range(0, buttonClickSounds.Length); //.Length is exclusive, determines which sfx to play
            audioSource.PlayOneShot(buttonClickSounds[buttonClickSoundsIndex], 0.7F); //Plays a cool sfx

            //Scene Loading
            SceneManager.LoadScene(mainGameSceneName); //Loads the scene titled "MainGame". uses UnityEngine.SceneManagement;

            
        }
        Debug.Log(PlayerPrefs.GetString("HighPlayerName"));
    }

    //When run, the application will be exited.
    public void ExitGame() 
    {
        //Click SFX
        int buttonClickSoundsIndex = Random.Range(0, buttonClickSounds.Length); //.Length is exclusive, determines which sfx to play
        audioSource.PlayOneShot(buttonClickSounds[buttonClickSoundsIndex], 0.7F); //Plays a cool sfx

#if UNITY_EDITOR //copied from Ford's code, stops the editor running
            UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); //Closes the application
#endif
    }

    //Controls, Credits, and HighScore buttons
    public void ButtonControls() 
    {
        //Click SFX
        int buttonClickSoundsIndex = Random.Range(0, buttonClickSounds.Length); //.Length is exclusive, determines which sfx to play
        audioSource.PlayOneShot(buttonClickSounds[buttonClickSoundsIndex], 0.7F); //Plays a cool sfx

        LoadControls();
    }
    public void LoadControls() 
    {
        //ControlsPaper and CreditsButton should be active, HighScorePaper and Controls button should not be active.

        //Load
        CreditsButton.SetActive(true);
        ControlsPaper.SetActive(true);

        //Unload
        HiScorePaper.SetActive(false);
        CreditsPaper.SetActive(false);
        ControlsButton.SetActive(false);
    }
    public void ButtonCredits()
    {
        //Click SFX
        int buttonClickSoundsIndex = Random.Range(0, buttonClickSounds.Length); //.Length is exclusive, determines which sfx to play
        audioSource.PlayOneShot(buttonClickSounds[buttonClickSoundsIndex], 0.7F); //Plays a cool sfx

        LoadCredits();
    }
    public void LoadCredits()
    {
        //Paper Should be active, button should not be.

        //Load
        HiScoreButton.SetActive(true);
        CreditsPaper.SetActive(true);

        //Unload
        ControlsPaper.SetActive(false);
        HiScorePaper.SetActive(false);
        CreditsButton.SetActive(false);
    }
    public void ButtonHiScore()
    {
        //Click SFX
        int buttonClickSoundsIndex = Random.Range(0, buttonClickSounds.Length); //.Length is exclusive, determines which sfx to play
        audioSource.PlayOneShot(buttonClickSounds[buttonClickSoundsIndex], 0.7F); //Plays a cool sfx

        LoadHiScore();
    }
    public void LoadHiScore()
    {
        //Paper Should be active, button should not be.

        //Load
        ControlsButton.SetActive(true);
        HiScorePaper.SetActive(true);

        //Unload
        CreditsPaper.SetActive(false);
        ControlsPaper.SetActive(false);
        HiScoreButton.SetActive(false);
    }
    /////////////////////



    //6. updateHighScore updates the high score page appropriately
    public void updateHighScore()
    {
        if (PlayerPrefs.GetFloat("HighScore") < 1) { PlayerPrefs.SetFloat("HighScore", 0); }
        /*
            //code from PointsHandler
            PlayerPrefs.SetFloat("HighScore", totalPoints);
            PlayerPrefs.SetInt("HighWaves", waves);
            PlayerPrefs.SetString("HighPlayerName", playerName);
            nameEntry.SetActive(true);
         */

        //Step 1: For each Rank Sprite in RankSprites, deactivate them
        for (int i = 0; i < RankSprites.Length; i++)
        { RankSprites[i].SetActive(false); }



        //Step 2: Update the HighScoreText with the HighPlayerName, HighWaves, and HighScore
        string HighPlayerName = PlayerPrefs.GetString("HighPlayerName");
        //If the length of the HighPlayerName is less than 1, set it to the default.
        if (HighPlayerName.Length < 1)
        {
            PlayerPrefs.SetString("HighPlayerName", highPlayerNameDefault);
        }

        //Step 3: Define the HighScore as a variable
        float HighScore = PlayerPrefs.GetFloat("HighScore");
        int HighWaves = PlayerPrefs.GetInt("HighWaves");
        HighPlayerName = PlayerPrefs.GetString("HighPlayerName");

        //Update the highScoreText.Text
        if (showWaves) { highScoreText.text = HighPlayerName + " - W" + HighWaves + " - " + HighScore; }
        else { highScoreText.text = HighPlayerName + " - " + HighScore; }


        //Step 4: For Rank Barrier 0, check if the HighScore is higher or equal, then spawn the first RankSprite.
        if (HighScore >= RankBarriers[0])
        {
            RankSprites[0].SetActive(true);
        }

        //Step 5: For each Rank Barrier (aside from 0), check if the HighScore is higher or equal, then dectivate the previous one and spawn the next one. 
        for (int i=1; i<RankSprites.Length; i++)
        if (HighScore >= RankBarriers[i])
        {
            RankSprites[(i-1)].SetActive(false);
            RankSprites[i].SetActive(true);
        }

    }

    //7. clearHighScore clears the high score player prefs, then runs updateHighScore
    public void clearHighScore()
    {
        //Click SFX
        int buttonClickSoundsIndex = Random.Range(0, buttonClickSounds.Length); //.Length is exclusive, determines which sfx to play
        audioSource.PlayOneShot(buttonClickSounds[buttonClickSoundsIndex], 0.7F); //Plays a cool sfx

        PlayerPrefs.DeleteAll(); //Deletes all keys
        if (PlayerPrefs.GetString("HighPlayerName").Length < 1)
        {
            PlayerPrefs.SetString("HighPlayerName", highPlayerNameDefault); //Sets the PlayerName to default
        } 
        updateHighScore(); //Updates the HighScore
    }
}
