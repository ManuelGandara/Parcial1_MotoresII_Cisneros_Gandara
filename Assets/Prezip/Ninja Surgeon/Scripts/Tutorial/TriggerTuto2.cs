using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTuto2 : MonoBehaviour
{
    public ManagerT tutorial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == tutorial.Eye)
        {
            Debug.Log("collisiono con 1");
            tutorial.ShowMessage2();
        }
    }
}
