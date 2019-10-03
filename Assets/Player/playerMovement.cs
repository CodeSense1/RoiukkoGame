using System.Collections;
using System;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    enum dir { up, down, left, right};
    private dir direction = dir.right;
    private float tileSize = 100;

    public Sprite[] tyoUkot;
    public float maxVel;
    Rigidbody2D rb;
    private SpriteRenderer renderer;
    // Use this for initialization
    void Start () {

        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void rotate(ref dir facing)
    {
        // Rotates player
        if (Input.GetKeyDown("w") && facing != dir.up)
        {
            renderer.sprite = tyoUkot[3];
            facing = dir.up;
        }
        if (Input.GetKeyDown("a") && facing != dir.left)
        {
            renderer.sprite = tyoUkot[2];
            facing = dir.left;
        }
        if (Input.GetKeyDown("s") && facing != dir.down)
        {
            renderer.sprite = tyoUkot[1];
            facing = dir.down;
        }
        if (Input.GetKeyDown("d") && facing != dir.right)
        {
            renderer.sprite = tyoUkot[0];
            facing = dir.right;
        }
    }

    void constrainMovement(ref Vector2 vel)
    {
        if (vel.x > maxVel)
        {
            vel = new Vector2(maxVel, vel.y);
        } else if (vel.x < -maxVel)
        {
            vel = new Vector2(-maxVel, vel.y);
        }
        if (vel.y > maxVel)
        {
            vel = new Vector2(vel.x, maxVel);
        }
        else if (vel.y < -maxVel)
        {
            vel = new Vector2(vel.x, -maxVel);
        }

    }

    private void move(ref dir facing)
    {
        // Moves player, if facing correct direction
        if (Input.GetKey("w"))
        {
            renderer.sprite = tyoUkot[3];
            facing = dir.up;
            rb.velocity = (new Vector3(rb.velocity.x, tileSize, 0));

        }
        if (Input.GetKey("a"))
        {
            renderer.sprite = tyoUkot[2];
            facing = dir.left;
            rb.velocity = (new Vector3(-tileSize, rb.velocity.y, 0));
        }
        if (Input.GetKey("s"))
        {
            renderer.sprite = tyoUkot[1];
            facing = dir.down;
            rb.velocity = (new Vector3(rb.velocity.x, -tileSize, 0));
        }
        if (Input.GetKey("d"))
        {
            renderer.sprite = tyoUkot[0];
            facing = dir.right;
            rb.velocity = (new Vector3(tileSize, rb.velocity.y, 0));

        }
    }

    private void Update()
    {
        move(ref direction);
        //rotate(ref direction);

        Vector2 vel = rb.velocity;
        constrainMovement(ref vel);
        rb.velocity = vel;
    }




}
