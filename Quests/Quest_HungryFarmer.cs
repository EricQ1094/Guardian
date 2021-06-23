using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_HungryFarmer : QuestThing
{
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
        if (Inventory_Player.ThisPlayerInventory.CheckItemInInventory(14))
        {
            thisQuestConditionsArray[0] = "Bring Rabbit BBQ to hungry farmer.";
        }

        else
        {
            thisQuestConditionsArray[0] = "Cook a Rabbit BBQ";

            thisQuestConditionBoolsArray[0] = false;
        }
    }

    public override void SetQuestFinish()
    {
        thisQuestConditionBoolsArray[0] = true;

        base.SetQuestFinish();
    }
}
