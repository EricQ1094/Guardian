using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestTracker : MonoBehaviour
{
    [SerializeField] private Text thisQuestNameText = null;
    [SerializeField] private Text thisQuestDetailsText = null;
    [SerializeField] private QuestThing[] thisQuestsArray = null; // The array that contains all Quest in this game.
    public List<Text> thisQuestHintsList = null; // The texts of all quest conditions inside one quest.
    private int thisActiveQuestIndex = 0; // Which quest is been tracking now? Use with the Quest Index List.

    // Update is called once per frame
    void Update()
    {
        UpdateTrackingQuest();
    }

    //Update Quest info and tracker inside Quest UI based on what quest has been selected by player.
    protected void UpdateTrackingQuest()
    {
        // The Quest class that been tracking.
        QuestThing aTrackingQuest = thisQuestsArray[thisActiveQuestIndex];
        // Update the Name of the Quest.
        thisQuestNameText.text = aTrackingQuest.thisQuestName;

        // Update the Description of the Quest.
        thisQuestDetailsText.text = aTrackingQuest.thisQuestDescription;

        int aQuestConditionNum = thisQuestsArray[thisActiveQuestIndex].thisQuestConditionsArray.Length; // How many quest condition needed to be track?

        // Update the Quest Trackers based on loaded quest.
        for (int iQuestTrack = 0; iQuestTrack < aQuestConditionNum; iQuestTrack++)
        {
            string aQuestCondition = thisQuestsArray[thisActiveQuestIndex].thisQuestConditionsArray[iQuestTrack]; // The text description of this quest condition.

            thisQuestHintsList[iQuestTrack].text = aQuestCondition;

            bool aQuestConditionFinished = thisQuestsArray[thisActiveQuestIndex].thisQuestConditionBoolsArray[iQuestTrack]; // The bool of this quest conditon.

            // Set the color of the text based on if the quest condition is finished.
            if (!aQuestConditionFinished)
            {
                thisQuestHintsList[iQuestTrack].color = Color.red;
            }

            else
            {
                thisQuestHintsList[iQuestTrack].color = Color.blue;
            }
        }

        // Clean the extra condition text from previous quest.
        // For example. One quest has 5 condition text. But the next one only have 2.
        // So this function hide the extra 3 condition text.
        if (thisQuestHintsList.Count > aTrackingQuest.thisQuestConditionBoolsArray.Length)
        {
            for (int aExtraCondition = aTrackingQuest.thisQuestConditionBoolsArray.Length; aExtraCondition < thisQuestHintsList.Count; aExtraCondition++)
            {
                thisQuestHintsList[aExtraCondition].text = " ";
            }
        }
    }
    // Replace the Quest Name and Text in Quest UI Component.
    public void UILoadQuest_Picnic()
    {
        thisActiveQuestIndex = 0;
    }

    public void UILoadQuest_BuildTheCannon()
    {
        thisActiveQuestIndex = 1;
    }
    public void UILoadQuest_BeatTheMonster()
    {
        thisActiveQuestIndex = 2;
    }

    public void UILoadQuest_ParrotandPirate()
    {
        thisActiveQuestIndex = 3;
    }

    public void UILoadQuest_HungryFarmer()
    {
        thisActiveQuestIndex = 4;
    }
}
