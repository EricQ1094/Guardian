using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator thisAnimator = null;
    [SerializeField] float thisMaxHealth = 0f;
    private float thisCurrentHealth = 0f;
    [SerializeField] private GameObject thisEndGameObj = null;
    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = GetComponent<Animator>();

        thisCurrentHealth = thisMaxHealth;

        UIController.ThisUIController.TriggerBossFightHud(true);
    }

    // Update is called once per frame
    void Update()
    {
        UIController.ThisUIController.UpdateBossHPHud(thisCurrentHealth / thisMaxHealth);
    }

    public void OnDamage(float aDamage)
    {
        thisCurrentHealth -= aDamage;

        if (thisCurrentHealth <= 0f)
        {
            thisEndGameObj.SetActive(true);

            thisAnimator.SetBool("isDead1", true);
        }
    }
}
