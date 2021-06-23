using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_IntroMarketNPCs : MonoBehaviour
{
    [SerializeField] private GameObject thisMerchantPos = null;
    private bool lookAtMerchant = false;
    [SerializeField] private GameObject thisSmithPos = null;
    private bool lookAtSmith = false;
    [SerializeField] private GameObject thisMovetoPos = null;
    private bool moveToPos = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtMerchant)
        {
            CGCamera.ThisCGCamera.CGCameraLookAt(thisMerchantPos.transform.position);
        }

        else if (lookAtSmith)
        {
            CGCamera.ThisCGCamera.CGCameraLookAt(thisSmithPos.transform.position);
        }

        if (moveToPos)
        {
            CGCamera.ThisCGCamera.CGCameraMoveto(thisMovetoPos.transform.position, 10f);
        }
    }

    protected IEnumerator IntroCannonCGBehavior()
    {
        CGController.ThisCGController.StartCG();

        moveToPos = true;

        UIController.ThisUIController.ShowCGDialogue("Player", "Here is the small market of village.");

        yield return new WaitForSeconds(4f);

        lookAtMerchant = true;

        UIController.ThisUIController.ShowCGDialogue("Player", "She is the merchant. I can buy and sell stuff from her.");

        yield return new WaitForSeconds(4f);

        lookAtMerchant = false;

        lookAtSmith = true;

        UIController.ThisUIController.ShowCGDialogue("Player", "The best smith of the village. He can help me craft.");

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
