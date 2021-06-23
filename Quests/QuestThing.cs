using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestThing : MonoBehaviour
{
    public string thisQuestName = null;
    public string thisQuestDescription = null;
    public string[] thisQuestConditionsArray = null;
    public bool[] thisQuestConditionBoolsArray = null;
    protected bool isQuestFinished = false;

    public virtual void SetQuestFinish()
    {
        isQuestFinished = true;
    }

    public void AddDetailToDescription(string aDetail)
    {
        thisQuestDescription += '\n' + aDetail;
    }
}
