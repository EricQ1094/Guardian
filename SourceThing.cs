using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceThing : MonoBehaviour
{
    [SerializeField] protected int thisHP = 0;
    public Items thisSource = null;
    public int thisNeededTool = 0;

    public void OnDamage(int aDamage)
    {
        thisHP -= aDamage;

        if (thisHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
