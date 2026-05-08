using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float throwForce = 6f;
    public float holdDistance = 3;
	public float maxDistance = 5;
	public float heldAngularDamping = .5f;
	public float heldMass = .5f;

	Rigidbody rb;
	float linearDamping;
	float angularDamping;
	float mass;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
		rb = GetComponent<Rigidbody>();
		linearDamping = rb.linearDamping;
		angularDamping = rb.angularDamping;
		mass = rb.mass;
	}

	public virtual void OnPickup() {
        rb.useGravity = false;
		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.linearDamping = 0;
		rb.angularDamping = heldAngularDamping;
		rb.mass = heldMass;
	}

    public virtual void OnDrop(Vector3 direction, bool thrown = true) {
		rb.useGravity = true;
		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.linearDamping = linearDamping;
		rb.angularDamping = angularDamping;
		rb.mass = mass;
		if (thrown) {
			rb.AddForce(direction*throwForce, ForceMode.Impulse);
		}
	}
}
