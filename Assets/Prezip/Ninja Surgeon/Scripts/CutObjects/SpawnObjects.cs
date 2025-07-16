using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject evilEye;
    public float left;
    public float right;

    void Start()
    {
        StartCoroutine(SpawnRandomObject());
    }

    private IEnumerator SpawnRandomObject()
    {
        yield return new WaitForSeconds(1);

        while (FindObjectOfType<GameManager>().gameIsOver == false && FindAnyObjectByType<GameManager>().gameVictory == false)
        {
            InstantiateRandomObject();
            yield return new WaitForSeconds(RandomRepeatrate());
        }
    }

    private void InstantiateRandomObject()
    {
        GameObject eye;

        if (Random.Range(0f, 1f) > RemoteConfigManager.Instance.RemoteConfigValues.EvilEyesProbability)
        {
            eye = objects[Random.Range(0, objects.Length)];
        }
        else
        {
            eye = evilEye;
        }

        GameObject obj = Instantiate(eye, transform.position, eye.transform.rotation);

        obj.GetComponent<Rigidbody>().AddForce(RandomVector() * RandomForce(), ForceMode.Impulse);

        obj.transform.rotation = Random.rotation;
    }

    private float RandomForce()
    {
        float force = Random.Range(14f, 16f);
        return force;
    }

    private float RandomRepeatrate()
    {
        float repeatrate = Random.Range(0.5f, 3f);
        return repeatrate;
    }

    private Vector2 RandomVector()
    {
        Vector2 moveDirection = new Vector2(Random.Range(left, right), 1).normalized;
        return moveDirection;
    }
}
