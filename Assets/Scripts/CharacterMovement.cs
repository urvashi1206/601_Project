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
    public float timer = 0, g_timer = 0, l_timer = 0;
    public Vector3 start, end;
    public Vector2 start2, end2;
    float angle;
    public GameObject head;
    public GameObject EndSpot;
    public bool onrotation, ongetup, onlookat;
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
        head = GameObject.Find("MCH-ROT-head");
        uiSpeechBubble = playerUI.transform.Find("SpeechBubble").gameObject;
        screenFocusFade = playerUI.transform.Find("Gradient").gameObject;

        //character facing endpoint
        look_at_endpoint();
        onrotation = false;
        ongetup = false;
        onlookat = false;
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
        if (onlookat)
        {
            l_timer += Time.deltaTime;
            float t = 180 * l_timer / (2 * angle);
            float t_angle = angle * t;
            Debug.Log(t_angle);
            Vector2 t_pointer = new Vector2(start2.x*Mathf.Cos(t_angle*Mathf.Deg2Rad)-start2.y*Mathf.Sin(t_angle * Mathf.Deg2Rad),start2.x * Mathf.Sin(t_angle * Mathf.Deg2Rad) + start2.y * Mathf.Cos(t_angle * Mathf.Deg2Rad));
            Debug.Log(t_pointer);
            transform.LookAt(new Vector3(t_pointer.x+transform.position.x, transform.position.y, t_pointer.y + transform.position.z));
            //Debug.Log(t);
            //transform.LookAt(end);
            //Debug.Log("hi");
            if (l_timer > 2*angle/180)
            {
                l_timer = 0;
                onlookat = false;
                transform.LookAt(end);
                foreach (ArrowMovement xyzobject in xyzObjects)
                {
                    xyzobject.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
        if(GetComponent<Rigidbody>().velocity.magnitude < 0.001 && !onrotation)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        if(timer > 0.25)
        {
            if (Vector3.Dot(transform.up, Vector3.up) < 0.99)
            {
                if(ongetup)
                {
                    g_timer ++;
                    if(g_timer > 9)
                    {
                        g_timer = 0;
                        ongetup = false;
                        anim.SetTrigger("IsLanded");
                        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                        //character facing endpoint
                        Vector3 dir = EndSpot.transform.position;
                        start = transform.forward;
                        end = new Vector3(dir.x, transform.position.y, dir.z);
                        start2 = new Vector2(start.x, start.z);
                        end2 = new Vector2(dir.x-transform.position.x, dir.z-transform.position.z);
                        angle = Vector2.Angle(start2, end2);
                        Debug.Log(angle);
                        //Debug.Log(start2);
                        //Debug.Log(end2);
                        onlookat = true;
                        //transform.LookAt(new Vector3(dir.x, transform.position.y, dir.z));
                        //head.transform.LookAt(dir);
                    }
                }
                else
                {
                    anim.SetTrigger("IsLanded");
                    ongetup = true;
                    GetComponent<Rigidbody>().isKinematic = true;
                    GetComponent<BoxCollider>().enabled = false;
                    //character stand up
                    //transform.position += new Vector3(0, 1, 0);
                    //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                    GetComponent<Rigidbody>().isKinematic = false;
                    GetComponent<BoxCollider>().enabled = true;
                    Debug.Log("Should run");
                }
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
