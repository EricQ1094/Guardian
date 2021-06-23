using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_MiningCave : MonoBehaviour
{
    [SerializeField] private GameObject thisIronOre = null;
    [SerializeField] private GameObject thisIronOreLookAtPos = null;
    private bool lookAtIronOre = false;
    private bool moveToIronOre = false;
    [SerializeField] private GameObject thisUpperSpace = null;
    private bool lookAtUpperSpace = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtIronOre)
        {
            CGCamera.ThisCGCamera.CGCameraLookAt(thisIronOre.transform.position);
        }

        else if (lookAtUpperSpace)
        {
            CGCamera.ThisCGCamera.CGCameraLookAt(thisUpperSpace.transform.position);
        }

        if (moveToIronOre)
        {
            CGCamera.ThisCGCamera.CGCameraMoveto(thisIronOreLookAtPos.transform.position, 2f);
        }

    }

    protected IEnumerator GameStartCGBehavior()
    {
        CGController.ThisCGController.StartCG();

        UIController.ThisUIController.ShowCGDialogue(" ", "Here is the mining cave.");

        yield return new WaitForSeconds(3f);

        lookAtIronOre = true;

        moveToIronOre = true;

        UIController.ThisUIController.ShowCGDialogue(" ", "That's the Iron Ore I need for casting a Cannon Barrel.");

        yield return new WaitForSeconds(5f);

        lookAtIronOre = false;

        UIController.ThisUIController.ShowCGDialogue(" ", "I wandering what is up there?");

        lookAtUpperSpace = true;

        yield return new WaitForSeconds(3f);

        CGController.ThisCGController.EndCG();

        UIController.ThisUIController.CloseNPCDialogue();

        Destroy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        Player aPlayer = other.gameObject.GetComponent<Player>();

        if (aPlayer != null)
        {
            StartCoroutine(GameStartCGBehavior());
        }
    }
}
