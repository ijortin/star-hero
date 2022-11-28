using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float walkSpeed;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;
    public Rigidbody2D rb;
    public Transform gc;
    public LayerMask groundLayer;
	
    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(mustPatrol){
		Patroling();
	}
    }

    private void FixedUpdate(){
    	if(mustPatrol){
		mustTurn = !Physics2D.OverlapCircle(gc.position, 0.1f, groundLayer);
	}
    }

    void Patroling(){
	if(mustTurn){
		Flip();
	}
	rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip(){
	mustPatrol = false;
	transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
	walkSpeed *= -1;
	mustPatrol = true;
    }
}
