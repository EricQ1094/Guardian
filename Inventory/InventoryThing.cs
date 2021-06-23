using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryThing : MonoBehaviour
{
    public List<Inventory_SlotThing> thisSlotList = null;
    [SerializeField] protected List<Inventory_SlotThing> thisSelectedSlotList = null;
    [SerializeField] protected GameObject thisInventoryUI = null;
    // Start is called before the first frame update
    protected void StartThing()
    {
        if (thisInventoryUI == null)
        {
            thisInventoryUI = this.gameObject;
        }
    }

    // Update is called once per frame
    protected void UpdateThing()
    {
        
    }

    // Place a item to a specific slot which is clicked by mouse.
    // Only use this function while player is selecting a item with the mouse.
    public virtual bool PlaceItemtoSlot(Items aItem, Inventory_SlotThing aSlot)
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

            return true;
        }

        else
        {
            print("Bad Slot!");

            return false;
        }
    }

    protected void ForcePlaceItemtoSlot(Items aItem, Inventory_SlotThing aSlot)
    {
        int aFirstIndex = thisSlotList.IndexOf(aSlot);
        int aXSize = aItem.thisItemXSize;
        int aYSize = aItem.thisItemYSize;

        // Check all slots avalibility via item's X and Y size.
        for (int iXSize = 0; iXSize < aXSize; iXSize++)
        {
            int aXIndex = aFirstIndex + iXSize;

            for (int iYSize = 0; iYSize < aYSize; iYSize++)
            {
                int aSlotIndex = aXIndex + iYSize * 8;

                thisSelectedSlotList.Add(thisSlotList[aSlotIndex]);
            }
        }

        foreach (Inventory_SlotThing aLockedSlot in thisSelectedSlotList)
        {
            aLockedSlot.thisIteminSlot = aItem;

            aItem.thisTakenSlots.Add(aLockedSlot);
        }

        aItem.transform.SetParent(thisInventoryUI.transform);
        aItem.transform.SetSiblingIndex(80);

        thisSelectedSlotList.Clear(); // Empty List for next time use.

        aItem.thisAudioSource.Play();
    }

    // Switch position with another item.

    public void SwitchIteminSlot(Items aItem, Inventory_SlotThing aSlot) // aItem is the one that needs to go to player selected item. Then player selected item will replace the position of aItem.
    {
        Items aPlaceItem = PlayerSelectedItem.ThisItemSelector.thisSelectedItem;

        // For compare item size.
        int aPlaceItemValue = aPlaceItem.thisItemXSize * aPlaceItem.thisItemYSize;
        int aItemValue = aItem.thisItemXSize * aItem.thisItemYSize;

        if (aPlaceItemValue > aItemValue)
        {
            print("Item too big!");

            // Error bat slot! slot haven't been released.
            return;
        }

        else
        {
            PlayerSelectedItem.ThisItemSelector.thisSelectedItem = null;
            aItem.PickUpItem();

            ForcePlaceItemtoSlot(aPlaceItem, aSlot);
            // Start to swith
            // Need to take care of
            // 1. Slots's stored item info. (Already took care by PickUpItem function in Items script.)
            // 2. Items's stored slots info. Write slot's info to new item. And new item to slots.
            // 3. destory old player selected item. Add it to the slot by PlacetoSlot function.
        }
        // Switch item in slot with the one that player selected.
    }

    public void RemoveItemFromSlot(Items aItem)
    {
        foreach (Inventory_SlotThing aSlot in aItem.thisTakenSlots)
        {
            aSlot.thisIteminSlot = null;
        }

        Destroy(aItem.gameObject);
    }

    // Check one slot to see if it's empty.
    protected bool CheckSingleSlotAvalibity(int aSlotIndex)
    {
        if (thisSlotList[aSlotIndex].isSlotEmpty != true)
        {
            return false;
        }

        else
        {
            return true;
        }
    }

    // Check many slots to see if they are empty by a Index, aXsize and a Y size. So basically check a rectangle by a first slot and sizes.
    protected bool CheckMultipleSlotsAvalibity(int aFirstIndex, Items aItem)
    {
        int aCheckedSlotNum = 0;

        int aXSize = aItem.thisItemXSize;
        int aYSize = aItem.thisItemYSize;

        int aCheckInt = (aFirstIndex + 1) % 8;

        if (aCheckInt == 0)
        {
            aCheckInt = 8;
        }

        if (aCheckInt > 9 - aXSize)
        {
            return false;
            // Not its problem.
        }

        // Check all slots avalibility via item's X and Y size.
        for (int iXSize = 0; iXSize < aXSize; iXSize++)
        {
            
            int aXIndex = aFirstIndex + iXSize;

            for (int iYSize = 0; iYSize < aYSize; iYSize++)
            {
                int aSlotIndex = aXIndex + iYSize * 8;

                if (aSlotIndex > thisSlotList.Count - 1)
                {
                    break;
                }

                else
                {
                    if (CheckSingleSlotAvalibity(aSlotIndex))
                    {
                        aCheckedSlotNum++;

                        thisSelectedSlotList.Add(thisSlotList[aSlotIndex]);

                        // Prelock slot so it will not be used by another AddItem function.
                        thisSlotList[aSlotIndex].isSlotEmpty = false;
                    }
                }
            }
        }

        if (aCheckedSlotNum == aXSize * aYSize)
        {
            return true;
        }

        else
        {
            foreach (Inventory_SlotThing aLockedSlot in thisSelectedSlotList)
            {
                // Release prelocked slot if multiple slot empty check is failed.
                aLockedSlot.isSlotEmpty = true;
            }

            // Clear the selected slot list for next time use.
            thisSelectedSlotList.Clear();

            return false;
        }
    }

    /*protected void LoadItemToSlot(Items aItem, Inventory_SlotThing aSlot)
    {
        aSlot.thisIteminSlot = aItem;
    }

    protected void EmptyItemFromSlot(Inventory_SlotThing aSlot)
    {
        aSlot.thisIteminSlot = null;
    }*/
    public void DropItemToGround(Items aItem)
    {
        Vector3 aSpawnPos = Vector3.zero;
        aSpawnPos = CameraController.ThisPlayerCamera.transform.position + CameraController.ThisPlayerCamera.transform.forward * 1f;

        ItemObjs aItemObjIns = Instantiate(aItem.thisItemObj, aSpawnPos, Quaternion.identity);
    }
}
