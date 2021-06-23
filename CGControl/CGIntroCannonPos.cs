using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGIntroCannonPos : MonoBehaviour
{
    [SerializeField] private GameObject thisCannonPos = null;
    private bool lookAtCannon = false;
    [SerializeField] private GameObject thisLookAtCannonPos = null;
    private bool moveToLooAtCannonPos = false;
    [SerializeField] private GameObject thisBossPos = null;
    private bool lookAtBoss = false;
    // Update is called once per frame
    void Update()
    {
        if (lookAtCannon)
        {
            CGCamera.ThisCGCamera.CGCameraLookAt(thisCannonPos.transform.position);
        }

        else if(lookAtBoss)
        {
            CGCamera.ThisCGCamera.CGCameraLookAt(thisBossPos.transform.position);
        }

        if (moveToLooAtCannonPos)
        {
            CGCamera.ThisCGCamera.CGCameraMoveto(thisLookAtCannonPos.transform.position, 3f);
        }
    }

    protected IEnumerator IntroCannonCGBehavior()
    {
        CGController.ThisCGController.StartCG();

        moveToLooAtCannonPos = true;

        lookAtCannon = true;

        yield return new WaitForSeconds(2f);

        UIController.ThisUIController.ShowCGDialogue("Player", "Here is the place to build the giant cannon.");

        yield return new WaitForSeconds(4f);

        lookAtCannon = false;

        lookAtBoss = true;

        UIController.ThisUIController.ShowCGDialogue("Player", "The monster will come from this way. We need to stop it here.");

        yield return new WaitForSeconds(4f);

        CGController.ThisCGController.EndCG();

        UIController.ThisUIController.CloseNPCDialogue();

        Destroy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        Player aPlayer = other.gameObject.GetComponent<Player>();

        if (aPlayer != null)
        {
            StartCoroutine(IntroCannonCGBehavior());
        }
    }
}
