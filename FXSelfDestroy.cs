using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSelfDestroy : MonoBehaviour
{
    [SerializeField] private float thisSelfDestroyTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(thisSelfDestroyTime);

        Destroy(gameObject);
    }
}
