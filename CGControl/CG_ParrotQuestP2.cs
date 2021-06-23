using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_ParrotQuestP2 : MonoBehaviour
{
    // This Script controls The CG of ParrotQuest Part (2/4).


    [SerializeField] private CameraController thisPlayerCamera = null;
    [SerializeField] private CGCamera thisCGCamera = null;
    private bool lookAtParrot = false;
    [SerializeField] private GameObject thisParrot = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartParrotCGP2Behavior());
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtParrot)
        {
            thisCGCamera.CGCameraLookAt(thisParrot.transform.position);
        }
    }
    protected IEnumerator StartParrotCGP2Behavior()
    {
        thisPlayerCamera.gameObject.SetActive(false);

        thisCGCamera.gameObject.SetActive(true);

        lookAtParrot = true;

        UIController.ThisUIController.ShowCGDialogue("Parrot", "Thank you Guardian!");

        yield return new WaitForSeconds(3f);

        UIController.ThisUIController.ShowCGDialogue("Parrot", "I'm just a normal prince. But this pirate kidnapped me and turned me into a parrot!");

        yield return new WaitForSeconds(4f);

        UIController.ThisUIController.ShowCGDialogue("Parrot", "Could you help me turn back to human?");

        yield return new WaitForSeconds(3f);

        UIController.ThisUIController.ShowCGDialogue("Parrot", "Only the yellow flower that grow at the highest place of a dark cave has that kind of magic power!");

        yield return new WaitForSeconds(4f);

        UIController.ThisUIController.ShowCGDialogue("Parrot", "Please help me and I will pay you well! I'm a prince! I mean.. I was.");

        yield return new WaitForSeconds(4f);

        thisPlayerCamera.gameObject.SetActive(true);

        thisCGCamera.gameObject.SetActive(false);

        UIController.ThisUIController.CloseNPCDialogue();

        Destroy(gameObject);
    }
}
