using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    // Gameobject Components.
    private AudioSource thisAudioSource;
    // Attributes of Canon.
    public bool isPlayerControling = false;
    private bool canFire = true;
    private float thisFireTimer = 0f;
    [SerializeField] private float thisFireCD = 0f;
    [SerializeField] private GameObject thisBarrel = null;
    private float thisBarrelAngle = 0f;
    [SerializeField] private Transform thisCanonBallSP = null;
    [SerializeField] private CanonBall thisCanonBallPrefab = null;
    [SerializeField] private GameObject thisCanonFireFX = null;
    // Start is called before the first frame update
    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        thisFireTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFireState();

        if (isPlayerControling)
        {
            UpdateBarrelRotation();

            if (canFire)
            {
                UpdateCanonFire();
            }
        }
    }

    protected void UpdateBarrelRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            thisBarrelAngle += 30f * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.E))
        {
            thisBarrelAngle -= 30f * Time.deltaTime;
        }

        thisBarrelAngle = Mathf.Clamp(thisBarrelAngle, -10.5f, 20f);

        Vector3 aBarrelLocalEulerAngles = new Vector3(thisBarrelAngle, 0f, 0f);

        thisBarrel.transform.localEulerAngles = aBarrelLocalEulerAngles;
    }

    protected void UpdateFireState()
    {
        if (thisFireTimer > 0f)
        {
            thisFireTimer -= Time.deltaTime;
        }

        else
        {
            canFire = true;

            thisFireTimer = thisFireCD;
        }
    }

    protected void UpdateCanonFire()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Try to find CannonBall in player's inventory by searching each slot.
            foreach (Inventory_SlotThing aSlot in Inventory_Player.ThisPlayerInventory.thisSlotList)
            {
                // Here is my item to find in slot.
                Items aItem = aSlot.thisIteminSlot;

                // If the slot contains a item
                if (aItem != null)
                {
                    // If aItem match the cannon ball.
                    if (aItem.thisItemID == 5)
                    {
                        Inventory_Player.ThisPlayerInventory.RemoveItemFromSlot(aItem);

                        GameObject aFireFXIns;
                        aFireFXIns = Instantiate(thisCanonFireFX, thisCanonBallSP.position, Quaternion.identity);

                        aFireFXIns.transform.forward = thisBarrel.transform.forward;

                        thisAudioSource.Play();

                        StartCoroutine(FireCanonBall());

                        break;
                    }

                    // Item doesn't match the cannon ball.
                    else
                    {
                        // If the slot is the last slot.
                        if (aSlot.transform.GetSiblingIndex() == Inventory_Player.ThisPlayerInventory.thisSlotList.Count - 1)
                        {
                            print("No Cannon Ball!");
                        }

                        // If there are more slot.
                        else
                        {
                            continue;
                        }
                    }
                }

                // If the slot is empty. Skip this slot.
                else
                {
                    continue;
                }
            }
        }
    }

    protected IEnumerator FireCanonBall()
    {
        yield return new WaitForSeconds(0.1f);

        CanonBall aCanonBallIns;
        aCanonBallIns = Instantiate(thisCanonBallPrefab, thisCanonBallSP.position, Quaternion.identity);

        aCanonBallIns.thisDirection = thisBarrel.transform.forward;

        canFire = false;
    }

    protected void OnTriggerEnter(Collider other)
    {
        Player aPlayer = other.GetComponent<Player>();

        if (aPlayer != null)
        {
            isPlayerControling = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        Player aPlayer = other.GetComponent<Player>();

        if (aPlayer != null)
        {
            isPlayerControling = false;
        }
    }
}
