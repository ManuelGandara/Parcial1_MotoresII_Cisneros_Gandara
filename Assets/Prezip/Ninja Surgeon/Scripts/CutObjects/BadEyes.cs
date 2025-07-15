using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEyes : MonoBehaviour
{
    private float rotationForce = 200;
    public ParticleSystem BadEyeParticle;


    void Update()
    {
        transform.Rotate(Vector2.right * Time.deltaTime * rotationForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blade")
        {
            Destroy(gameObject);
            SFXManager.Instance.PlayClip(1);
            Instantiate(BadEyeParticle, transform.position, BadEyeParticle.transform.rotation);
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
