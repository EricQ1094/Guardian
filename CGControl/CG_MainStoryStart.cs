using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_MainStoryStart : MonoBehaviour
{
    [SerializeField] private GameObject thisCaptain = null;
    private bool lookAtCaptain = false;
    [SerializeField] private GameObject thisCaptainLookAtPos = null;
    private bool moveToCapatainLookAtPos = false;
    // For change the scene so player can walk out the tutorial area.
    [SerializeField] private GameObject thisWoodtoHide = null;
    [SerializeField] private GameObject thisWoodtoShow = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMainQuestCGBehavior());
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtCaptain)
        {
            CGCamera.ThisCGCamera.CGCameraLookAt(thisCaptain.transform.position);
        }

        if (moveToCapatainLookAtPos)
        {
            CGCamera.ThisCGCamera.CGCameraMoveto(thisCaptainLookAtPos.transform.position, 3f);
        }
    }

    protected IEnumerator StartMainQuestCGBehavior()
    {
        CGController.ThisCGController.StartCG();
        // Shake Camera. Play sound of explosion and earthquake from far distance.
        CGCamera.ThisCGCamera.CallCGCameraShake(1f);

        thisWoodtoHide.SetActive(false);

        thisWoodtoShow.SetActive(true);

        yield return new WaitForSeconds(2f);

        UIController.ThisUIController.ShowCGDialogue("Player", "What just happened?");

        yield return new WaitForSeconds(2f);

        UIController.ThisUIController.ShowCGDialogue("Player", "Picnic abort. I need to report to Captain right now!");

        yield return new WaitForSeconds(3f);

        UIController.ThisUIController.ShowCGDialogue("Player", "I can find him at the entrance of the village. Near our armory.");

        moveToCapatainLookAtPos = true;

        lookAtCaptain = true;

        yield return new WaitForSeconds(5f);

        CGController.ThisCGController.EndCG();

        UIController.ThisUIController.CloseNPCDialogue();

        gameObject.SetActive(false);
    }
}
