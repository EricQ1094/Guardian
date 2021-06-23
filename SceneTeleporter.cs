using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleporter : MonoBehaviour
{
    [SerializeField] private Transform thisTeleportToPos = null;
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
            StartCoroutine(TeleportPlayerBehavior());

            aPlayer.MovePlayerTo(thisTeleportToPos.position);
        }
    }

    protected IEnumerator TeleportPlayerBehavior()
    {
        UIController.ThisUIController.ShowLoadingScreen();

        yield return new WaitForSeconds(1.0f);

        UIController.ThisUIController.HideLoadingScreen();
    }
}
