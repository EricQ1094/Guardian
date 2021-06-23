using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectedItem : MonoBehaviour
{
    private static PlayerSelectedItem thisInstance;
    public static PlayerSelectedItem ThisItemSelector
    {
        get
        {
            return thisInstance;
        }
    }

    public Items thisSelectedItem = null;

    public Items thisMouseOnItem = null;
    // Start is called before the first frame update
    void Start()
    {
        thisInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisSelectedItem != null)
        {
            Vector3 aPos = Input.mousePosition;

            aPos.x += thisSelectedItem.GetComponent<Image>().sprite.rect.width /4f;
            aPos.y -= thisSelectedItem.GetComponent<Image>().sprite.rect.height /4f;

            thisSelectedItem.transform.position = aPos;
        }

        UpdateItemDescription();
    }

    protected void UpdateItemDescription()
    {
        if (thisMouseOnItem != null)
        {
            if (thisSelectedItem == null)
            {
                UIController.ThisUIController.ItemDescriptionOn(thisMouseOnItem.thisItemDescription);
            }

            else
            {
                UIController.ThisUIController.ItemDescriptionOff();
            }
        }
        else
        {
            UIController.ThisUIController.ItemDescriptionOff();
        }
    }

    public void DropSelectedItem()
    {
        Destroy(thisSelectedItem.gameObject);

        thisSelectedItem = null;
    }
}
