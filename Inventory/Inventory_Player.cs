using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : InventoryThing
{
    private static Inventory_Player thisIntance;

    public static Inventory_Player ThisPlayerInventory
    {
        get
        {
            return thisIntance;
        }
    }

    [SerializeField] private List<Items> thisTestItems = null;
    [SerializeField] private int thisGoldAmount = 0;
    [SerializeField] private List<Items> thisCheckedItems = null; // For check if multiple items is in player's inventory.
    public int ThisGoldAmount
    {
        get
        {
            return thisGoldAmount;
        }

        set
        {
            thisGoldAmount = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        thisIntance = this;

        StartThing();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThing();

        if (Input.GetKeyDown(KeyCode.B))
        {
            foreach (Items aItem in thisTestItems)
            {
                if (CheckSlots(aItem))
                {
                    AddItemtoInventory(aItem);
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.F12))
        {
            AddGoldToPlayer(500);
        }

        UIController.ThisUIController.SetGoldText(thisGoldAmount);
    }
    /// <summary>
    /// Add aItem to player's inventory.
    /// </summary>
    /// <param name="aItem"></param>
    public void AddItemtoInventory(Items aItem)
    {
        // Spawn a UI_Item.
        Items aItemIns;
        aItemIns = Instantiate(aItem, transform.position, Quaternion.identity);

        // Each slot that contains this item will store the info of this item.
        foreach (Inventory_SlotThing aLockedSlot in thisSelectedSlotList)
        {
            aLockedSlot.thisIteminSlot = aItemIns;

            // Each slot will be added to the item as well. So item knows what slots has been taken by itself.
            aItemIns.thisTakenSlots.Add(aLockedSlot);
        }

        aItemIns.transform.SetParent(thisInventoryUI.transform);
        aItemIns.transform.SetSiblingIndex(80);

        thisSelectedSlotList.Clear(); // Empty List for next time use.

        UIController.ThisUIController.SpawnItemToInvEffect(aItem);
    }
    /// <summary>
    /// Check if a item can fit into player's inventory. Need to be called before adding a item to player's inventory.
    /// </summary>
    /// <param name="aItem"></param>
    /// <returns></returns>
    public bool CheckSlots(Items aItem)
    {
        bool aBool = true;

        foreach (Inventory_SlotThing aSlot in thisSlotList)
        {
            // If a empty slot been found.
            if (aSlot.isSlotEmpty)
            {
                // The index of this empty slot as first slot.
                int aFirstSlotIndex = thisSlotList.IndexOf(aSlot);

                // Check for multiple slots (The slots that needed for place a item). The check funtion returns a bool.
                if (CheckMultipleSlotsAvalibity(aFirstSlotIndex, aItem))
                {
                    aBool = true;

                    break;
                }

                else
                {
                    if (thisSlotList.IndexOf(aSlot) == thisSlotList.Count - 1)
                    {
                        UIController.ThisUIController.HintError("Inventory Full!");

                        aBool = false;
                    }
                }
            }
        }

        print(aBool);

        return aBool;
    }
    /// <summary>
    /// Check if an item is inside player's inventory.
    /// </summary>
    /// <param name="aItemID"></param>
    /// <returns></returns>
    public bool CheckItemInInventory(int aItemID)
    {
        bool aCheck = false;
        // Check if aItemID can match the item inside aSlot.
        foreach(Inventory_SlotThing aSlot in thisSlotList)
        {
            if (aSlot.thisIteminSlot != null)
            {
                // The ItemID matched. Found the item in player's inventory!
                if (aSlot.thisIteminSlot.thisItemID == aItemID)
                {
                    aCheck = true;

                    break;
                }

                // The ItemID not matched. Is this the last slot in player's inventory?
                else if (aSlot.thisIteminSlot.thisItemID != aItemID)
                {
                    // Check if this is the last slot in player's inventory.
                    int aSlotIndex = aSlot.transform.GetSiblingIndex();

                    // This is the last slot.
                    if (aSlotIndex >= thisSlotList.Count - 1)
                    {
                        aCheck = false;

                        break;
                    }
                    // This is not the last slot. There is more slot to check.
                    else
                    {
                        continue;
                    }
                }
            }

            else
            {
                // Check if this is the last slot in player's inventory.
                int aSlotIndex = aSlot.transform.GetSiblingIndex();

                // This is the last slot.
                if (aSlotIndex >= thisSlotList.Count - 1)
                {
                    aCheck = false;

                    break;
                }
                // This is not the last slot. There is more slot to check.
                else
                {
                    continue;
                }
            }
        }

        return aCheck;
    }

    // A function to check if player has multiple same items. For example: If player has 10 Apple?
    // Returns a int. How many this item does player have?
    public int CheckMultipleItems(int aItemID)
    {
        int aItemNum = 0;


        // Check if aItemID can match the item inside aSlot.
        foreach (Inventory_SlotThing aSlot in thisSlotList)
        {
            if (aSlot.thisIteminSlot != null)
            {
                Items aItemtoCheck = aSlot.thisIteminSlot;

                // If this item hasn't been checked yet.
                if (!aItemtoCheck.beenLocked)
                {
                    // The ItemID matched. Found the item in player's inventory!
                    if (aItemtoCheck.thisItemID == aItemID)
                    {
                        aItemNum++;

                        aItemtoCheck.beenLocked = true; // Lock this item to make sure they only been count once.

                        thisCheckedItems.Add(aItemtoCheck);
                        // One Item found. Continue to check next slot.
                        continue;
                    }

                    // The ItemID not matched. Is this the last slot in player's inventory?
                    else if (aSlot.thisIteminSlot.thisItemID != aItemID)
                    {
                        // Check if this is the last slot in player's inventory.
                        int aSlotIndex = aSlot.transform.GetSiblingIndex();

                        // This is the last slot.
                        if (aSlotIndex >= thisSlotList.Count - 1)
                        {
                            break;
                        }
                        // This is not the last slot. There is more slot to check.
                        else
                        {
                            continue;
                        }
                    }
                }
                
            }

            else
            {
                // Check if this is the last slot in player's inventory.
                int aSlotIndex = aSlot.transform.GetSiblingIndex();

                // This is the last slot.
                if (aSlotIndex >= thisSlotList.Count - 1)
                {
                    break;
                }
                // This is not the last slot. There is more slot to check.
                else
                {
                    continue;
                }
            }
        }

        // Unlock all items that been pre-locked before.
        foreach (Items aItemToFree in thisCheckedItems)
        {
            aItemToFree.beenLocked = false;
        }

        return aItemNum;
    }

    // Directly remove a item in inventory. Need to check if there is a item first by using CheckItem method.
    public void RemoveItemInInventory(int aItemID)
    {
        // Check if aItemID can match the item inside aSlot.
        foreach (Inventory_SlotThing aSlot in thisSlotList)
        {
            if (aSlot.thisIteminSlot != null)
            {
                // The ItemID matched. Found the item in player's inventory!
                if (aSlot.thisIteminSlot.thisItemID == aItemID)
                {
                    // Destroy this item by Eat method in Items.
                    aSlot.thisIteminSlot.EatItem();
                }
            }
        }
    }
    public void AddGoldToPlayer(int aGold)
    {
        thisGoldAmount += aGold;

        SoundPlayer.ThisSoundPlayer.PlayCoinSound();
    }
}
