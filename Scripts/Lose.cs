using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
   

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Player")
        {
	    SceneManager.LoadScene(3);
        }
    }

} 
