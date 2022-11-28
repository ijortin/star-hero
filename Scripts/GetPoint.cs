using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
                PointCounter.instance.AddPoint();
		Destroy(gameObject);
        }
    }
}