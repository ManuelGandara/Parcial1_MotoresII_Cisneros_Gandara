using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCube : MonoBehaviour
{
    public float timeStopDuration = 5f;

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

            GameManager.instance.StopTime(timeStopDuration);
        }
    }
}
