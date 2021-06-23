using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCThing : MonoBehaviour
{
    [SerializeField] protected string m_NPCName = null;
    [SerializeField] protected string[] m_NPCWords = null;
    protected bool isPlayerInSight = false;
    protected GameObject m_Player = null;
    public bool isPlayerLooking = false;

    protected void StartThing()
    {

    }
    // Update is called once per frame
    protected void UpdateThing()
    {
        UpdatePlayerInsight();
    }

    protected virtual void UpdatePlayerInsight()
    {
        if (isPlayerInSight)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                UIController.ThisUIController.SetNPCTalkHintVisible(false);

                UIController.ThisUIController.ShowNPCDialogue(m_NPCName, m_NPCWords, this);
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Player aPlayer;
        aPlayer = other.gameObject.GetComponent<Player>();

        if (aPlayer != null)
        {
            m_Player = aPlayer.gameObject;

            isPlayerInSight = true;

            UIController.ThisUIController.SetNPCTalkHintVisible(true);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        Player aPlayer;
        aPlayer = other.gameObject.GetComponent<Player>();

        if (aPlayer != null)
        {
            isPlayerInSight = false;

            UIController.ThisUIController.SetNPCTalkHintVisible(false);
        }
    }

    // This is the event that happened after player talked with NPC.
    // Can use this to give player quest reward or reveal new quests. etc.
    public virtual void NPCEvent()
    {

    }
}
