using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public struct LevelData
{
    public string name;
}

public class LevelSelectManager : MonoBehaviour
{
    float maxX = 800f;
    float maxY = 350f;
    int rowSize = 5;
    int columnSize = 5;

    public GameObject backToMenu;
    public GameObject mainMenu;

    GameObject[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        Transform lvlParent = transform.Find("Levels");
        if (File.Exists("SaveData/completed_evels.json"))
        {
            string[] json = File.ReadAllText("SaveData/completed_evels.json").Split("}");
            for (int i = 0; i < json.Length - 1; i++) // the last element is an empty string so omit it
            {
                if (i < lvlParent.childCount)
                {
                    // Get the ll data
                    LevelData data = JsonUtility.FromJson<LevelData>(json[i] + "}");

                    // Set the currently selected level button to reflect this data
                    Transform child = lvlParent.GetChild(i);
                    child.GetChild(0).GetComponent<TextMeshPro>().text = data.name;
                    child.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("World");
                        WorldInitializer.LoadStartLevel(data.name);
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
