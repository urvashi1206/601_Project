using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldInitializer : MonoBehaviour
{
    public string startupLevel;

    // Start is called before the first frame update
    void Start()
    {
        if (startupLevel == gameObject.scene.name)
        {
            Debug.LogWarning("You can't set the startup level of this scene to itself!");
            return;
        }
        SceneManager.LoadScene(startupLevel, LoadSceneMode.Additive);
    }
}
