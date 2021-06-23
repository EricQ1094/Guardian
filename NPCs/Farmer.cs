using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : NPCThing
{
    [SerializeField] private GameObject m_Head = null;
    private Vector3 m_OriginalLookingDir = Vector3.zero;
    private bool isQuestBeenGiven = false;
    private bool isPlayerHasRabbitStew = false;
    private bool isQuestFinished = false;
    [SerializeField] private string[] thisPhase2Words = null;
    [SerializeField] private string[] thisPhase3Words = null;
    [SerializeField] private string[] thisPhase4Words = null;
    [SerializeField] private Items thisRewardItem = null;
    [SerializeField] private QuestThing thisBelongtoQuest = null;
    // Start is called before the first frame update
    void Start()
    {
        StartThing();

        m_OriginalLookingDir = m_Head.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThing();
        UpdateLookAtPlayer();

        if (isQuestFinished)
        {
            m_NPCWords = thisPhase4Words;
        }

        else
        {
            if (isQuestBeenGiven)
            {
                m_NPCWords = thisPhase2Words;
            }

            if (isPlayerHasRabbitStew)
            {
                m_NPCWords = thisPhase3Words;
            }
        }
    }

    protected void UpdateLookAtPlayer()
    {
        if (isPlayerInSight)
        {
            m_Head.transform.LookAt(m_Player.transform);
        }

        else
        {
            m_Head.transform.localEulerAngles = m_OriginalLookingDir;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (Inventory_Player.ThisPlayerInventory.CheckItemInInventory(14))
        {
            isPlayerHasRabbitStew = true;
        }

        else
        {
            isPlayerHasRabbitStew = false;
        }
    }

    public override void NPCEvent()
    {
        if (!isQuestBeenGiven)
        {
            UIController.ThisUIController.UnlockRabbitBBQ();

            UIController.ThisUIController.UnlockQuest_HungryFarmer();

            isQuestBeenGiven = true;
        }
        
        if (!isQuestFinished)
        {
            if (isPlayerHasRabbitStew)
            {
                if (Inventory_Player.ThisPlayerInventory.CheckSlots(thisRewardItem))
                {
                    Inventory_Player.ThisPlayerInventory.AddItemtoInventory(thisRewardItem);

                    Inventory_Player.ThisPlayerInventory.RemoveItemInInventory(14);

                    thisBelongtoQuest.SetQuestFinish();

                    thisBelongtoQuest.AddDetailToDescription("I bring the farmer a Rabbit BBQ. He gave me his father's armor as reward." + '\n' + "Quest Finished.");

                    isQuestFinished = true;
                }
            }
        }

        base.NPCEvent();
    }
}
