using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideThing : MonoBehaviour
{
    protected CharacterController thisCharCon = null;
    protected Animator thisAnimator = null;
    public GameObject thisRidePosition = null;
    [SerializeField] protected float thisMoveSpeed = 0f;
    public bool isRiding = false;
    public Player thisPlayer = null;
    // Start is called before the first frame update
    protected void StartThing()
    {
        thisCharCon = GetComponent<CharacterController>();
        thisAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void UpdateThing()
    {
        
    }
}
