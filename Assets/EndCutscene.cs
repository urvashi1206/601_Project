using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCutscene : MonoBehaviour
{
    public Animator anim;
    public Animator CurtainAnim;

    [SerializeField] private Button ClickEvent;
    // Start is called before the first frame update
    void Start()
    {
        ClickEvent = GameObject.Find("Newspaper").GetComponent<Button>();
        ClickEvent.onClick.AddListener(IsClicked);
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animationInfo;
        animationInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animationInfo.normalizedTime > 1.0f)//check if animation has end
        {
            Debug.Log("Anim end.");

        }
    }

    private void IsClicked()
    {
        // Click the newspaper and then set the fade_out parameter to true
        Debug.Log("Click happens");

        CurtainAnim.SetTrigger("ClickNewspaper");

        SceneManager.LoadScene("MainMenu");
    }
}
