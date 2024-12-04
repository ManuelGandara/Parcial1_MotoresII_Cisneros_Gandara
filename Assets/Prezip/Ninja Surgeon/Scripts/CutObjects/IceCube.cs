using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCube : MonoBehaviour
{
    public float timeStopDuration = 4f;

    private float rotationForce = 200;
    public ParticleSystem IceCubeParticle;

    void Update()
    {
        transform.Rotate(Vector2.right * Time.deltaTime * rotationForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blade")
        {
            Destroy(gameObject);
            Instantiate(IceCubeParticle, transform.position, IceCubeParticle.transform.rotation);

            StartCoroutine(FreezeObjectives());
        }
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


