using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory_SlotThing : MonoBehaviour
{
    public InventoryThing thisBelongtoInventory = null;
    public bool isSlotEmpty = true;
    public Items thisIteminSlot = null;
    private Color thisEmptyColor = Color.black;
    private Color thisFullColor = Color.blue;
    // Start is called before the first frame update
    protected void StartThing()
    {
        thisEmptyColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    protected void UpdateThing()
    {
        UpdateSlotAvailability();
    }

    protected void UpdateSlotAvailability()
    {
        if (thisIteminSlot != null)
        {
            isSlotEmpty = false;

            GetComponent<Image>().color = thisFullColor;
        }

        else
        {
            isSlotEmpty = true;

            GetComponent<Image>().color = thisEmptyColor;
        }
    }
}
