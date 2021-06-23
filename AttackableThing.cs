using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableThing : MonoBehaviour
{
    [SerializeField] private int thisHP = 0;
    private AudioSource thisAudioSource = null;
    [SerializeField] private AudioClip[] thisHurtSounds = null;
    // Start is called before the first frame update
    protected void StartThing()
    {
        thisAudioSource = GetComponent<AudioSource>();
    }

    public void OnDamage(int aDamage)
    {
        int aRandomIndex = Random.Range(0, thisHurtSounds.Length);

        print(aRandomIndex);

        AudioClip aRandomHurtSound = thisHurtSounds[aRandomIndex];

        thisAudioSource.PlayOneShot(aRandomHurtSound);

        thisHP -= aDamage;

        if (thisHP <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        UIController.ThisUIController.SetNPCTalkHintVisible(false);

        gameObject.SetActive(false);
    }
}
