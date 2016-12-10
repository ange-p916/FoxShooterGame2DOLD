using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BirdScript : MonoBehaviour {

    public float randomVal;
    public LayerMask WhatIsBird;
    public float dstFromBirds;
    List<BirdScript> birds = new List<BirdScript>();
    Rigidbody2D rb2d;

    void Start()
    {
        birds = FindObjectsOfType<BirdScript>().ToList();
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(CrazyMovement());
    }

    void Update()
    {
        
    }

    IEnumerator CrazyMovement()
    {
        var rayLength = Mathf.Abs(rb2d.velocity.x);
        foreach (var bird in birds)
        {
            var circle = Physics2D.CircleCast(transform.position, 10f, Vector2.zero, 10f);
            var hit = Physics2D.Raycast(transform.position, bird.transform.position, rayLength, WhatIsBird);
            var totalHitDist = hit.distance - dstFromBirds;
            yield return new WaitForSeconds(1f);
            dstFromBirds = Random.Range(-randomVal, randomVal);

            Debug.DrawLine(transform.position, bird.transform.position, Color.green);
            if (hit)
            {
                rb2d.velocity = new Vector2(totalHitDist, totalHitDist);
                rayLength = hit.distance;
            }
        }
    }

}
