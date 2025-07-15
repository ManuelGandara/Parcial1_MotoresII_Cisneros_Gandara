using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEye : MonoBehaviour
{
    private GameManager gm;
    public int scorePoints;
    public GameObject Eyes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Objective" || other.gameObject.layer == 8)
        {
            gm.UpdateTheScore(scorePoints);
            SFXManager.Instance.PlayClip(0);
            Destroy(gameObject);
        }

        if (other.tag == "BottomTrigger")
        {
            gm.UpdateLives();
        }
    }
}
