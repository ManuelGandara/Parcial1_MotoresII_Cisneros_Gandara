using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    private GameManager gm;
    public GameObject slicedEye;
    public GameObject EyeBlood;
    private float rotationForce = 200;
    private Rigidbody rb;
    public int scorePoints;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.Rotate(Vector2.right * Time.deltaTime * rotationForce);
    }

    private void InstantiateSlicedEye()
    {
        GameObject instantiatedEye = Instantiate(slicedEye, transform.position, transform.rotation);
        GameObject instantiatedBlood = Instantiate(EyeBlood, new Vector3(transform.position.x, transform.position.y, 0), EyeBlood.transform.rotation);

        Rigidbody[] slicedRb = instantiatedEye.transform.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody srb in slicedRb)
        {
            srb.AddExplosionForce(130f, transform.position, 10);
            srb.velocity = rb.velocity * 1.2f;
        }

        Destroy(instantiatedEye, 5);
        Destroy(instantiatedBlood, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Blade")
        {
            gm.UpdateTheScore(scorePoints);
            Destroy(gameObject);
            InstantiateSlicedEye();
        }

        if(other.tag == "BottomTrigger")
        {
            gm.UpdateLives();
        }
    }
}
