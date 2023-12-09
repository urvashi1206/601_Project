using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Lvl7 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CharacterMovement.Get().QueueDialogue("I'm close to the exit... I can feel it!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
