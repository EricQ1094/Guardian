using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable_Rabbit : AttackableThing
{
    private Animator thisAnimator = null;
    private bool isDead = false;
    private ItemObjs thisItemObj = null;
    // Start is called before the first frame update
    void Start()
    {
        thisItemObj = GetComponent<ItemObjs>();
        thisAnimator = GetComponent<Animator>();
        StartThing();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            thisItemObj.enabled = true;
        }

        else
        {
            thisItemObj.enabled = false;
        }
    }

    protected override void OnDeath()
    {
        StartCoroutine(DeathBehavior());
    }

    protected IEnumerator DeathBehavior()
    {
        thisAnimator.SetBool("isDead_1", true);

        yield return new WaitForSeconds(1.3f);

        isDead = true;
    }
}
