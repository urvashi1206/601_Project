using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update

    public string Level;
    public GameObject weirdDevice;
    public GameObject firstArrowControl;

    void Start()
    {
        CharacterMovement CM = CharacterMovement.Get();

        if (Level == "test 1.0")
        {
            CM.QueueDialoguePause(1f);

            CM.QueueDialogue("Ouch, that was quite the fall... Where am I?");
            CM.QueueDialogue("I-I can't move! ");
            CM.QueueDialogue("The air itself is like tar...");
            CM.QueueDialoguePause(2f);
            CM.QueueDialogue("Wait... what's this? It's beautiful...");

            // Direct player's attention toward the device
            CM.QueueDialoguePause(5f, () => CM.EnableVisualFocus(
                Camera.main.WorldToScreenPoint(weirdDevice.transform.position)));

            CM.QueueDialogue("Whoa! This sensation...", () => CM.DisableVisualFocus());
            CM.QueueDialogue("Let me try something...");

            // Direct the player's attention toward the arrow control
            CM.QueueDialoguePause(4f, () => CM.EnableVisualFocus(
                Camera.main.WorldToScreenPoint(firstArrowControl.transform.position)));

            CM.QueueDialogue("Whoa!!!", () => CM.DisableVisualFocus());
        }
        else
        {
            if(Level == "test 1.1")
            {
                CM.QueueDialoguePause(1f);

                CM.QueueDialogue("This power... I can rotate these rooms!");
                CM.QueueDialogue("Let's explore these rooms with this power...");
                CM.QueueDialogue("Perhaps I can escape.");
            }
        }
    }
}
