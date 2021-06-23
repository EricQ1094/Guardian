using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Picnic : QuestThing
{
    // This Script controls player's Quests Progress.
    // GameStart Quest: Picnic.
    private bool startMonsterInvader = false;
    [SerializeField] private GameObject thisMonsterInvadeCG = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isQuestFinished)
        {
            UpdateTrackingText();

            if (thisQuestConditionBoolsArray[0] && thisQuestConditionBoolsArray[1])
            {
                if (!startMonsterInvader)
                {
                    thisMonsterInvadeCG.SetActive(true);

                    startMonsterInvader = true;

                    thisQuestDescription += '\n' + "An unsual earthquake stopped my picnic. I need to report to capatain immediately!";

                    isQuestFinished = true;
                }
            }
        }
    }

    protected void UpdateTrackingText()
    {
        // Check if player has woodlog.
        if (!Inventory_Player.ThisPlayerInventory.CheckItemInInventory(7))
        {
            thisQuestConditionBoolsArray[0] = false;
        }

        else
        {
            thisQuestConditionBoolsArray[0] = true;
        }

        // Check if player has fish.
        if (!Inventory_Player.ThisPlayerInventory.CheckItemInInventory(8))
        {
            thisQuestConditionBoolsArray[1] = false;
        }

        else
        {
            thisQuestConditionBoolsArray[1] = true;
        }
    }
}
