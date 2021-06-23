using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ItemtoInventoryEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * 400f * Time.deltaTime);
    }

    protected IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
