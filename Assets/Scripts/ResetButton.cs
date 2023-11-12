using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private Button resetButton;
    public GameObject character;
    public GameObject scene;
    public Quaternion character_rotation;
    public Vector3 character_position;
    public Quaternion scene_rotation;
    public Vector3 scene_position;
    // Start is called before the first frame update
    void Start()
    {
        resetButton = GetComponent<Button>();
        resetButton.onClick.AddListener(ResetRoomRotation);
        resetButton.onClick.AddListener(ResetCharacterPosition);
        
        //record starting status
        character = GameObject.Find("character");
        character_rotation = character.transform.rotation;
        character_position = character.transform.position;
        scene = GameObject.Find("Level_Anchor");
        scene_rotation = scene.transform.rotation;
        scene_position = scene.transform.position;
    }

    private void ResetCharacterPosition()
    {
        Debug.Log("ResetCharacterPosition");
        character.transform.position = character_position;
        character.transform.rotation = character_rotation;
        character.GetComponent<CharacterMovement>().look_at_endpoint();
    }

    private void ResetRoomRotation()
    {
        Debug.Log("ResetRoomRotation");
        scene.transform.rotation = scene_rotation;
        scene.transform.position = scene_position;
    }
    
}
