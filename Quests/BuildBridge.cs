using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridge : MonoBehaviour
{
    private bool isPlayerInTrigger = false;
    [SerializeField] private GameObject thisBridgeToBuild = null;
    [SerializeField] private Quest_ParrotandPirate thisQuest = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInTrigger)
        {
            if (Inventory_Player.ThisPlayerInventory.CheckMultipleItems(7) >= 5)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Inventory_Player.ThisPlayerInventory.RemoveItemInInventory(7);
                    Inventory_Player.ThisPlayerInventory.RemoveItemInInventory(7);
                    Inventory_Player.ThisPlayerInventory.RemoveItemInInventory(7);
                    Inventory_Player.ThisPlayerInventory.RemoveItemInInventory(7);
                    Inventory_Player.ThisPlayerInventory.RemoveItemInInventory(7);

                    thisBridgeToBuild.SetActive(true);

                    thisQuest.isPlayerBuildBridge = true;

                    Destroy(gameObject);
                }

                UIController.ThisUIController.HintNormal("Press E to Build the Bridge.");
            }
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        Player aPlayer = other.gameObject.GetComponent<Player>();

        if (aPlayer != null)
        {
            isPlayerInTrigger = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        Player aPlayer = other.gameObject.GetComponent<Player>();

        if (aPlayer != null)
        {
            isPlayerInTrigger = false;
        }
    }
}
