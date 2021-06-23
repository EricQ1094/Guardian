using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable_KeyChar : AttackableThing
{
    [SerializeField] private GameObject[] thisKeyEventToTrigger = null; // Trigger special event by setactive the script object.
    // Start is called before the first frame update
    void Start()
    {
        StartThing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnDeath()
    {
        base.OnDeath();

        foreach (GameObject aKeyEventToTrigger in thisKeyEventToTrigger)
        {
            aKeyEventToTrigger.SetActive(true);
        }

        UIController.ThisUIController.CloseNPCDialogue();
    }
}
