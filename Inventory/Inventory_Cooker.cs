using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Cooker : MonoBehaviour
{
    // This Script check if player has certain food for Cook.
    // This is a UI Script.

    [SerializeField] private Inventory_Player thisPlayerInventory = null;
    [SerializeField] private Items[] thisFoodsNeeded = null;
    [SerializeField] private List<Items> thisFoodsPreLocked = null;
    [SerializeField] private Items thisItemCooked = null;
    private AudioSource thisAudioSource = null;
    public CookObj thisCookObj = null;
    [SerializeField] private string thisErrorText = null;

    protected void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
    }
    public void CookRecipe()
    {
        int aFoodInt = thisFoodsNeeded.Length;

        foreach (Items aFood in thisFoodsNeeded)
        {
            foreach (Inventory_SlotThing aSlot in Inventory_Player.ThisPlayerInventory.thisSlotList)
            {
                // Here is my item to find in slot.
                Items aItem = aSlot.thisIteminSlot;

                // If the slot contains a item
                if (aItem != null)
                {
                    // If aItem match the needed item.
                    if (aItem.thisItemID == aFood.thisItemID)
                    {
                        if (!aItem.beenLocked)
                        {
                            aFoodInt--;

                            thisFoodsPreLocked.Add(aItem);

                            aItem.beenLocked = true;

                            break;
                        }

                        else
                        {
                            continue;
                        }
                    }

                    // Item doesn't match the food needed.
                    else
                    {
                        continue;
                        /*// If the slot is the last slot.
                        if (aSlot.transform.GetSiblingIndex() == Inventory_Player.ThisPlayerInventory.thisSlotList.Count - 1)
                        {
                            thisFoodsPreLocked.Clear();

                            UIController.ThisUIController.HintError(thisErrorText);

                            break;
                        }

                        // If there are more slot.
                        else
                        {
                            continue;
                        }*/
                    }
                }

                // If the slot is empty. Skip this slot.
                else
                {
                    continue;
                }
            }
        }

        if (aFoodInt == 0)
        {
            if (thisPlayerInventory.CheckSlots(thisItemCooked))
            {
                foreach (Items aUsedFood in thisFoodsPreLocked)
                {
                    aUsedFood.EatItem();
                }

                thisPlayerInventory.AddItemtoInventory(thisItemCooked);

                thisAudioSource.Play();

                aFoodInt = thisFoodsNeeded.Length;
            }

            else
            {
                foreach (Items aItem in thisFoodsPreLocked)
                {
                    aItem.beenLocked = false;
                }

                thisFoodsPreLocked.Clear();
            }
        }

        else if (aFoodInt > 0)
        {
            foreach(Items aItem in thisFoodsPreLocked)
            {
                aItem.beenLocked = false;
            }

            thisFoodsPreLocked.Clear();

            UIController.ThisUIController.HintError(thisErrorText);
        }
    }
}
