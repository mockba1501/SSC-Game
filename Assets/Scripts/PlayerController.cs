using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerState curState;            // current player state

    // values
    public float moveSpeed;                 // force applied horizontally when moving
    public float flyingSpeed;               // force applied upwards when flying
    public bool grounded;                   // is the player currently standing on the ground?
    public float stunDuration;              // duration of a stun
    private float stunStartTime;            // time that the player was stunned

    // components
    public Rigidbody2D rig;                 // Rigidbody2D component
    public Animator anim;                   // Animator component
    public ParticleSystem jetpackParticle;  // ParticleSystem of jetpack

    void FixedUpdate ()
    {
        grounded = IsGrounded();
        CheckInputs();

        // is the player stunned?
        if(curState == PlayerState.Stunned)
        {
            // has the player been stunned for the duration?
            if(Time.time - stunStartTime >= stunDuration)
            {
                curState = PlayerState.Idle;
            }
        }
    }

    // checks for user input to control player
    void CheckInputs ()
    {
        if (curState != PlayerState.Stunned)
        {
            // movement
            Move();

            // flying
            if (Input.GetKey(KeyCode.UpArrow))
                Fly();
            else
                jetpackParticle.Stop();
        }

        // update our current state
        SetState();
    }

    // sets the player's state
    void SetState ()
    {
        // don't worry about changing states if the player's stunned
        if (curState != PlayerState.Stunned)
        {
            // idle
            if (rig.velocity.magnitude == 0 && grounded)
                curState = PlayerState.Idle;
            // walking
            if (rig.velocity.x != 0 && grounded)
                curState = PlayerState.Walking;
            // flying
            if (rig.velocity.magnitude != 0 && !grounded)
                curState = PlayerState.Flying;
        }

        // tell the animator we've changed states
        anim.SetInteger("State", (int)curState);
    }

    // moves the player horizontally
    void Move ()
    {
        // get horizontal axis (A & D, Left Arrow & Right Arrow)
        float dir = Input.GetAxis("Horizontal");

        // flip player to face the direction they're moving
        if (dir > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (dir < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // set rigidbody horizontal velocity
        rig.velocity = new Vector2(dir * moveSpeed, rig.velocity.y);
    }

    // adds force upwards to player
    void Fly ()
    {
        // add force upwards
        rig.AddForce(Vector2.up * flyingSpeed, ForceMode2D.Impulse);

        // play jetpack particle effect
        if (!jetpackParticle.isPlaying)
            jetpackParticle.Play();
    }

    // called when the player gets stunned
    public void Stun ()
    {
        curState = PlayerState.Stunned;
        rig.velocity = Vector2.down * 3;
        stunStartTime = Time.time;
        jetpackParticle.Stop();
    }

    // returns true if player is on ground, false otherwise
    bool IsGrounded ()
    {
        // shoot a raycast down underneath the player
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.85f), Vector2.down, 0.3f);

        // did we hit anything?
        if(hit.collider != null)
        {
            // was it the floor?
            if(hit.collider.CompareTag("Floor"))
            {
                return true;
            }
        }

        return false;
    }

    // called when the player enters another object's collider
    void OnTriggerEnter2D (Collider2D col)
    {
        // if the player isn't already stunned, stun them if the object was an obstacle
        if(curState != PlayerState.Stunned)
        {
            if(col.CompareTag("Obstacle"))
            {
                Stun();
            }
        }
    }
}

public enum PlayerState
{
    Idle,       // 0
    Walking,    // 1
    Flying,     // 2
    Stunned     // 3
}