using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DialogueControler : MonoBehaviour
{
    [SerializeField] private Text thisNPCNameText = null;
    [SerializeField] private Text thisDialogueText = null;
    [SerializeField] private Text thisContinueHint = null;
    [SerializeField] private string[] thisDialogueArray = null;
    private int thisDialogueIndex = 0;
    public bool isShowingDialogue = false;
    public NPCThing thisTalkingNPC = null;

    // Update is called once per frame
    void Update()
    {
        if (isShowingDialogue) // If this Dialogue UI been Turned On by UI Controller.
        {
            UpdateDialogue(); // Show Dialogue based on Dialogue Index.

            if (Input.GetKeyDown(KeyCode.F)) // Press F to show next line.
            {
                thisDialogueIndex++;
            }

            if (thisDialogueIndex == thisDialogueArray.Length) // If the line is the last one.
            {
                thisDialogueIndex = 0; // Rest Dialogue Index.
                thisTalkingNPC.NPCEvent(); // Trigger the NPC event.
                CloseDialogue(); // Turn off dialogue UI.
            }
        }
    }
    public void ShowSingleDialogue(string aSpeakerName, string aDialogue)
    {
        thisNPCNameText.text = aSpeakerName;

        thisDialogueText.text = aDialogue;

        thisContinueHint.enabled = false;
    }
    /// <summary>
    /// Loading Name and Dialogue lines from NPC to this script.
    /// </summary>
    /// <param name="aNPCName"></param>
    /// <param name="aDialogueArray"></param>
    public void LoadDialogue(string aNPCName, string[] aDialogueArray)
    {
        thisNPCNameText.text = aNPCName;

        thisDialogueArray = aDialogueArray;

        thisContinueHint.enabled = true;
    }
    /// <summary>
    /// Display lines on Dialogue UI based on Index.
    /// </summary>
    protected void UpdateDialogue()
    {
        thisDialogueText.text = thisDialogueArray[thisDialogueIndex];
    }
    /// <summary>
    /// Turn Dialogue UI off.
    /// </summary>
    protected void CloseDialogue()
    {
        UIController.ThisUIController.CloseNPCDialogue();
    }
}
