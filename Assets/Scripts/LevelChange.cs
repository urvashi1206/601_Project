using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
