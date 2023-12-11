using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public string scenename;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collide");
        if (collision.gameObject.name == "character")
        {
            SceneManager.UnloadSceneAsync(gameObject.scene);
            SceneManager.LoadScene(scenename, LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log(collision.gameObject.name);
        }
}

    }