using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_BuildCannon : QuestThing
{
    private bool hasPickAxeandAxe = false;
    private bool hasEnoughIronOre = false;
    private bool hasEnoughWoodLog = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isQuestFinished)
        {
            UpdateCheckQuestConditions();
        }
    }

    protected void UpdateCheckQuestConditions()
    {
        if (!hasPickAxeandAxe)
        {
            if (Inventory_Player.ThisPlayerInventory.CheckItemInInventory(6) && Inventory_Player.ThisPlayerInventory.CheckItemInInventory(13))
            {
                thisQuestConditionBoolsArray[0] = true;

                AddDetailToDescription("I picked up my equipments in armory.");

                hasPickAxeandAxe = true;
            }
        
        }

        if (!hasEnoughIronOre)
        {
            if (Inventory_Player.ThisPlayerInventory.CheckMultipleItems(10) >= 10)
            {
                thisQuestConditionBoolsArray[1] = true;

                AddDetailToDescription("I collected enough Iron Ore. Bring them to Smith so he can help me to cast the barrel.");

                hasEnoughIronOre = true;
            }
        }
        
        if (!hasEnoughWoodLog)
        {
            if (Inventory_Player.ThisPlayerInventory.CheckMultipleItems(7) >= 4)
            {
                thisQuestConditionBoolsArray[2] = true;

                AddDetailToDescription("I collected enough WoodLog. Bring them to Smith so he can help me to craft the rack.");

                hasEnoughWoodLog = true;
            }
        }
        

        // Check if player has cannon barrel.
        if (Inventory_Player.ThisPlayerInventory.CheckItemInInventory(15))
        {
            thisQuestConditionBoolsArray[3] = true;
        }

        else
        {
            thisQuestConditionBoolsArray[3] = false;
        }

        // Check if player has cannon rack.
        if (Inventory_Player.ThisPlayerInventory.CheckItemInInventory(16))
        {
            thisQuestConditionBoolsArray[4] = true;
        }

        else
        {
            thisQuestConditionBoolsArray[4] = false;
        }
    }
}
