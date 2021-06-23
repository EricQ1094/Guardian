using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : RideThing
{
    private AudioSource thisAudioSource = null;
    [SerializeField] private AudioClip thisHorseRunSound = null;
    // Start is called before the first frame update
    void Start()
    {
        StartThing();

        thisAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRiding)
        {
            UpdateHorseMovement();
            UpdateHorseAnimation();
        }

        if (thisPlayer != null)
        {
            if (thisPlayer != null)
            {
                thisPlayer.canPlayerMove = false;
            }

            else
            {
                thisPlayer.canPlayerMove = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayHorseRunSound();
        }

        
    }
    protected void UpdateHorseMovement()
    {
        Vector3 aDir = Vector3.zero;

        float aVInput = Input.GetAxis("Vertical");
        if (aVInput != 0f)
        {
            aDir += transform.forward * aVInput;
        }

        thisCharCon.SimpleMove(aDir * thisMoveSpeed);
    }

    protected void UpdateHorseAnimation()
    {
        if (thisCharCon.velocity != Vector3.zero)
        {
            thisAnimator.SetBool("isRunning", true);
        }

        else
        {
            thisAnimator.SetBool("isRunning", false);
        }
    }

    protected void PlayHorseRunSound()
    {
        thisAudioSource.PlayOneShot(thisHorseRunSound);
    }
}
