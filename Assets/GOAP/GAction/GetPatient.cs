using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatient : GAction
{
    private GameObject resource;

    public override bool PrePerform()
    {
        target = GWorld.Instance.RemovePatient();
        if (target == null) { return false; } //if no patient found, return target

        resource = GWorld.Instance.RemoveCubicle();
        if (resource != null)
            inventory.AddItem(resource);
        else
        {
            GWorld.Instance.AddPatient(target);//if we can't get a cubicle, we need to release the patient
            target = null;
            return false;
        }

        GWorld.Instance.GetWorld().ModifyState("Freeubicle", -1);
        return true;
    }

    public override bool PostPerform()
    {
        //take away waiting patient
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        if (target)
            target.GetComponent<GAgent>().inventory.AddItem(resource);//add item to the patient inventory
        return true;
    }
}