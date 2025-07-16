using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTuto1 : MonoBehaviour
{
    public ManagerT tutorial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == tutorial.Eye)
        {
            tutorial.ShowMessage1();
        }
    }
}
