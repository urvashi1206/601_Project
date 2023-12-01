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
    public GameObject head;
    public GameObject EndSpot;
    public bool onrotation;
    public ArrowMovement[] xyzObjects;
    public GameObject playerUI;
    public Animator anim;

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
        EndSpot = GameObject.Find("EndingSpot");
        uiSpeechBubble = playerUI.transform.Find("SpeechBubble").gameObject;
        screenFocusFade = playerUI.transform.Find("Gradient").gameObject;

        //character facing endpoint
        look_at_endpoint();
        onrotation = false;
        xyzObjects = GameObject.FindObjectsOfType<ArrowMovement>();
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
                anim.SetTrigger("IsLanded");
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
