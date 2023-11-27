using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public struct LevelData
{
    public List<string> completed_levels;
}

public class LevelSelectManager : MonoBehaviour
{
    public GameObject backToMenu;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists("save_data/level_progress.json"))
        {
            Transform lvlParent = transform.Find("Levels");
            LevelData data = JsonUtility.FromJson<LevelData>(File.ReadAllText("save_data/level_progress.json"));
            for (int i = 0; i < data.completed_levels.Count; i++) // the last element is an empty string so omit it
            {
                if (i < lvlParent.childCount)
                {
                    string lvlName = data.completed_levels[i];
                    Transform child = lvlParent.GetChild(i);
                    child.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("World");
                        WorldInitializer.LoadStartLevel(lvlName);
                    });
                }
            }
        }

        backToMenu.GetComponent<Button>().onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
        });
    }
}
