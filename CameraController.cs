using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private static CameraController thisInstance;
    public static CameraController ThisPlayerCamera
    {
        get
        {
            return thisInstance;
        }
    }

    [SerializeField] private Player thisPlayer = null;
    [SerializeField] private float CameraRotateSpeed = 0f;
    private float MouseX = 0f;
    private float MouseY = 0f;
    // Start is called before the first frame update
    void Start()
    {
        thisInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisPlayer.canPlayerControl)
        {
            UpdateCameraMovement();
        }
    }

    private void LateUpdate()
    {
        UpdateCameraFollowPlayer();
    }

    protected void UpdateCameraMovement()
    {
        MouseX += Input.GetAxis("Mouse X") * CameraRotateSpeed;
        MouseY -= Input.GetAxis("Mouse Y") * CameraRotateSpeed;
        MouseY = Mathf.Clamp(MouseY, -80f, 80f);

        thisPlayer.transform.rotation = Quaternion.Euler(0f, MouseX, 0f);

        transform.rotation = Quaternion.Euler(MouseY, MouseX, 0f);
    }

    protected void UpdateCameraFollowPlayer()
    {
        Vector3 aCamPos = thisPlayer.transform.position;
        aCamPos.y += 0.5f;

        transform.position = aCamPos;
    }
}
