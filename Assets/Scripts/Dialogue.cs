using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterMovement CM;
    public string Level;

    void Start()
    {
        if (Level == "test 1.0")
        {
            CM.QueueDialogue("", 1f);

            CM.QueueDialogue("Where the frick I am ?", 2f);
            CM.QueueDialogue("I-I can¡¯t move! ", 1f);
            CM.QueueDialogue("The air itself is like tar...", 2f);
            CM.QueueDialogue("", 3f);
            CM.QueueDialogue("Wait... what¡¯s this? It¡¯s beautiful...", 4f);
            CM.QueueDialogue("", 5f);
            CM.QueueDialogue("Whoa! This sensation...", 2f);
            CM.QueueDialogue("Let me try something...", 4f);
            CM.QueueDialogue("", 4f);
            CM.QueueDialogue("Whoa!!!", 1f);
        }
        else
        {
            if(Level == "test 1.1")
            {
                CM.QueueDialogue("", 1f);

                CM.QueueDialogue("This power¡­ I can rotate these rooms!", 2f);
                CM.QueueDialogue("Let¡¯s explore these rooms with this power...", 2f);
                CM.QueueDialogue("Perhaps I can escape.", 1f);
            }

        }

       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
