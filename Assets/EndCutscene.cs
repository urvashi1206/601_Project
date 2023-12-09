using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animationInfo;
        animationInfo = anim.GetCurrentAnimatorStateInfo(0);
        if ((animationInfo.normalizedTime > 1.0f) && (animationInfo.IsName("Blackout")))//animation end
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
