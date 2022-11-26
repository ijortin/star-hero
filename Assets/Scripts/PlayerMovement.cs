// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } */

    [SerializeField] private float moveSpeedCap;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float currentMoveSpeed;
    private int wallJumpWindow;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake() {

        // Grab references to components from object
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Update() {

        float horizontalInput = Input.GetAxis("Horizontal");

        // Flips player along x-axis according to travel direction
        if (horizontalInput > 0.01f) {
            transform.localScale = Vector3.one;
            if (currentMoveSpeed < moveSpeedCap && wallJumpWindow <= 0) {
                currentMoveSpeed += moveSpeedCap / 100;
            }
        }
        else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);
            if (currentMoveSpeed < moveSpeedCap && wallJumpWindow <= 0) {
                currentMoveSpeed += moveSpeedCap / 100;
            }
        }
        else {
            currentMoveSpeed = 0;
        }

        // Set animator parameters
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());
        animator.SetFloat("vertical_velocity", body.velocity.y);
        animator.SetFloat("horizontal_speed", Mathf.Abs(body.velocity.x));
        animator.SetInteger("walljump_window", wallJumpWindow);

        if (Input.GetKey(KeyCode.Space)) {
            Jump();
        }
        /* if (Mathf.Abs(body.velocity.x)  moveSpeedCap) {
            body.AddForce(new Vector2(horizontalInput * moveSpeedCap, body.velocity.y));
        } */
        body.velocity = new Vector2(horizontalInput * moveSpeedCap, body.velocity.y);

        // Check wall jump occurrence 
        if (wallJumpWindow > 0) {
            wallJumpWindow--;
        }
        if (onWall() && !isGrounded() && ((Input.GetKeyDown(KeyCode.LeftArrow) && transform.localScale.x > 0) || (Input.GetKeyDown(KeyCode.RightArrow) && transform.localScale.x < 0))) {
            wallJumpWindow = 60;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // print(onWall());

    }

    private void Jump() {

        if (isGrounded()) {
            animator.SetTrigger("jump");
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            print("jump");
        }
        else if (wallJumpWindow > 0) {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeedCap / 3, jumpSpeed);
            wallJumpWindow = 0;
            print("walljump");
        }

    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        
    }

    private bool isGrounded() {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return (raycastHit.collider != null);

    }

    private bool onWall() {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return (raycastHit.collider != null);

    }

}
