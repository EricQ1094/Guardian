using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGCamera : MonoBehaviour
{
    // This is the Camera that used in CG.

    private static CGCamera thisInstance;

    public static CGCamera ThisCGCamera
    {
        get
        {
            return thisInstance;
        }
    }
    
    private Vector3 thisOriginalPos = Vector3.zero;
    private float thisCameraShakeTimer = 0f;
    [SerializeField] private Image thisUIFadeObj = null;
    private float thisFadeValue = 0f;
    public bool isUIFadeOut = false;
    protected void Awake()
    {
        thisInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        thisOriginalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisCameraShakeTimer > 0f)
        {
            thisCameraShakeTimer -= Time.deltaTime;
            CGCameraShake();
        }

        if (isUIFadeOut)
        {
            thisUIFadeObj.gameObject.SetActive(true);
            thisFadeValue += Time.deltaTime;
            thisFadeValue = Mathf.Clamp(thisFadeValue, 0f, 1f);

            CGCameraFade();
        }

        else
        {
            thisFadeValue -= Time.deltaTime;
            thisFadeValue = Mathf.Clamp(thisFadeValue, 0f, 1f);

            CGCameraFade();

            if (thisFadeValue <= 0f)
            {
                thisUIFadeObj.gameObject.SetActive(false);
            }
        }
    }

    public void CGCameraLookAt(Vector3 aObjectPos)
    {
        Vector3 direction = aObjectPos - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 3f * Time.deltaTime);
    }

    public void CGCameraMoveto(Vector3 aPosition, float aTime)
    {
        float aDistance = (aPosition - transform.position).magnitude;
        float aSpeed = aDistance / aTime;

        transform.position = Vector3.MoveTowards(transform.position, aPosition, aSpeed * Time.deltaTime);
    }
    // Reset Camera Position to original position.
    public void CGCameraResetPosition()
    {
        transform.localPosition = new Vector3(0f, 1f, 0f);
    }

    private void CGCameraShake()
    {
        Vector3 aRandomShakePos = thisOriginalPos;
        aRandomShakePos.x += Random.Range(-0.1f, 0.1f);
        aRandomShakePos.y += Random.Range(-0.1f, 0.1f);

        transform.localPosition = aRandomShakePos;
    }

    public void CallCGCameraShake(float aShakeDuration)
    {
        thisCameraShakeTimer = aShakeDuration;
    }

    private void CGCameraFade()
    {
        Color aBlackColor = Color.black;
        Color aTransparentColor = aBlackColor;
        aTransparentColor.a = 0f;

        Color aLerpColor = Color.Lerp(aTransparentColor, aBlackColor, thisFadeValue);

        thisUIFadeObj.color = aLerpColor;
    }
    public void SetCameraFadeOut(bool aBool)
    {
        isUIFadeOut = aBool;
    }
}
