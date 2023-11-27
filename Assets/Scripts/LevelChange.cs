using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelChange : MonoBehaviour
{
    public string scenename;
    public bool unloadAllOtherScenes = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "character")
        {
            LevelData data = new LevelData();
            data.completed_levels = new();

            // If data doesn't exit yet, create it
            if (!Directory.Exists("save_data"))
            {
                Directory.CreateDirectory("save_data");
            }
            else if (File.Exists("save_data/level_progress.json"))
            {
                data = JsonUtility.FromJson<LevelData>(File.ReadAllText("save_data/level_progress.json"));
            }

            // if not already completed, add this level to the list of completed levels
            if (!data.completed_levels.Contains(gameObject.scene.name))
            {
                data.completed_levels.Add(gameObject.scene.name);
            }

            // overwrite w/ new data
            File.WriteAllText("save_data/level_progress.json", JsonUtility.ToJson(data));

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
}
