using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public float maxPickupDistance;

    public float pickupDirectSpeed;
    public float pickupCircularSpeed;
	public float pickupOutSpeed;
	//How many frames between moving a picked up object in a circular motion to save on computation. 1 means every frame. Multiplies pickupCircularSpeed to compensate.
	public int pickupCircularMotionDelay;
	public float pickupFriction;

	int heldObjectCircularMotionFrame;

	public Transform cameraYtransform;
    public Transform cameraTransform;

	float horizontalInput;
	float verticalInput;
	List<Collider> grounds;
	bool grounded;
	bool readyToJump;
    bool leftMouseInput;
    
    public GameObject youwintext;

	Rigidbody rb;

#nullable enable
    PickupScript? heldObject;
	Rigidbody? heldObjectRb;
#nullable disable

	private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        grounds = new List<Collider>();
        youwintext.SetActive(false);
    }

	public void Update() {
		if (Input.GetMouseButtonDown((int) MouseButton.Left)) {
            leftMouseInput = true;
		}
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
        HandleHeldObject();
        
	}

    private void HandleHeldObject()
    {
        if (heldObject != null)
        {
            Vector3 heldGoal = cameraTransform.position + cameraTransform.forward * heldObject.holdDistance;
            Vector3 directMotion = heldGoal - heldObject.transform.position;

            Vector3 relativeHeld = heldObject.transform.position - cameraTransform.position;
            Vector3 relativeHeldNormalized = relativeHeld.normalized;

            if (directMotion.x != 0 || directMotion.y != 0 || directMotion.z != 0)
            { //If the picked up object is already at the correct position, don't bother moving it.
                Vector3 circleMotion = Vector3.zero;
                if (heldObjectCircularMotionFrame == 0 && pickupCircularSpeed != 0)
                {
                    Vector3 relativeGoal = heldGoal - cameraTransform.position;

                    Vector3 heldXZ = new(relativeHeld.x, 0, relativeHeld.z);
                    Vector3 heldXZNormalized;
                    float heldXZMagnitude = heldXZ.magnitude;
                    if (heldXZMagnitude == 0)
                    {
                        heldXZNormalized = new Vector3(relativeGoal.x, 0, relativeGoal.z).normalized;
                    }
                    else
                    {
                        heldXZNormalized = heldXZ / heldXZMagnitude;
                    }

                    float XZAngleDelta = Mathf.DeltaAngle(Mathf.Atan2(relativeHeld.z, relativeHeld.x) * Mathf.Rad2Deg, Mathf.Atan2(relativeGoal.z, relativeGoal.x) * Mathf.Rad2Deg);

                    Vector3 XZMotion = 2 * Mathf.PI * XZAngleDelta / 360f *
                            new Vector3(-relativeHeld.z, 0, relativeHeld.x);
                    Vector3 YMotion = (Mathf.Asin(relativeHeldNormalized.y) - Mathf.Asin(relativeGoal.normalized.y)) *
                            new Vector3(heldXZNormalized.x * relativeHeld.y, -heldXZMagnitude, heldXZNormalized.z * relativeHeld.y);

                    //Debug.Log(XZMotion + " " + YMotion);
                    //Debug.Log(XZAngleDelta + " " + -(Mathf.Asin(relativeHeldNormalized.y) - Mathf.Asin(relativeGoal.normalized.y)));

                    circleMotion = XZMotion + YMotion;
                }
                Vector3 outMotion = relativeHeldNormalized * heldObject.holdDistance - relativeHeld;
                heldObjectRb.AddForce(-heldObjectRb.linearVelocity * pickupFriction, ForceMode.Acceleration);
                heldObjectRb.AddForce(pickupDirectSpeed * directMotion + pickupCircularMotionDelay * pickupCircularSpeed * circleMotion + pickupOutSpeed * outMotion, ForceMode.Acceleration);
            }
            heldObjectCircularMotionFrame++;
            if (heldObjectCircularMotionFrame == pickupCircularMotionDelay)
            {
                heldObjectCircularMotionFrame = 0;
            }
        }
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
        if (leftMouseInput) {
            leftMouseInput = false;
            HandlePickup();
            Door();
        }
	}

	public void HandlePickup() {
		if (heldObject == null) {
			if (Physics.Raycast(cameraTransform.position, cameraTransform.transform.forward, out RaycastHit hit, maxPickupDistance)) {
				if (hit.collider.gameObject.TryGetComponent<PickupScript>(out PickupScript pickup)) {
					heldObject = pickup;
                    heldObjectRb = pickup.GetComponent<Rigidbody>();
					pickup.OnPickup();
                    heldObjectCircularMotionFrame = 0;
				}
			}
		} else {
			heldObject.OnDrop(cameraTransform.transform.forward);
			heldObject = null;
            heldObjectRb = null;
		}
	}
    public void Door()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.transform.forward, out RaycastHit hit, maxPickupDistance)) 
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Door"))
            {
                Debug.Log("Win");
                youwintext.SetActive(true);
            }
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
