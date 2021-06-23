using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player thisInstance;
    public static Player ThisPlayer
    {
        get
        {
            return thisInstance;
        }
    }
    //Components of player.
    private CharacterController thisCharCon = null;
    [SerializeField] private GameObject thisCamera = null;
    private AudioSource thisAudioSource = null;
    private Inventory_Player thisPlayerInventory = null;
    //Vars of player controls
    [SerializeField] private float thisSpeed = 0f; // this is player movement speed.
    private Vector3 thisDirection = Vector3.zero; // this is player moving direction.
    private bool isClimbing = false;
    // Bools for gameplay.
    public bool canPlayerControl = true;
    public bool canPlayerMove = true;
    // For player interaction with NPCs or Objs.
    public GameObject thisLookAtObj = null;
    // For Player Weapons
    private bool isAttacking = false;
    [SerializeField] private GameObject thisAxe = null;
    [SerializeField] private AudioClip thisAxeChopSound = null;
    public bool playerHasAxe = false;
    [SerializeField] private GameObject thisPickAxe = null;
    [SerializeField] private AudioClip thisPickaxeHitSound = null;
    public bool playerHasPickaxe = false;
    void Start()
    {
        thisInstance = this;
        thisCharCon = GetComponent<CharacterController>();
        thisAudioSource = GetComponent<AudioSource>();
        thisPlayerInventory = GetComponent<Inventory_Player>();
    }

    private void Update()
    {
        CheckPlayerControl();

        if (canPlayerControl)
        {
            if (canPlayerMove)
            {
                UpdatePlayerMovement();
            }

            UpdatePlayerAttack();
            UpdateInteract();
        }

        UpdatePlayerWeapon();
    }

    private void FixedUpdate()
    {
        UpdateRaycast();
    }

    protected void UpdatePlayerMovement()
    {
        Vector3 aDir = Vector3.zero;

        float aVInput = Input.GetAxis("Vertical");
        if (aVInput != 0f)
        {
            if (!isClimbing)
            {
                aDir += transform.forward * aVInput;
            }

            else
            {
                aDir += transform.up * aVInput;
            }
        }

        float aHInput = Input.GetAxis("Horizontal");
        if (aHInput != 0f)
        {
            aDir += transform.right * aHInput;
        }

        if (isClimbing)
        {
            thisCharCon.Move(aDir * thisSpeed * Time.deltaTime);
        }

        else
        {
            thisCharCon.SimpleMove(aDir * thisSpeed);
        }
    }

    // Use raycast to detect what player is look and plan to interacting with.
    protected void UpdateRaycast()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(thisCamera.transform.position, thisCamera.transform.forward, out hit, 3f))
        {
            Debug.DrawRay(thisCamera.transform.position, thisCamera.transform.forward * hit.distance, Color.yellow);

            thisLookAtObj = hit.collider.gameObject;
        }
        else
        {
            Debug.DrawRay(thisCamera.transform.position, thisCamera.transform.forward * 1000, Color.white);

            thisLookAtObj = null;
        }
    }

    // Interact with different objs. Based on what player is looking.
    protected void UpdateInteract()
    {
        if (thisLookAtObj != null)
        {
            Chest aChest = thisLookAtObj.GetComponent<Chest>();

            ItemObjs aItemObj = thisLookAtObj.GetComponent<ItemObjs>();

            RideThing aRideObj = thisLookAtObj.GetComponent<RideThing>();

            CookObj aCooker = thisLookAtObj.GetComponent<CookObj>();

            if (aChest != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    aChest.SwitchChestOpen();
                }

                UIController.ThisUIController.ShowInteractHint("E to Open");
            }

            else if (aCooker != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    aCooker.OpenCooker();
                    aCooker.CookerOn();
                }

                UIController.ThisUIController.ShowInteractHint("E to Cook");
            }

            else if (aItemObj != null)
            {
                if (aItemObj.enabled)
                {
                    if (aItemObj.isFree)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            if (thisPlayerInventory.CheckSlots(aItemObj.thisItemUIPrefab))
                            {
                                thisPlayerInventory.AddItemtoInventory(aItemObj.thisItemUIPrefab);

                                if (!aItemObj.infinityPickUp)
                                {
                                    Destroy(aItemObj.gameObject);
                                }
                            }
                        }

                        UIController.ThisUIController.ShowInteractHint("E to Pick Up");
                    }

                    else
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            aItemObj.BuyGood();
                        }

                        UIController.ThisUIController.ShowInteractHint("E to Buy: " + aItemObj.thisValue);
                    }
                }
            }

            else if (aRideObj != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    transform.SetParent(aRideObj.transform);

                    transform.position = aRideObj.thisRidePosition.transform.position;

                    GetComponent<Collider>().enabled = false;

                    aRideObj.isRiding = true;

                    aRideObj.thisPlayer = this;
                }

                UIController.ThisUIController.ShowInteractHint("E to Ride");
            }

            else
            {
                UIController.ThisUIController.thisInteractHint.SetActive(false);
            }
        }

        else
        {
            UIController.ThisUIController.thisInteractHint.SetActive(false);
        }
    }

    protected void CheckPlayerControl()
    {
        if (GlobalController.thisGlobalController.inMenu)
        {
            canPlayerControl = false;
        }

        else
        {
            canPlayerControl = true;
        }
    }

    protected void UpdatePlayerWeapon()
    {
        if (playerHasAxe)
        {
            thisAxe.SetActive(true);
        }

        else
        {
            thisAxe.SetActive(false);
        }

        if (playerHasPickaxe)
        {
            thisPickAxe.SetActive(true);
        }

        else
        {
            thisPickAxe.SetActive(false);
        }
    }
    protected void UpdatePlayerAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                // What happened if player swing his axe.
                // This code seems has some duplicate with pickaxe attack behavior. Need to be sort into one.
                if (playerHasAxe)
                {
                    isAttacking = true;

                    Animator aAxeAnimator = thisAxe.GetComponent<Animator>();

                    StartCoroutine(PlayerAttackBehavior(aAxeAnimator));

                    RaycastHit aAxeHit;

                    if (Physics.Raycast(thisCamera.transform.position, thisCamera.transform.forward, out aAxeHit))
                    {
                        if (aAxeHit.distance < 3f)
                        {
                            Collider aCollider = aAxeHit.collider;

                            if (aCollider != null)
                            {
                                thisAudioSource.PlayOneShot(thisAxeChopSound);
                            }

                            SourceThing aSource = aAxeHit.collider.gameObject.GetComponent<SourceThing>();

                            // If there are sources to gathering and if player has the right tool.
                            if (aSource != null && aSource.thisNeededTool == 6)
                            {
                                if (thisPlayerInventory.CheckSlots(aSource.thisSource))
                                {
                                    thisPlayerInventory.AddItemtoInventory(aSource.thisSource);

                                    aSource.OnDamage(1);
                                }
                                
                                thisAudioSource.PlayOneShot(thisAxeChopSound);
                            }

                            AttackableThing aAttackableObj = aAxeHit.collider.gameObject.GetComponent<AttackableThing>();

                            if (aAttackableObj != null)
                            {
                                aAttackableObj.OnDamage(1);

                                thisAudioSource.PlayOneShot(thisAxeChopSound);
                            }
                        }
                    }
                    // Player Swing Axe.
                    // Shoot a ray. If a ray hit a tree.
                    // Player Axe animation and chop the tree.
                }

                else if (playerHasPickaxe)
                {
                    isAttacking = true;

                    Animator aAxeAnimator = thisPickAxe.GetComponent<Animator>();

                    StartCoroutine(PlayerAttackBehavior(aAxeAnimator));

                    RaycastHit aPickAxeHit;

                    if (Physics.Raycast(thisCamera.transform.position, thisCamera.transform.forward, out aPickAxeHit))
                    {
                        if (aPickAxeHit.distance < 3f)
                        {
                            Collider aCollider = aPickAxeHit.collider;

                            if (aCollider != null)
                            {
                                thisAudioSource.PlayOneShot(thisPickaxeHitSound);
                            }

                            SourceThing aSource = aPickAxeHit.collider.gameObject.GetComponent<SourceThing>();

                            // If there are sources to gathering and if player has the right tool.
                            if (aSource != null && aSource.thisNeededTool == 13)
                            {
                                if (thisPlayerInventory.CheckSlots(aSource.thisSource))
                                {
                                    thisPlayerInventory.AddItemtoInventory(aSource.thisSource);

                                    aSource.OnDamage(1);
                                }

                                thisAudioSource.PlayOneShot(thisPickaxeHitSound);
                            }

                            // If hit an Attackable NPC.
                            AttackableThing aAttackableObj = aPickAxeHit.collider.gameObject.GetComponent<AttackableThing>();

                            if (aAttackableObj != null)
                            {
                                aAttackableObj.OnDamage(1);

                                thisAudioSource.PlayOneShot(thisPickaxeHitSound);
                            }
                        }
                    }
                    // Player Swing Pickaxe.
                    // Shoot a ray. If a ray hit a Minable.
                    // Player Pickaxe animation and mine the minable.
                }
            }
        }
    }

    protected IEnumerator PlayerAttackBehavior(Animator aWeaponAnimator)
    {
        aWeaponAnimator.SetBool("isPlayerAttacked", true);

        yield return new WaitForSeconds(0.25f);

        aWeaponAnimator.SetBool("isPlayerAttacked", false);

        isAttacking = false;
    }

    public void MovePlayerTo(Vector3 aPos)
    {
        thisCharCon.enabled = false;
        transform.position = aPos;
        thisCharCon.enabled = true;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Climbable")
        {
            isClimbing = true;

            print("Climbing!");
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Climbable")
        {
            isClimbing = false;
        }
    }
}
