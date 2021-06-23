using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Slot_Specific : Inventory_SlotThing
{
    public int thisAcceptedItem = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartThing();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThing();
        UpdateEquipWeapon();
    }

    protected void UpdateEquipWeapon()
    {
        if (thisIteminSlot != null)
        {
            if (thisIteminSlot.thisItemID == 6)
            {
                Player.ThisPlayer.playerHasAxe = true;
            }

            else if (thisIteminSlot.thisItemID == 13)
            {
                Player.ThisPlayer.playerHasPickaxe = true;
            }
        }

        else
        {
            if (thisAcceptedItem == 4)
            {
                Player.ThisPlayer.playerHasAxe = false;
                Player.ThisPlayer.playerHasPickaxe = false;
            }
        }
    }
}
