using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCannon : MonoBehaviour
{
    //this script detect if player has cannon barrel and rack to build the cannon. Then if player has the material, hint player to press E to build the cannon.
    public bool canBuildCannon = false;
    [SerializeField] private CG_BossFight thisBossFightCG = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canBuildCannon)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                thisBossFightCG.gameObject.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
    protected void OnTriggerEnter(Collider other)
    {
        Player aPlayer = other.gameObject.GetComponent<Player>();

        if (aPlayer != null)
        {
            if (Inventory_Player.ThisPlayerInventory.CheckItemInInventory(15) && Inventory_Player.ThisPlayerInventory.CheckItemInInventory(16))
            {
                UIController.ThisUIController.HintNormal("Press E to Build the Cannon!");

                canBuildCannon = true;
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        Player aPlayer = other.gameObject.GetComponent<Player>();

        if (aPlayer != null)
        {
            UIController.ThisUIController.thisInteractHint.SetActive(false);
        }
    }
}
