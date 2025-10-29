using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Audio_handler aHandler; // handler of audio
    private new Rigidbody2D rigidbody; // Our rididbody.
    private new Camera camera; // A reference to the camera.
    private Vector2 velocity; // A 2D vector representing our velocity.
    private float inputAxis; // A float representing which way we are moving.

    private Animator Animator;

    public PauseMenu pauseMenu;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        aHandler = GetComponent<Audio_handler>();
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    void Start()
    {
        
    }

    void Update() 
    {

        //Debug.Log(grounded);
        HorizontalMovement();
        grounded = rigidbody.Raycast(Vector2.down) && velocity.y < 0;

        if (grounded)
        {
            GroundedMovement(); 
        }

        ApplyGravity();

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            pauseMenu.Pause();
        }
        Animator.SetBool("Jumping", jumping);
        Animator.SetBool("Running", running);
        Animator.SetBool("Sliding", sliding);
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }

        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f); // Reduces gravity
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
            aHandler.Playclip(aHandler.jump);
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0 || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        velocity.y += (gravity * multiplier * Time.deltaTime);
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position; // What is our current position?
        position += velocity * Time.fixedDeltaTime; // Add two vectors to get our new vector, then move our rigidbody

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero); // The left most edge
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); // The right most edge
        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x); // This ensures position.x is always within the bounds of leftEdge and rightEdge

        rigidbody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp") && !collision.gameObject.CompareTag("Coin"))
        {            
            velocity.y = 0f;
            if (transform.DotProductTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }

        if (collision.gameObject.CompareTag("Trampoline"))
        {
            grounded = false;
            velocity.y = jumpForce * 1.5f;
            jumping = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger: " + other.tag);
    }
}
