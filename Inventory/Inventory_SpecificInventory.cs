using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_SpecificInventory : InventoryThing
{
    // Start is called before the first frame update
    void Start()
    {
        StartThing();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThing();
    }

    public bool PutItemtoSpecificSlot(Items aItem, Inventory_Slot_Specific aSpecificSlot)
    {
        if (aItem.thisItemTypeIndex == aSpecificSlot.thisAcceptedItem)
        {
            if (aSpecificSlot.isSlotEmpty)
            {
                aItem.thisTakenSlots.Add(aSpecificSlot);

                aSpecificSlot.thisIteminSlot = aItem;

                aItem.transform.SetParent(transform);

                aItem.thisAudioSource.Play();

                return true;
            }

            else
            {
                UIController.ThisUIController.HintError("Already equiped a Item!");

                return false;
            }
        }

        else
        {
            print("Item doesn't fit Slot!");

            return false;
        }
    }
}
