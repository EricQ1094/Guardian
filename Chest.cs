using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool isChestOpened = false;
    public bool isInventoryUIOpened = false;

    protected void Awake()
    {
        
    }

    protected void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (isChestOpened)
        {
            if (!isInventoryUIOpened)
            {
                GetComponent<Inventory_Obj>().OpenInventory();

                isInventoryUIOpened = true;
            }
        }

        else
        {

        }
    }

    public void SwitchChestOpen()
    {
        if (!isChestOpened)
        {
            isChestOpened = true;
        }

        else
        {
            isChestOpened = false;
        }
    }
}
