using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookObj : MonoBehaviour
{
    // This is the GameObject inside realgame for Cooker.
    // Use with the Cooker UI component.

    [SerializeField] private GameObject thisFireVFX = null;
    [SerializeField] private GameObject thisFireLight = null;
    private bool isCookerOn = false;
    private float thisCookerOnTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        thisCookerOnTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCookerOn)
        {
            thisFireVFX.SetActive(true);
            thisFireLight.SetActive(true);

            thisCookerOnTimer -= Time.deltaTime;
        }

        else
        {
            thisFireVFX.SetActive(false);
            thisFireLight.SetActive(false);
        }

        if (thisCookerOnTimer <= 0f)
        {
            isCookerOn = false;
            thisCookerOnTimer = 5f;
        }
    }

    public void OpenCooker()
    {
        UIController.ThisUIController.OpenCookMenu();
    }

    public void CookerOn()
    {
        isCookerOn = true;
    }
}
