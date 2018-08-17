using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialogue : MonoBehaviour {

    Text text;
    public int index;
    public string[] dialogue;
    public bool dialogueDone;

    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && !dialogueDone)
        {
            UpdateText();
        }
        if (index == dialogue.Length)
        {
            dialogueDone = true;
        }
    }

    void UpdateText()
    { 
        text.text = dialogue[index];
        index++;
    }

}
