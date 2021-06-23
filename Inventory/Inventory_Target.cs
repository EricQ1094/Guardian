using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Target : InventoryThing
{
    private static Inventory_Target thisTargetInventory;

    public static Inventory_Target ThisTargetInventory
    {
        get
        {
            return thisTargetInventory;
        }
    }

    public Inventory_Obj thisOpenedContainer = null;
    public string thisInventoryName = null;
    [SerializeField] private Text thisInventoryText = null;
    // Start is called before the first frame update
    void Start()
    {
        thisTargetInventory = this;

        StartThing();
        // Display the inventory name. For Target inventory only. Player inventory doesn't have a name.
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThing();

        if (thisInventoryText != null)
        {
            thisInventoryText.text = thisInventoryName;
        }
    }

    public override bool PlaceItemtoSlot(Items aItem, Inventory_SlotThing aSlot)
    {
        int aFirstSlotIndex = thisSlotList.IndexOf(aSlot);

        print(aSlot + "Index is " + aFirstSlotIndex);

        if (CheckMultipleSlotsAvalibity(aFirstSlotIndex, aItem))
        {
            foreach (Inventory_SlotThing aLockedSlot in thisSelectedSlotList)
            {
                aLockedSlot.thisIteminSlot = aItem;

                aItem.thisTakenSlots.Add(aLockedSlot);
            }

            aItem.transform.SetParent(thisInventoryUI.transform);

            thisSelectedSlotList.Clear(); // Empty List for next time use.

            aItem.thisAudioSource.Play();

            thisOpenedContainer.thisItemList.Add(aItem);
            aItem.thisInventory = thisOpenedContainer;

            return true;
        }

        else
        {
            print("Bad Slot!");

            return false;
        }
    }

    public void CloseContainer()
    {
        thisOpenedContainer.CloseInventory();
    }
}
