using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Rigidbody rb;
    private SphereCollider sc;
    private TrailRenderer tr;
    private GameManager gm;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
        tr = GetComponent<TrailRenderer>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gm.gameIsOver == false)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    tr.enabled = true;
                    sc.enabled = true;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    tr.enabled = false;
                    sc.enabled = false;
                }
                BladeFollowTouch(touch);
            }
        }
    }

    private void BladeFollowTouch(Touch touch)
    {
        Vector3 touchPos = touch.position;
        touchPos.z = 8;
        rb.position = Camera.main.ScreenToWorldPoint(touchPos);
    }
}

