using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCube : MonoBehaviour
{
    public float timeStopDuration = 4f;
    private float rotationForce = 200;
    public int scorePoints;
    private GameManager gm;

    public GameObject slicedEye;
    public GameObject EyeBlood;
    private Rigidbody rb;
    //public ParticleSystem IceCubeParticle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.Rotate(Vector2.right * Time.deltaTime * rotationForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blade")
        {
            gm.UpdateTheScore(scorePoints);
            SFXManager.Instance.PlayClip(0);
            Destroy(gameObject);
            InstantiateSlicedEye();
            StartCoroutine(FreezeObjectives());

        }
    }

    private void InstantiateSlicedEye()
    {
        GameObject instantiatedEye = Instantiate(slicedEye, transform.position, transform.rotation);
        GameObject instantiatedBlood = Instantiate(EyeBlood, new Vector3(transform.position.x, transform.position.y, 0), EyeBlood.transform.rotation);

        Rigidbody[] slicedRb = instantiatedEye.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody srb in slicedRb)
        {
            srb.AddExplosionForce(130f, transform.position, 10);
            srb.velocity = rb.velocity * 1.2f;
        }

        Destroy(instantiatedEye, 5);
        Destroy(instantiatedBlood, 5);
    }

    private IEnumerator FreezeObjectives()
    {
        GameObject[] objectives = GameObject.FindGameObjectsWithTag("Objective");

        foreach (GameObject obj in objectives)
        {
            if (obj.TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }
        }

        yield return new WaitForSeconds(timeStopDuration);

        foreach (GameObject obj in objectives)
        {
            if (obj.TryGetComponent(out Rigidbody rb))
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.useGravity = true;
            }
        }
    }
}


