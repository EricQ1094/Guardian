using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    // The one script that controll all UI elements.

    private static UIController thisInstance;
    public static UIController ThisUIController
    {
        get
        {
            return thisInstance;
        }
    }
    // Components of UI
    public GameObject thisEscMenu = null;
    public GameObject thisInteractHint = null;
    public Text thisInteractHintText = null;
    protected AudioSource thisAudioSource = null;
    [SerializeField] private AudioClip[] thisAudioArray = null;
    public GameObject thisCookMenu = null;
    [SerializeField] private GameObject thisLoadingScreen = null;
    // For Inventory Menu
    [SerializeField] private GameObject thisInventoryMenu = null;
    [SerializeField] private GameObject thisTargetInventoryMenu = null;
    [SerializeField] private Inventory_Player thisPlayerInventory = null;
    [SerializeField] private Text thisGoldText = null;
    // For Items in Inventory Menu
    [SerializeField] private GameObject thisItemDescription = null;
    private bool isItemDescriptionVisible = true;
    // For Inventory System Visualization.
    [SerializeField] private GameObject thisItemToInventoryEffectPrefab = null;
    [SerializeField] private GameObject thisItemEffectSP = null;
    [SerializeField] private Text thisHintText = null;
    private float thisHintTextTimer = 0f;
    // Raycast on Canva.
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    // Bools to control menus
    [SerializeField] private bool isPlayerInventoryMenuOn = false;
    [SerializeField] private bool isTargetInventoryMenuOn = false;
    [SerializeField] private bool isEscMenuOn = false;
    [SerializeField] private bool isCookingMenuOn = false;
    private bool isDialogueTextOn = false;
    [SerializeField] private bool isAnyMenuOn = false;
    // For NPC dialogue
    [Header("NPC Dialogue")]
    [SerializeField] private GameObject thisTalkHint = null;
    [SerializeField] private UI_DialogueControler thisUIDialogueController = null;
    [Header("Player Quest")]
    public UI_QuestTracker thisQuestUI = null;
    private bool isQuestUIOn = false;
    [SerializeField] private GameObject thisBuildtheCannonQuest = null;
    [SerializeField] private GameObject thisParrotandPirateQuest = null;
    [SerializeField] private GameObject thisHungryFarmerQuest = null;
    [Header("Boss Battle")]
    [SerializeField] private GameObject thisBossUI = null;
    [SerializeField] private Image thisBossHPBar = null;
    [SerializeField] private Image thisWallHPBar = null;
    [SerializeField] private GameObject thisEndGameUI = null;
    [Header("Craft System")]
    [SerializeField] private GameObject thisRabbitBBQCover = null;
    [SerializeField] private GameObject[] thisCanonCovers = null;
    [SerializeField] private GameObject thisCraftMenu = null;
    // Start is called before the first frame update
    void Start()
    {
        thisInstance = this;
        thisAudioSource = GetComponent<AudioSource>();
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenPlayerInventoryMenu();
        }

        else if (Input.GetKeyDown(KeyCode.J))
        {
            if (!isQuestUIOn)
            {
                isQuestUIOn = true;
            }
        }

        if (isItemDescriptionVisible)
        {
            UpdateItemDescriptionPos();
        }

        UpdateMouseClicked();
        UpdateInventoryHintTextTimer();
        UpdateMenus();
    }

    protected void UpdateMenus()
    {
        // Control Menu UI Display or not.
        if (isPlayerInventoryMenuOn)
        {
            thisInventoryMenu.gameObject.SetActive(true); // Show Inventory Menu
        }

        else
        {
            thisInventoryMenu.gameObject.SetActive(false); // Hide Inventory Menu
        }

        if (isTargetInventoryMenuOn)
        {
            thisTargetInventoryMenu.gameObject.SetActive(true); // Show Target Inventory Menu
        }

        else
        {
            thisTargetInventoryMenu.gameObject.SetActive(false); // Hide Target Inventory Menu
        }

        if (isCookingMenuOn)
        {
            thisCookMenu.gameObject.SetActive(true); // Show Cooking Menu
        }

        else
        {
            thisCookMenu.gameObject.SetActive(false); // Hide Cooking Menu
        }

        if (isEscMenuOn)
        {
            thisEscMenu.gameObject.SetActive(true); // Show Esc Menu
        }

        else
        {
            thisEscMenu.gameObject.SetActive(false); // Hide Esc Menu
        }

        if (isQuestUIOn)
        {
            thisQuestUI.gameObject.SetActive(true); // Show Quest Menu
        }

        else
        {
            thisQuestUI.gameObject.SetActive(false); // Hide Quest Menu
        }


        // Check if any menu ui is on. Will not checking ESC menu.
        if (isPlayerInventoryMenuOn || isTargetInventoryMenuOn || isCookingMenuOn || isQuestUIOn || isDialogueTextOn)
        {
            isAnyMenuOn = true; 
        }

        // Check if no normal ui is on. Will not cheking ESC menu.
        else if (!isPlayerInventoryMenuOn && !isTargetInventoryMenuOn && !isCookingMenuOn && !isQuestUIOn && !isDialogueTextOn)
        {
            isAnyMenuOn = false;

            ItemDescriptionOff();
        }

        // If there is any menu ui on. Unlock and show crusor for player to interacte with UI.
        if (isAnyMenuOn)
        {
            GlobalController.thisGlobalController.inMenu = true;

            // Close all UI by press ESC. PS. Target Inventory Menu will not be closed because it has its own way to close.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseAllNormalMenu();
            }
        }

        else
        {
            if (isEscMenuOn)
            {
                GlobalController.thisGlobalController.inMenu = true;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CloseEscMenu();
                }
            }

            else
            {
                GlobalController.thisGlobalController.inMenu = false;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    OpenEscMenu();
                }
            }
        }
    }

    // This section controls Show or Hide of ALL Menu UI.
    public void OpenPlayerInventoryMenu()
    {
        isPlayerInventoryMenuOn = true;
    }

    protected void ClosePlayerInventoryMenu()
    {
        isPlayerInventoryMenuOn = false;
    }

    public void OpenTargetInventoryMenu()
    {
        isTargetInventoryMenuOn = true;
    }

    public void CloseTargetInventoryMenu()
    {
        isTargetInventoryMenuOn = false;
    }
    
    public void OpenCookMenu()
    {
        isCookingMenuOn = true;
    }

    protected void CloseCookMenu()
    {
        isCookingMenuOn = false;
    }
    protected void OpenEscMenu()
    {
        isEscMenuOn = true;
    }

    protected void CloseEscMenu()
    {
        isEscMenuOn = false;
    }
    public void ShowCraftMenu()
    {
        thisCraftMenu.SetActive(true);

        isDialogueTextOn = true;
    }

    public void CloseCraftMenu()
    {
        thisCraftMenu.SetActive(false);

        isDialogueTextOn = false;
    }
    protected void CloseAllNormalMenu()
    {
        ClosePlayerInventoryMenu();
        CloseCookMenu();
        CloseCraftMenu();
        isQuestUIOn = false;
        ItemDescriptionOff();
    }

    // This section is for showing Item's description while a mouse is on it.
    public void ItemDescriptionOn(string aString)
    {
        thisItemDescription.SetActive(true);

        Text aDescription = thisItemDescription.transform.GetChild(0).GetComponent<Text>();

        aDescription.text = aString;

        isItemDescriptionVisible = true;
    }

    public void ItemDescriptionOff()
    {
        Text aDescription = thisItemDescription.transform.GetChild(0).GetComponent<Text>();

        aDescription.text = " ";

        isItemDescriptionVisible = false;

        thisItemDescription.SetActive(false);
    }

    protected void UpdateItemDescriptionPos()
    {
        Vector3 aPos = Input.mousePosition;
        aPos.x += 0.1f + thisItemDescription.GetComponent<RectTransform>().rect.width / 2f;
        aPos.y -= 0.1f + thisItemDescription.GetComponent<RectTransform>().rect.height / 2f;

        thisItemDescription.transform.position = aPos;
    }
    
    // This section determine what player clicked on and what to do with this click.
    protected void UpdateMouseClicked()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                // To Pick Up a item.
                Items aItem = result.gameObject.GetComponent<Items>();

                if (aItem != null)
                {
                    if (PlayerSelectedItem.ThisItemSelector.thisSelectedItem == null)
                    {
                        aItem.PickUpItem();

                        return;
                    }
                }

                Inventory_Slot_Common aSlot = result.gameObject.GetComponent<Inventory_Slot_Common>();

                // Is there a inventory slot?
                if (aSlot != null)
                {
                    InventoryThing aInventory = aSlot.thisBelongtoInventory;
                    // Is the slot empty?
                    if (!aSlot.isSlotEmpty)
                    {
                        // Is player selecting any item?
                        if (PlayerSelectedItem.ThisItemSelector.thisSelectedItem != null)
                        {
                            aInventory.SwitchIteminSlot(aSlot.thisIteminSlot, aSlot);
                        }
                    }

                    else
                    {
                        // Is player selecting any item?
                        if (PlayerSelectedItem.ThisItemSelector.thisSelectedItem != null)
                        {
                            if (aInventory.PlaceItemtoSlot(PlayerSelectedItem.ThisItemSelector.thisSelectedItem, aSlot))
                            {
                                PlayerSelectedItem.ThisItemSelector.thisSelectedItem = null;
                            }
                        }
                    }
                }
            }

            // Foreach loop to find if a inventory is been clicked.
            foreach (RaycastResult result in results)
            {
                Inventory_UIArea aInventory = result.gameObject.GetComponent<Inventory_UIArea>();

                //is Inventory been clicked?
                if (aInventory != null)
                {
                    //Yes, break the loop.
                    break;
                }

                else
                {
                    // No, Keep finding.

                    // Keep finding until reach the end of the list.
                    if (result.index < results.Count - 1)
                    {
                        continue;
                    }

                    // No Inventory UI been clicked. Can drop item to ground.
                    else
                    {
                        if (PlayerSelectedItem.ThisItemSelector.thisSelectedItem != null)
                        {
                            thisPlayerInventory.DropItemToGround(PlayerSelectedItem.ThisItemSelector.thisSelectedItem);

                            PlayerSelectedItem.ThisItemSelector.DropSelectedItem();
                        }
                    }
                }
            }

            // Use for put a equipment into a specific slot.
            foreach (RaycastResult result in results)
            {
                Inventory_Slot_Specific aSpecificSlot = result.gameObject.GetComponent<Inventory_Slot_Specific>();

                if (aSpecificSlot != null)
                {
                    // Tested! print("Slot Found!");

                    Inventory_SpecificInventory aSpecificInventory = aSpecificSlot.transform.parent.GetComponent<Inventory_SpecificInventory>();

                    if (aSpecificInventory != null)
                    {
                        // Tested! print("Inventory Found!");

                        if (PlayerSelectedItem.ThisItemSelector.thisSelectedItem != null)
                        {
                            Items aItemToPlaceintoSlot = PlayerSelectedItem.ThisItemSelector.thisSelectedItem;

                            if (aSpecificInventory.PutItemtoSpecificSlot(aItemToPlaceintoSlot, aSpecificSlot))
                            {
                                PlayerSelectedItem.ThisItemSelector.thisSelectedItem = null;
                            }
                        }
                    }
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                Items aItem = result.gameObject.GetComponent<Items>();

                if (aItem != null)
                {
                    if (aItem.thisItemTypeIndex == 0)
                    {
                        aItem.EatItem();
                    }
                }
            }
        }
    }
    /// <summary>
    /// This method shows a little animation in HUD that a item has been added to player's inventory.
    /// </summary>
    /// <param name="aItem"></param>
    public void SpawnItemToInvEffect(Items aItem)
    {
        GameObject aEffectIns = Instantiate(thisItemToInventoryEffectPrefab, thisItemEffectSP.transform.position, Quaternion.identity);

        aEffectIns.gameObject.GetComponent<Image>().sprite = aItem.GetComponent<Image>().sprite;

        float aWidth = aItem.gameObject.GetComponent<RectTransform>().sizeDelta.x;
        float aHeight = aItem.gameObject.GetComponent<RectTransform>().sizeDelta.y;

        RectTransform aRT = aEffectIns.gameObject.GetComponent<RectTransform>();
        aRT.sizeDelta = new Vector2(aWidth, aHeight);

        aEffectIns.transform.SetParent(transform);
    }

    // This section is for TEXT HINT on HUD for player to understand what is going on.
    protected void UpdateInventoryHintTextTimer()
    {
        if (thisHintTextTimer > 0)
        {
            thisHintTextTimer -= Time.deltaTime;

            thisHintText.gameObject.SetActive(true);
        }

        else
        {
            thisHintText.gameObject.SetActive(false);
        }
    }

    public void ShowInteractHint(string aText)
    {
        thisInteractHint.SetActive(true);

        thisInteractHintText.text = aText;
    }
    public void HintError(string aString)
    {
        thisHintTextTimer = 2f;

        thisHintText.text = aString;

        thisAudioSource.PlayOneShot(thisAudioArray[0]);
    }

    public void HintNormal(string aHint)
    {
        thisHintTextTimer = 2f;

        thisHintText.text = aHint;
    }

    public void SetNPCTalkHintVisible(bool aBool)
    {
        thisTalkHint.SetActive(aBool);
    }

    // This is for talk to NPCs while player interacte with them.
    public void ShowNPCDialogue(string aName, string[] aDialogueArray, NPCThing aNPC)
    {
        isDialogueTextOn = true;
        thisUIDialogueController.gameObject.SetActive(true);
        thisUIDialogueController.LoadDialogue(aName, aDialogueArray);
        thisUIDialogueController.isShowingDialogue = true;
        thisUIDialogueController.thisTalkingNPC = aNPC;
    }

    public void CloseNPCDialogue()
    {
        isDialogueTextOn = false;
        thisUIDialogueController.gameObject.SetActive(false);
        thisUIDialogueController.isShowingDialogue = false;
    }

    // This is for showing dialogue inside a CG.
    public void ShowCGDialogue(string aName, string aDialogue)
    {
        isDialogueTextOn = true;
        thisUIDialogueController.gameObject.SetActive(true);
        thisUIDialogueController.ShowSingleDialogue(aName, aDialogue);
    }

    // Show player's gold amount in HUD.
    public void SetGoldText(int aGoldNum)
    {
        thisGoldText.text = "Gold: " + aGoldNum.ToString();
    }

    // This is for showing and hiding a fake loading screen while teleport player around.
    public void ShowLoadingScreen()
    {
        thisLoadingScreen.SetActive(true);
        isDialogueTextOn = true;
    }

    public void HideLoadingScreen()
    {
        thisLoadingScreen.SetActive(false);
        isDialogueTextOn = false;
    }
    // This section is for Final Boss fight.
    public void TriggerBossFightHud(bool aBool)
    {
        thisBossUI.SetActive(aBool);
    }
    public void UpdateWallHPHud(float aScale)
    {
        thisWallHPBar.rectTransform.localScale = new Vector3(aScale, 1f, 1f);
    }
    public void UpdateBossHPHud(float aScale)
    {
        thisBossHPBar.rectTransform.localScale = new Vector3(aScale, 1f, 1f);
    }

    public void SetEndGamePanelActive(bool aBool)
    {
        thisEndGameUI.SetActive(aBool);

        isDialogueTextOn = aBool;

        print("What?");
    }

    // This section if for unlock UI Elements like Craft BP; Cook Reciept; Quest.etc.
    public void UnlockRabbitBBQ()
    {
        HintNormal("New Cook Receipt Unlock!");

        thisRabbitBBQCover.SetActive(false);
    }

    public void UnlockCannonBlueprint()
    {
        HintNormal("New Craft Blueprint Unlock!");

        foreach (GameObject aCover in thisCanonCovers)
        {
            aCover.SetActive(false);
        }
    }

    public void UnlockQuest_BuildtheCannon()
    {
        thisBuildtheCannonQuest.SetActive(true);
    }

    public void UnlockQuest_ParrotandPirate()
    {
        thisParrotandPirateQuest.SetActive(true);
    }

    public void UnlockQuest_HungryFarmer()
    {
        thisHungryFarmerQuest.SetActive(true);
    }
}
