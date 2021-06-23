using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Items : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//, IPointerDownHandler, IPointerUpHandler
{
    // This Script is the UI Icon of a item.

    public int thisItemID = 0; // Item Index. Use it to find specific item prefab.
    public int thisItemTypeIndex = 0; // 0 Comsumable, 1 Noncomsumable, 2 Helmet, 3 Armor, 4 Sword, 5 Sheld, 6 Glove, 7 Boot, 8 foods 9 Materials.
    public int thisItemXSize = 0; // How much slot does this item take in X Axis.
    public int thisItemYSize = 0; // How much slot does this item take in X Axis.
    public string thisItemDescription = null; // The description of this Item.
    public List<Inventory_SlotThing> thisTakenSlots = null; // What slots has been occupied by this item.
    public Inventory_Obj thisInventory = null; // Is this item inside an Inventory? Only needed while in Target Inventory like chest. No needed in player's inventory.
    public ItemObjs thisItemObj = null; // The 3D Gameobject of this item.
    public AudioSource thisAudioSource = null; // The audio while moving items around in inventory.
    public bool beenLocked = false; // This is used for checking multiple items in inventory. A locked item will not be counted again. So check function can tell how many of same items is in this inventory.
    // Start is called before the first frame update
    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update Item's position inside Inventory.
        if (PlayerSelectedItem.ThisItemSelector.thisSelectedItem != this)
        {
            UpdateItemPos();
        }
    }

    // For determine if a mouse cursor is on this item.
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        PlayerSelectedItem.ThisItemSelector.thisMouseOnItem = this;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        PlayerSelectedItem.ThisItemSelector.thisMouseOnItem = null;
    }

    // Pick up this item to Cursor.
    public void PickUpItem()
    {
        foreach (Inventory_SlotThing aSlot in thisTakenSlots)
        {
            aSlot.thisIteminSlot = null;
        }

        thisTakenSlots.Clear();

        if (thisInventory != null)
        {
            thisInventory.thisItemList.Remove(this);

            thisInventory = null;
        }

        PlayerSelectedItem.ThisItemSelector.thisSelectedItem = this;

        thisAudioSource.Play();

        transform.SetParent(UIController.ThisUIController.transform);
    }

    // Destroy this item. Used in crafting system.
    public void EatItem()
    {
        foreach (Inventory_SlotThing aSlot in thisTakenSlots)
        {
            aSlot.thisIteminSlot = null;
        }

        thisTakenSlots.Clear();

        if (thisInventory != null)
        {
            thisInventory.thisItemList.Remove(this);

            thisInventory = null;
        }

        Destroy(gameObject);
    }

    // Update self's position to fit the grid system of the inventory. Based on what slots has been occupied by this item.
    protected void UpdateItemPos()
    {
        float aItemXPos = (thisTakenSlots[0].GetComponent<RectTransform>().localPosition.x + thisTakenSlots[thisTakenSlots.Count - 1].GetComponent<RectTransform>().localPosition.x) / 2f;
        float aItemYPos = (thisTakenSlots[0].GetComponent<RectTransform>().localPosition.y + thisTakenSlots[thisTakenSlots.Count - 1].GetComponent<RectTransform>().localPosition.y) / 2f;

        Vector3 aItemPos = Vector3.zero;
        aItemPos.x = aItemXPos;
        aItemPos.y = aItemYPos;

        GetComponent<RectTransform>().localPosition = aItemPos;
    }
}