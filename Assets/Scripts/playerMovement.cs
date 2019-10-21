using System.Collections;
using System;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    enum dir { up, down, left, right};
    private dir facing = dir.right;

    public Sprite[] tyoUkot;

    float timeAfterFirstMove = 0.15f;
    float timeBetweenMoves = 0.05f;

    float timeSinceLastClick = 0;
    float timeSinceLastMove = 0;

    public Boss boss;
    private SpriteRenderer SprRenderer;
    // Use this for initialization
    void Start () {

        SprRenderer = GetComponentInChildren<SpriteRenderer>();
        boss = GameObject.FindObjectOfType<Boss>();

    }
    

    private void move(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + direction*0.55f, direction, 0.5f);

        if (hit.collider == null)
        {
            transform.Translate(direction);
        }

        else if (hit.collider.tag == "pushable" || hit.collider.tag == "pushAndHide")
        {

            // Check if pushable can move
            Transform pushable = hit.collider.transform;

            if ((pushable.position + direction - boss.transform.position).magnitude < 4)
            {
                return;
            }

            RaycastHit2D pushableHit = Physics2D.Raycast(pushable.position + direction*0.55f, direction, 0.5f);

            if (pushableHit.collider == null)
            {
                transform.Translate(direction);
                hit.transform.Translate(direction);
                
            } else if (pushableHit.collider.tag == "goal")
            {
                transform.Translate(direction);
                hit.transform.Translate(direction);
            }

        } else if (hit.collider.tag == "consumable")
        {
            transform.Translate(direction);
        }

        timeSinceLastMove = Time.time;
        
    }

    private void Update()
    {
        // Direction, where player should move
        Vector3 direction = Vector3.zero;
        

        // Set direction and sprite according to input
        if (Input.GetKeyDown("w"))
        {
            SprRenderer.sprite = tyoUkot[3];
            facing = dir.up;
            direction = Vector3.up;
            timeSinceLastClick = Time.time;

        }
        if (Input.GetKeyDown("a"))
        {
            SprRenderer.sprite = tyoUkot[2];
            facing = dir.left;
            direction = Vector3.left;
            timeSinceLastClick = Time.time;
        }
        if (Input.GetKeyDown("s"))
        {
            SprRenderer.sprite = tyoUkot[1];
            facing = dir.down;
            direction = (Vector3.down);
            timeSinceLastClick = Time.time;
        }
        if (Input.GetKeyDown("d"))
        {
            SprRenderer.sprite = tyoUkot[0];
            facing = dir.right;
            direction = (Vector3.right);
            timeSinceLastClick = Time.time;
        }

        // Move player
        if (direction.magnitude > 0)
            move(direction);


        // If player is holding key down and facing right direction, move by holding key down
        if (Time.time - timeSinceLastClick > timeAfterFirstMove && Time.time - timeSinceLastMove > timeBetweenMoves)
        {
            if (Input.GetKey("w") && facing == dir.up)
            {
                move(Vector3.up);
            }
            if (Input.GetKey("a") && facing == dir.left)
            {
                move(Vector3.left);
            }
            if (Input.GetKey("s") && facing == dir.down)
            {
                move(Vector3.down);
            }
            if (Input.GetKey("d") && facing == dir.right)
            {
                move(Vector3.right);
            }
            
        }


    }




}
