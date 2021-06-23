using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_BossFight : MonoBehaviour
{
    private AudioSource thisAudioSource = null;
    [SerializeField] private AudioClip thisCraftCannonSound = null;
    [SerializeField] private GameObject thisCannon = null;
    [SerializeField] private GameObject thisBoss = null;
    // Start is called before the first frame update
    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();

        StartCoroutine(BossFightStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator BossFightStart()
    {
        // Screen Fade out.
        // Play building cannon sounds.
        // Screen Fade in.
        // Intro Boss and Cannon.
        // Start Boss Fight.

        CGController.ThisCGController.StartCG();

        CGCamera.ThisCGCamera.SetCameraFadeOut(true);

        yield return new WaitForSeconds(1f);

        thisAudioSource.PlayOneShot(thisCraftCannonSound);

        yield return new WaitForSeconds(3f);

        thisCannon.SetActive(true);
        thisBoss.SetActive(true);

        CGCamera.ThisCGCamera.SetCameraFadeOut(false);

        yield return new WaitForSeconds(1f);

        UIController.ThisUIController.ShowCGDialogue("Player", "The monster is here. Time to finish this.");

        yield return new WaitForSeconds(2f);

        UIController.ThisUIController.CloseNPCDialogue();

        CGController.ThisCGController.EndCG();
    }
}
