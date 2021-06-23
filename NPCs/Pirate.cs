using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : NPCThing
{
    [SerializeField] private GameObject m_Head = null;
    private Vector3 m_OriginalLookingDir = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        StartThing();

        m_OriginalLookingDir = m_Head.transform.localEulerAngles; // Record Original looking direction for recover after player leave.
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThing();
        UpdateLookAtPlayer();
    }

    /// <summary>
    /// The head of this NPC will look at player's direction.
    /// </summary>
    protected void UpdateLookAtPlayer()
    {
        if (isPlayerInSight)
        {
            m_Head.transform.LookAt(m_Player.transform.GetChild(0));
        }

        else
        {
            m_Head.transform.localEulerAngles = m_OriginalLookingDir;
        }
    }
}
