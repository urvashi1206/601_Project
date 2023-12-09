using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseEnd : MonoBehaviour
{
    // Start is called before the first frame update
    private Button Close;
    public Animator CurtainAnim;
    void Start()
    {
        Close = GetComponent<Button>();
        Close.onClick.AddListener(EndCutscene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EndCutscene()
    {
        Debug.Log("Click happens");

        CurtainAnim.SetTrigger("CloseNewspaper");

        SceneManager.LoadScene("MainMenu");
    }
}
