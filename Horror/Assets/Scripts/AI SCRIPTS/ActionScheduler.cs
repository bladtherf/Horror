﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{

    MonoBehaviour currentAction;

    public void StartAction(MonoBehaviour action)
    {
        if (currentAction == action) return;
        if (currentAction != null)
        {
            print("Cancelling" + currentAction);
        }
        currentAction = action;
    }

    public void CancelCurrentAction()
    {
        StartAction(null);
    }
}
