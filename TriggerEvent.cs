using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    // This Script enable a gameobject while Player on trigger enter.

    [SerializeField] private GameObject thisActiveEvent = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter(Collider other)
    {
        Player aPlayer = other.gameObject.GetComponent<Player>();

        if (aPlayer != null)
        {
            thisActiveEvent.SetActive(true);

            Destroy(gameObject);
        }
    }
}
