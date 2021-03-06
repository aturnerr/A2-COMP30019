﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour {

    public GameObject explosionEffect;
    public Transform target;
    public Vector3 targetDirection;
    readonly float throwForce = 2f;
    float countdown = 2f;
    bool exploded;

    void Start()
    {
        StartCoroutine(ExecuteAfterTime(0.5f));
    }
    // Update is called once per frame
    void Update () {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !exploded){
            exploded = true;
            GameObject explosion = Instantiate(explosionEffect);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<interaction>().OnPlayerHit();
            GameObject explosion = Instantiate(explosionEffect);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        targetDirection = target.position - transform.position;
        gameObject.GetComponent<Rigidbody>().AddForce(targetDirection * throwForce, ForceMode.Impulse);
    }
}
