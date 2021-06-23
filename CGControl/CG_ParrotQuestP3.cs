using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_ParrotQuestP3 : MonoBehaviour
{
    // This is the CG that intro player to build a bridge to get Magic Flower.
    private bool lookAtMagicFlower = false;
    [SerializeField] private GameObject thisMagicFlower = null;
    private bool lookAtBridge = false;
    [SerializeField] private GameObject thisBridgeBuildPos = null;
    [SerializeField] private Quest_ParrotandPirate thisQuest = null;
    [SerializeField] private GameObject thisBridgeBuildTrigger = null;

    protected void Update()
    {
        if (lookAtMagicFlower)
        {
            CGCamera.ThisCGCamera.CGCameraLookAt(thisMagicFlower.transform.position);
        }

        else if (lookAtBridge)
        {
            CGCamera.ThisCGCamera.CGCameraLookAt(thisBridgeBuildPos.transform.position);
        }
    }
    protected void OnTriggerEnter(Collider other)
    {
        Player aPlayer = other.gameObject.GetComponent<Player>();

        if (aPlayer != null)
        {
            StartCoroutine(MagicFlowerIntroBehavior());
        }
    }

    protected IEnumerator MagicFlowerIntroBehavior()
    {
        CGController.ThisCGController.StartCG();

        lookAtMagicFlower = true;

        UIController.ThisUIController.ShowCGDialogue("Player", "That must be the magic flower the prince parrot was talked about!");

        yield return new WaitForSeconds(5f);

        lookAtMagicFlower = false;

        lookAtBridge = true;

        UIController.ThisUIController.ShowCGDialogue("PLayer", "I need to build a bridge to get it.");

        yield return new WaitForSeconds(3f);

        UIController.ThisUIController.ShowCGDialogue("PLayer", "5 Wood Logs should be enough.");

        yield return new WaitForSeconds(3f);

        CGController.ThisCGController.EndCG();

        UIController.ThisUIController.CloseNPCDialogue();

        thisQuest.isPlayerCheckedCave = true;

        thisBridgeBuildTrigger.SetActive(true);

        Destroy(gameObject);
    }
}
