using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float throwForce = 6f;
    public float holdDistance = 3;
	public float maxDistance = 5;

	Rigidbody rb;
	float angularDamping;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
		rb = GetComponent<Rigidbody>();
		angularDamping = rb.angularDamping;
	}

	public virtual void OnPickup() {
        rb.useGravity = false;
		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.angularDamping = .5f;
	}

    public virtual void OnDrop(Vector3 direction, bool thrown = true) {
		rb.useGravity = true;
		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.angularDamping = angularDamping;
		if (thrown) {
			rb.AddForce(direction*throwForce, ForceMode.Impulse);
		}
	}
}
