using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameBehavior : MonoBehaviour
{
    [SerializeField] private Boss thisBoss = null;
    protected void Start()
    {
        StartCoroutine(EndGameBahavior());
    }
    protected IEnumerator EndGameBahavior()
    {
        // End Game. Triggered after Boss been defeated.
        UIController.ThisUIController.TriggerBossFightHud(false);

        yield return new WaitForSeconds(2f);

        Destroy(thisBoss);

        UIController.ThisUIController.SetEndGamePanelActive(true);
    }
}
