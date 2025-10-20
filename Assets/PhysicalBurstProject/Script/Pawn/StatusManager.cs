using System.Collections;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    IStatus status;

    private bool IsActable;

    public void setDependency(IStatus status)
    {
        this.status = status;
    }



}