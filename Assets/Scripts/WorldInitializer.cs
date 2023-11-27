using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class WorldInitializer : MonoBehaviour
{
    public string startupLevel;
    public bool startupFromSaveData = true;

    // Start is called before the first frame update
    void Start()
    {
        if (startupFromSaveData)
        {
            LevelData data = JsonUtility.FromJson<LevelData>(File.ReadAllText("save_data/level_progress.json"));
            SceneManager.LoadScene(data.completed_levels[data.levelIndex], LoadSceneMode.Additive);
        }
        else
        {
            if (startupLevel == gameObject.scene.name)
            {
                Debug.LogWarning("You can't set the startup level of this scene to itself!");
                return;
            }
            SceneManager.LoadScene(startupLevel, LoadSceneMode.Additive);
        }
    }
}
