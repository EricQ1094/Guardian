using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_ParrotQuest : MonoBehaviour
{
    // This Script controls The CG of ParrotQuest Part (1/4).

    [SerializeField] private CameraController thisPlayerCamera = null;
    [SerializeField] private CGCamera thisCGCamera = null;
    [SerializeField] private GameObject thisStartPos = null;
    private bool lookAtPirate = false;
    [SerializeField] private GameObject thisPirate = null;
    private bool lookAtParrot = false;
    [SerializeField] private GameObject thisParrot = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtPirate)
        {
            thisCGCamera.CGCameraLookAt(thisPirate.transform.position);
        }

        else if (lookAtParrot)
        {
            thisCGCamera.CGCameraLookAt(thisParrot.transform.position);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        Player aPlayer = other.gameObject.GetComponent<Player>();

        if (aPlayer != null)
        {
            StartCoroutine(StartParrotCGBehavior());
        }
    }

    protected IEnumerator StartParrotCGBehavior()
    {
        thisPlayerCamera.gameObject.SetActive(false);

        thisCGCamera.gameObject.SetActive(true);

        thisCGCamera.transform.position = thisStartPos.transform.position;

        lookAtParrot = true;

        UIController.ThisUIController.ShowCGDialogue("Parrot", "Help guardian! Help Me!!");

        yield return new WaitForSeconds(3f);

        lookAtParrot = false;

        lookAtPirate = true;

        UIController.ThisUIController.ShowCGDialogue("Pirate", "Shut you damn mouth!");

        yield return new WaitForSeconds(3f);

        lookAtPirate = false;

        lookAtParrot = true;

        UIController.ThisUIController.ShowCGDialogue("Parrot", "No! Help me!! He is the bad guy! Kill him or I will die soon!");

        yield return new WaitForSeconds(3f);

        thisPlayerCamera.gameObject.SetActive(true);

        thisCGCamera.gameObject.SetActive(false);

        UIController.ThisUIController.CloseNPCDialogue();

        UIController.ThisUIController.HintNormal("Start Quest - Parrot and Pirate");

        UIController.ThisUIController.UnlockQuest_ParrotandPirate();

        Destroy(gameObject);
    }
}
