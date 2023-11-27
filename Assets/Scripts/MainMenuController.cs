using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenuController : MonoBehaviour
{
    public GameObject playButton;
    public GameObject controlsButton;
    public GameObject lvlSelectorButton;
    public GameObject quitButton;
    public GameObject lvlSelectCanvas;

    // Start is called before the first frame update
    void Start()
    {
        playButton.GetComponent<Button>().onClick.AddListener(PlayButtonClicked);
        controlsButton.GetComponent<Button>().onClick.AddListener(ControlsButtonClicked);
        lvlSelectorButton.GetComponent<Button>().onClick.AddListener(LevelSelectorButtonClicked);
        quitButton.GetComponent<Button>().onClick.AddListener(QuitButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayButtonClicked()
    {
        // starting a new game; create fresh save data
        if (!Directory.Exists("save_data"))
        {
            Directory.CreateDirectory("save_data");
        }
        LevelData newData = new();
        newData.completed_levels = new();
        newData.completed_levels.Add("test 1.0");
        newData.levelIndex = 0;
        File.WriteAllText("save_data/level_progress.json", JsonUtility.ToJson(newData));

        // Begin the intro cutscene
        SceneManager.LoadScene("NarrativeStart");
    }

    void QuitButtonClicked()
    {
        Application.Quit();
    }

    void LevelSelectorButtonClicked()
    {
        gameObject.SetActive(false);
        lvlSelectCanvas.gameObject.SetActive(true);
    }

    void ControlsButtonClicked()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
