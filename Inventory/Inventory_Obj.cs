using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Obj : MonoBehaviour
{
    public string thisInventoryName = null;
    public List<Items> thisItemList = null;
    // Start is called before the first frame update
    void Start()
    {
        // Player place item into Chest.
        // Player close chest. all itemins get destroyed. But their itemid and slot been saved.
        // Player click on item in chest. Remove 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenInventory()
    {
        Inventory_Target.ThisTargetInventory.thisOpenedContainer = this;

        Inventory_Target.ThisTargetInventory.thisInventoryName = thisInventoryName;

        UIController.ThisUIController.OpenTargetInventoryMenu();

        UIController.ThisUIController.OpenPlayerInventoryMenu();

        if (thisItemList.Count > 0)
        {
            foreach (Items aItem in thisItemList)
            {
                aItem.gameObject.SetActive(true);

                foreach (Inventory_SlotThing aSlot in aItem.thisTakenSlots)
                {
                    aSlot.thisIteminSlot = aItem;
                }
            }
        }
    }

    public void CloseInventory()
    {
        if (thisItemList.Count > 0)
        {
            foreach (Items aItem in thisItemList)
            {
                foreach (Inventory_SlotThing aSlot in aItem.thisTakenSlots)
                {
                    aSlot.thisIteminSlot = null;
                }

                aItem.gameObject.SetActive(false);  
            }
        }

        Inventory_Target.ThisTargetInventory.thisOpenedContainer = null;

        UIController.ThisUIController.CloseTargetInventoryMenu();

        GetComponent<Chest>().isInventoryUIOpened = false;

        GetComponent<Chest>().isChestOpened = false;
    }
}
