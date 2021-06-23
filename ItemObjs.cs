using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjs : MonoBehaviour
{
    public Items thisItemUIPrefab = null;
    [SerializeField] private GameObject thisMerchant = null;
    public int thisValue = 0;
    public bool isFree = false;
    public bool infinityPickUp = false; // Item Obj will not be destroyed if this is true. Player can pickup as many times as they want.
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (thisMerchant == null)
        {
            isFree = true;
        }
    }
    public void BuyGood()
    {
        int aPlayerGoldAmount = Inventory_Player.ThisPlayerInventory.ThisGoldAmount;

        if (aPlayerGoldAmount >= thisValue)
        {
            Inventory_Player.ThisPlayerInventory.ThisGoldAmount -= thisValue;

            SoundPlayer.ThisSoundPlayer.PlayCoinSound();

            thisMerchant = null;
        }

        else
        {
            UIController.ThisUIController.HintError("No Enough Gold!");
        }
    }
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Seller")
        {
            GameObject.Find("Player").GetComponent<Inventory_Player>().AddGoldToPlayer(thisValue);

            Destroy(this.gameObject);
        }
    }
}
