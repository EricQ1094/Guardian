using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGController : MonoBehaviour
{
    private static CGController thisInstance;

    public static CGController ThisCGController
    {
        get
        {
            return thisInstance;
        }
    }

    [SerializeField] private CameraController thisPlayerCamera = null;
    [SerializeField] private CGCamera thisCGCamera = null;

    protected void Awake()
    {
        thisInstance = this;
    }
    public void StartCG()
    {
        thisCGCamera.gameObject.SetActive(true);

        thisCGCamera.CGCameraResetPosition();

        thisCGCamera.transform.localRotation = thisPlayerCamera.transform.localRotation;

        thisPlayerCamera.gameObject.SetActive(false);
    }

    public void EndCG()
    {
        thisCGCamera.gameObject.SetActive(false);

        thisPlayerCamera.gameObject.SetActive(true);
    }
}
