using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smith : NPCThing
{
    [SerializeField] private GameObject m_Head = null;
    private Vector3 m_OriginalLookingDir = Vector3.zero;
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

    protected override void UpdatePlayerInsight()
    {
        if (isPlayerInSight)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                UIController.ThisUIController.SetNPCTalkHintVisible(false);

                UIController.ThisUIController.ShowCraftMenu();
            }
        }
    }
}
