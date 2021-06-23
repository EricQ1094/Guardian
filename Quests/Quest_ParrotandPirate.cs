using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_ParrotandPirate : QuestThing
{
    private bool isPirateDead = false;
    [SerializeField] private GameObject thisPirate = null;
    public bool isPlayerCheckedCave = false;
    [SerializeField] private CG_ParrotQuestP3 thisCG = null;
    public bool isPlayerBuildBridge = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isQuestFinished)
        {
            UpdateQuestTracker();
        }
    }

    protected void UpdateQuestTracker()
    {
        if (!isPirateDead)
        {
            thisQuestConditionsArray[0] = "Kill the Pirate.";

            thisQuestConditionBoolsArray[0] = false;

            if (!thisPirate.activeSelf)
            {
                thisCG.gameObject.SetActive(true);

                AddDetailToDescription("I killed the pirate. The parrot claimed he was a prince, and he want me to bring him a Magic Flower which I can find in Mining Cave.");

                isPirateDead = true;
            }
        }

        else
        {
            thisQuestConditionsArray[1] = "Find Magic Flower at the top of Mining Cave.";

            thisQuestConditionBoolsArray[0] = true;

            thisQuestConditionBoolsArray[1] = false;

            if (isPlayerCheckedCave)
            {
                thisQuestConditionsArray[2] = "Get 5 wood logs to build a bridge.";

                thisQuestConditionBoolsArray[1] = true;

                if (Inventory_Player.ThisPlayerInventory.CheckMultipleItems(7) >= 5)
                {
                    thisQuestConditionBoolsArray[2] = true;
                }

                if (isPlayerBuildBridge)
                {
                    thisQuestConditionsArray[3] = "Bring Flower to Prince Parrot.";

                    thisQuestConditionBoolsArray[3] = false;
                }
            }
        }
    }

    public override void SetQuestFinish()
    {
        thisQuestConditionBoolsArray[3] = true;

        base.SetQuestFinish();
    }
}
