using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class CheckDay : ConditionTask {

    public string day;

    protected override bool OnCheck()
    {
        return GameManager.Instance.currentDay.Equals(day);
    }

}
