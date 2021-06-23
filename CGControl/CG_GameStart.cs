using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_GameStart : MonoBehaviour
{
    // This Script controls The CG of GameStart.


    [SerializeField] private CameraController thisPlayerCamera = null;
    [SerializeField] private CGCamera thisCGCamera = null;
    private bool lookatCampfire = false;
    [SerializeField] private GameObject thisCampfire = null;
    private bool movetoForest = false;
    [SerializeField] private GameObject thisForestWayPoint = null;
    private bool lookatWoodlog = false;
    [SerializeField] private GameObject thisWoodlog = null;
    private bool movetoRiver = false;
    [SerializeField] private GameObject thisRiverWayPoint = null;
    private bool lookatFish = false;
    [SerializeField] private GameObject thisFish = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameStartCGBehavior());
    }

    // Update is called once per frame
    void Update()
    {
        if (lookatCampfire)
        {
            thisCGCamera.CGCameraLookAt(thisCampfire.transform.position);
        }

        else if (lookatWoodlog)
        {
            thisCGCamera.CGCameraLookAt(thisWoodlog.transform.position);
        }

        else if (lookatFish)
        {
            thisCGCamera.CGCameraLookAt(thisFish.transform.position);
        }

        if (movetoForest)
        {
            thisCGCamera.CGCameraMoveto(thisForestWayPoint.transform.position, 5f);
        }

        else if (movetoRiver)
        {
            thisCGCamera.CGCameraMoveto(thisRiverWayPoint.transform.position, 5f);
        }
    }

    protected IEnumerator GameStartCGBehavior()
    {
        thisPlayerCamera.gameObject.SetActive(false);

        thisCGCamera.gameObject.SetActive(true);

        lookatCampfire = true;

        yield return new WaitForSeconds(1f);

        UIController.ThisUIController.ShowCGDialogue("Player", "What a nice day for picnic, Let's cook some fish.");

        yield return new WaitForSeconds(4f);

        lookatCampfire = false;

        lookatWoodlog = true;

        movetoForest = true;

        UIController.ThisUIController.ShowCGDialogue("Player", "A wood log to start fire.");

        yield return new WaitForSeconds(5f);

        lookatWoodlog = false;

        movetoForest = false;

        movetoRiver = true;

        lookatFish = true;

        UIController.ThisUIController.ShowCGDialogue("Player", "And a fresh fish from river.");

        yield return new WaitForSeconds(5f);

        UIController.ThisUIController.HintNormal("New Quest Received. Press J to check");

        thisPlayerCamera.gameObject.SetActive(true);

        thisCGCamera.gameObject.SetActive(false);

        UIController.ThisUIController.CloseNPCDialogue();

        Destroy(gameObject);
    }

    // Look at campfire.
    // Move to forest, look at wood log.
    // Move to river, look at fish.
}
