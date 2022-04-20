using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [HideInInspector] public Dialogue dialogue;
    [HideInInspector] public DialogueData dialogueData;

    [HideInInspector] public Line line;
    [HideInInspector] public DialogueBoxData boxData;

    [HideInInspector] public bool finished = false;
    [HideInInspector] public bool started = false;

    [SerializeField] private AudioSource[] blipSound;

    bool autoClick = false;
    bool skipClick = false;
    bool isTyped = false;

    int nextLineID = 0;
    int currentSound = 0;
    int blipArrayLength;
    string sentence;

    // Start is called before the first frame update
    void Awake()
    {
        blipArrayLength = blipSound.Length;
        started = false;
    }

    private void Update()
    {
        if (!line.hasResponses || (line.hasResponses && line.responses[0].connectedID == -1))
        {
            if (autoClick || (skipClick && isTyped))
            {
                skipClick = false;
                autoClick = false;
                isTyped = false;
                boxData.dialogueBox.SetActive(false);
                nextLineID++;
                if (line.hasResponses && line.responses[0].connectedID == -1)
                {
                    nextLineID = -1;
                }
                DisplayNextSentence(nextLineID);
            }
            else if (skipClick && !isTyped)
            {
                skipClick = false;
                autoClick = false;
                isTyped = false;
                StopAllCoroutines();
                StartCoroutine(DisplayWholeText());
            }
        }
        else
        {
            if ((skipClick && !isTyped) || isTyped)
            {
                skipClick = false;
                autoClick = false;
                isTyped = false;
                DisplayButtons(line);
                StopAllCoroutines();
                StartCoroutine(DisplayWholeText());
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        finished = false;
        started = true;
        nextLineID = 0;
        this.dialogue = dialogue;
        dialogueData = dialogue.dialogueData;

        DisplayNextSentence(nextLineID);
    }

    public void DisplayNextSentence(int lineID)
    {
        skipClick = false;
        autoClick = false;
        isTyped = false;

        if (lineID == -1)
        {
            StopAllCoroutines();
            boxData.dialogueText.text = "";
            EndDialog();
            return;
        }

        line = dialogueData.lines[lineID];

        boxData = dialogue.GetDialogueBoxDataWithName(line.name);
        boxData.dialogueBox.SetActive(true);
        dialogue.SetColors(line.color, line.name);
        boxData.nameText.text = line.name;

        sentence = line.line;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        blipSound[currentSound].Stop();
        boxData.dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            boxData.dialogueText.text += letter;
            currentSound = Random.Range(0, blipArrayLength);
            blipSound[currentSound].volume = Random.Range(0.17f, 0.20f);
            blipSound[currentSound].pitch = Random.Range(1.25f, 1.35f);
            blipSound[currentSound].Play();
            yield return new WaitForSeconds(0.06f);
        }
        isTyped = true;
        yield return new WaitForSeconds(5);
        autoClick = true;
        boxData.dialogueBox.SetActive(false);
    }

    void EndDialog()
    {
        finished = true;
        started = false;
        blipSound[currentSound].Stop();
        boxData.dialogueBox.SetActive(false);
    }

    public void OnSkipClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (finished == false)
            {
                skipClick = true;
            }
            else
            {
                return;
            }
        }
    }

    public IEnumerator DisplayWholeText()
    {
        boxData.dialogueText.text = "";
        boxData.dialogueText.text = sentence;
        isTyped = true;

        yield return new WaitForSeconds(3f);
        autoClick = true;
        boxData.dialogueBox.SetActive(false);
    }

    private void DisplayButtons(Line line)
    {
        for (int i = 0; i < line.responses.Length; i++)
        {
            dialogue.buttons[i].SetActive(true);
            dialogue.buttons[i].GetComponentInChildren<TMP_Text>().text = line.responses[i].response;
            int ID = line.responses[i].connectedID;
            dialogue.buttons[i].GetComponent<Button>().onClick.AddListener(() => OnButtonClick(ID));
        }
    }

    public void OnButtonClick(int lineID)
    {      
        for (int i = 0; i < line.responses.Length; i++)
        {
            dialogue.buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            dialogue.buttons[i].SetActive(false);
        }

        boxData.dialogueBox.SetActive(false);

        nextLineID = lineID;
        StopAllCoroutines();
        DisplayNextSentence(nextLineID);
    }
}
