using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public delegate void DialogueCallback();

    // Start is called before the first frame update
    float timer = 0;
    public GameObject head, forearm_l, forearm_r, upperarm_l, upperarm_r, shoulder_l, shoulder_r, spine003,spine, metarig;
    public GameObject EndSpot;
    public bool onrotation;
    public ArrowMovement[] xyzObjects;
    public GameObject playerUI;

    GameObject uiSpeechBubble;
    GameObject screenFocusFade;

    static CharacterMovement playerScript; // singleton

    // dialogue system
    struct Dialogue
    {
        public string message;
        public float duration;
        public bool isPause;
        public DialogueCallback callback;

        public Dialogue(string message, float duration, DialogueCallback callback)
        {
            this.message = message;
            this.duration = duration;
            this.isPause = false;
            this.callback = callback;
        }
    }
    Queue<Dialogue> dialogueQ = new();
    float dialogueTimer = 0f;
    bool dialoguePause = true;
    float clickProtectionTime = 0.5f;

    void Start()
    {
        head = GameObject.Find("head");
        upperarm_l = GameObject.Find("upper_arm.L");
        upperarm_r = GameObject.Find("upper_arm.R");
        shoulder_l = GameObject.Find("shoulder.L");
        shoulder_r = GameObject.Find("shoulder.R");
        spine003 = GameObject.Find("spine.003");
        spine = GameObject.Find("spine");
        metarig = GameObject.Find("metarig.001");
        forearm_l = GameObject.Find("forearm.L"); 
        forearm_r = GameObject.Find("forearm.R");

        EndSpot = GameObject.Find("EndingSpot");
        uiSpeechBubble = playerUI.transform.Find("SpeechBubble").gameObject;
        screenFocusFade = playerUI.transform.Find("Gradient").gameObject;

        //character facing endpoint
        look_at_endpoint();
        onrotation = false;
        xyzObjects = GameObject.FindObjectsOfType<ArrowMovement>();
        //(5.366f, -95.797f, -6.632) (5.366f, 95.797f, 6.632)
        upperarm_l.transform.rotation = shoulder_l.transform.rotation * Quaternion.Euler(5.366f, -95.797f, 80);
        upperarm_r.transform.rotation = shoulder_r.transform.rotation * Quaternion.Euler(5.366f, 95.797f, -80);
        forearm_l.transform.rotation = upperarm_l.transform.rotation * Quaternion.Euler(90, 150, 150);
        forearm_r.transform.rotation = upperarm_r.transform.rotation * Quaternion.Euler(90, -150, -150);
        //Debug.Log(metarig.transform.rotation.eulerAngles);
        //Debug.Log(transform.rotation.eulerAngles);
        //Debug.Log(upperarm_l.transform.rotation.eulerAngles);
    }

    public void look_at_endpoint()
    {
        Vector3 dir = EndSpot.transform.position;
        transform.LookAt(new Vector3(dir.x, transform.position.y, dir.z));
        head.transform.LookAt(dir);
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody>().velocity.magnitude < 0.001 && !onrotation)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
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
        if (dialoguePause ? dialogueTimer <= 0f  : dialogueTimer <= 0f && Input.GetMouseButtonDown(0))
        {
            if (dialogueQ.TryDequeue(out Dialogue newDialogue))
            {
                dialogueTimer = newDialogue.duration;
                dialoguePause = newDialogue.isPause;
                if (newDialogue.callback != null)
                    newDialogue.callback();

                uiSpeechBubble.SetActive(newDialogue.message.Length > 0);
                uiSpeechBubble.GetComponentInChildren<TextMeshProUGUI>().SetText(newDialogue.message);
            }
            else
            {
                uiSpeechBubble.SetActive(false);
                dialoguePause = true;
            }
        }
        else // udpate speech bubble location as character moves around
        {
            uiSpeechBubble.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
    }

    /// <summary>
    /// Queue up a clickable dialogue text for display.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="displayCallback">The callback to invoke when this dialogue is displayed.</param>
    public void QueueDialogue(string message, DialogueCallback displayCallback = null)
    {
        dialogueQ.Enqueue(new Dialogue(message, clickProtectionTime, displayCallback));
    }

    /// <summary>
    /// Queue up a timed barrier between queued dialogues.
    /// </summary>
    /// <param name="duration">How long, in seconds, to pause dialogue for.</param>
    /// <param name="callback">The callback to invoke when the dialogue queue reaches this dialogue barrier.</param>
    public void QueueDialoguePause(float duration, DialogueCallback callback = null)
    {
        Dialogue newD = new Dialogue("", duration, callback);
        newD.isPause = true;
        dialogueQ.Enqueue(newD);
    }

    public void EnableVisualFocus(Vector3 focusPos)
    {
        screenFocusFade.GetComponent<RectTransform>().position = focusPos;
        StartCoroutine(FocusAnim(false));
    }
    public void DisableVisualFocus()
    {
        StartCoroutine(FocusAnim(true));
    }

    public static CharacterMovement Get()
    {
        if (playerScript == null)
        {
            playerScript = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
        }
        return playerScript;
    }

    private IEnumerator FocusAnim(bool reverse)
    {
        Image img = screenFocusFade.GetComponent<Image>();
        Color imgCol = img.color;

        for (float i = 0f; i < 1f; i += Time.deltaTime * 2f)
        {
            imgCol.a = reverse ? Mathf.Lerp(0.75f, 0f, i) : Mathf.Lerp(0f, 0.75f, i);
            img.color = imgCol;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
