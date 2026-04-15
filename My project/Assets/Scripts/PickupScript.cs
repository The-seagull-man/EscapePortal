using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float throwForce = 6f;
    public float holdDistance = 3;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void OnPickup() {
        rb.useGravity = false;
		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

    public virtual void OnDrop(Vector3 direction) {
		rb.useGravity = true;
		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.AddForce(direction*throwForce, ForceMode.Impulse);
	}
}
