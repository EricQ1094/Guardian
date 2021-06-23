using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrot : NPCThing
{
    [SerializeField] private QuestThing thisBelongtoQuest = null;
    [SerializeField] private Pirate thisPirate = null;
    [SerializeField] private string[] thisPhase2Words = null;// Player killed the pirate.
    [SerializeField] private string[] thisPhase3Words = null;// Player has Magic Flower
    private bool isPirateAlive = true;
    private bool isPlayerHasFlower = false;
    // Start is called before the first frame update
    void Start()
    {
        StartThing();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThing();
        UpdatePirateStatus();

        if (!isPirateAlive)
        {
            if (!isPlayerHasFlower)
            {
                m_NPCWords = thisPhase2Words;
            }

            else
            {
                m_NPCWords = thisPhase3Words;
            }
        }
    }

    protected void UpdatePirateStatus()
    {
        if (!thisPirate.gameObject.activeSelf)
        {
            isPirateAlive = false;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (Inventory_Player.ThisPlayerInventory.CheckItemInInventory(17))
        {
            isPlayerHasFlower = true;
        }
    }

    public override void NPCEvent()
    {
        if (isPlayerHasFlower)
        {
            Inventory_Player.ThisPlayerInventory.RemoveItemInInventory(17);

            Inventory_Player.ThisPlayerInventory.AddGoldToPlayer(5000);

            thisBelongtoQuest.SetQuestFinish();
        }

        base.NPCEvent();
    }
}
