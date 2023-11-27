using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldInitializer : MonoBehaviour
{
    public string startupLevel;
    public static WorldInitializer Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        enabled = false; // only want to update once
        if (startupLevel == gameObject.scene.name)
        {
            Debug.LogWarning("You can't set the startup level of this scene to itself!");
            return;
        }
        SceneManager.LoadScene(startupLevel, LoadSceneMode.Additive);
    }

    public static void LoadStartLevel(string name)
    {
        Instance.startupLevel = name;
    }
}
