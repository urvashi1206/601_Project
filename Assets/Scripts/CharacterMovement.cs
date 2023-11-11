using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = 0;
    public GameObject head;
    public GameObject EndSpot;
    public bool onrotation;
    public ArrowMovement[] xyzObjects;
    public GameObject playerUI;

    GameObject uiSpeechBubble;

    // dialogue system
    struct Dialogue
    {
        public string message;
        public float duration;

        public Dialogue(string message, float duration)
        {
            this.message = message;
            this.duration = duration;
        }
    }
    Queue<Dialogue> dialogueQ = new();
    float dialogueTimer = 0f;

    void Start()
    {
        head = GameObject.Find("head");
        EndSpot = GameObject.Find("EndingSpot");
        uiSpeechBubble = playerUI.transform.Find("SpeechBubble").gameObject;

        //character facing endpoint
        Vector3 dir = EndSpot.transform.position;
        transform.LookAt(new Vector3(dir.x, transform.position.y, dir.z));
        head.transform.LookAt(dir);
        onrotation = false;
        xyzObjects = GameObject.FindObjectsOfType<ArrowMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody>().velocity.magnitude < 0.001 && !onrotation)
        {
            timer += Time.deltaTime;
            //UnityEngine.Debug.Log(timer);
        }
        else
        {
            timer = 0;
            //UnityEngine.Debug.Log("0");
        }
        if(timer > 1)
        {
            if (Vector3.Dot(transform.up, Vector3.up) < 0.99)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<BoxCollider>().enabled = false;
                //character stand up
                transform.position += new Vector3(0, 1, 0);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                //character facing endpoint
                Vector3 dir = EndSpot.transform.position;
                transform.LookAt(new Vector3(dir.x, transform.position.y, dir.z));
                head.transform.LookAt(dir);
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<BoxCollider>().enabled = true;
            }
            foreach (ArrowMovement xyzobject in xyzObjects)
            {
                xyzobject.GetComponent<BoxCollider>().enabled = true;
            }
            timer = 0;
        }

        // Dialogue timer handling
        dialogueTimer -= Time.deltaTime;

        if (dialogueTimer <= 0f)
        {
            if (dialogueQ.TryDequeue(out Dialogue newDialogue))
            {
                dialogueTimer = newDialogue.duration;
                uiSpeechBubble.SetActive(true);
                uiSpeechBubble.GetComponentInChildren<TextMeshProUGUI>().SetText(newDialogue.message);
            }
            else
            {
                uiSpeechBubble.SetActive(false);
            }
        }
        else // udpate speech bubble location as character moves around
        {
            uiSpeechBubble.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
    }

    public void QueueDialogue(string message, float duration)
    {
        dialogueQ.Enqueue(new Dialogue(message, duration));
    }
}
