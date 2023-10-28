using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject playButton;
    public GameObject controlsButton;
    public GameObject lvlSelectorButton;
    public GameObject quitButton;
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
        SceneManager.LoadScene("NarrativeStart");
    }

    void QuitButtonClicked()
    {
        Application.Quit();
    }

    void LevelSelectorButtonClicked()
    {
        Debug.Log("This function is not implemented yet.");
    }

    void ControlsButtonClicked()
    {
        Debug.Log("This function is not implemented yet.");
    }
}
