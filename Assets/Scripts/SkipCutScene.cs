using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipCutScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button SkipButton;
    public string scenename;
    public bool unloadAllOtherScenes = false;
    void Start()
    {
        SkipButton = GetComponent<Button>();
        SkipButton.onClick.AddListener(Skip);
    }

    // Update is called once per frame
    private void Skip()
    {
        if (unloadAllOtherScenes)
        {
            SceneManager.LoadScene(scenename);
        }
        else
        {
            SceneManager.UnloadSceneAsync(gameObject.scene);
            SceneManager.LoadScene(scenename, LoadSceneMode.Additive);
        }
    }
}
