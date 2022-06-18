using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public int shape;
    private int previousShape;

    public float acceleration = 2.5f;
    public float angularAcceleration = 1f;
    public float jumpStrength = 3500;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shape = 0;  // circle
        previousShape = 0;
        spriteRenderer.sprite = sprites[shape];
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = true;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {   
            if (previousShape == 0)
            {
                shape = 1;
            }
        } else if (Input.GetKeyDown("s"))
        {
            if (previousShape == 1)
            {
                shape = 0;
            }
        }

        if (shape != previousShape)
        {
            // only do these when we've actually changed shape
            spriteRenderer.sprite = sprites[shape];
            // TODO: this could be done better... maybe with a List of the colliders?
            // turn both colliders off before switching so no weirdness occurs.
            circleCollider.enabled = false;
            boxCollider.enabled = false;
            circleCollider.enabled = shape == 0;
            boxCollider.enabled = shape == 1;
        }

        previousShape = shape;

        switch (shape)
        {
            case 0:
                ApplyCircleMovement();
                break;
            case 1:
                ApplySquareMovement();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.tag == "Wall") && (shape == 1))
        {
            rigidBody.angularVelocity = 0f;  // cancel all rotation when we hit a wall as a square.
        }
    }

    void ApplyCircleMovement()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        if (xInput != 0f)
        {
            rigidBody.AddForce(new Vector2(xInput * acceleration, 0));
            rigidBody.AddTorque(-(xInput) * angularAcceleration);  // input axis inverted to apply correct rolling direction.
        }
    }

    void ApplySquareMovement()
    {
        if (Input.GetKeyDown("space"))
        {
            rigidBody.AddForce(Vector2.up * jumpStrength);
        }
    }
}
