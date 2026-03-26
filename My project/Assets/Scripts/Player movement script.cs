using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Playermovementscript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;

    public float groundDrag;
	public float airDrag;

	public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    [Header("Keybinds")]
    public KeyCode JumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float minGroundSine;

    float horizontalInput;
    float verticalInput;
    List<Collider> grounds;
	bool grounded;
	bool readyToJump;

	public Transform cameraYtransform;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        grounds = new List<Collider>();
    }

    private void FixedUpdate() {
		MyInput();

		// handle drag
		if (grounded) {
            rb.linearDamping = groundDrag;
        } else {
            rb.linearDamping = airDrag;
        }
        MovePlayer();
		SpeedControl();
	}

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if(Input.GetKey(JumpKey)&& readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump),jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //calculate movement direction
        Vector3 moveDirection = (cameraYtransform.forward * verticalInput + cameraYtransform.right * horizontalInput).normalized;
        if (grounded) {
            rb.AddForce(moveDirection * acceleration, ForceMode.Force);
        } else if (!grounded) {
            rb.AddForce(moveDirection * acceleration * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x,0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

	public void OnCollisionStay(Collision collision) {
        bool isGround = false;
        ContactPoint[] contacts = new ContactPoint[collision.contactCount];
        collision.GetContacts(contacts);
        //Check through each contact point, and verify whether there are any that aren't too steep.
		foreach (ContactPoint c in contacts) {
            if (c.normal.y >= minGroundSine) {
                isGround = true;
                break;
            }
        }

        //Add or remove the collider from the grounds list depending on whether the collision is too steep.
        if (isGround) {
            if (!grounds.Contains(collision.collider)) {
                grounds.Add(collision.collider);
                grounded = true;
            }
        } else {
            if (grounds.Remove(collision.collider) && grounds.Count == 0) {
                grounded = false;
            }
        }
	}

	public void OnCollisionExit(Collision collision) {
        //Remove the exited collider from the ground list, and mark the player ungrounded if it was the only ground.
		if (grounds.Remove(collision.collider) && grounds.Count == 0) {
            grounded = false;
        }
	}
}
