using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float throwForce = 600f;
    public float holdDistance;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void OnPickup() {
        rb.isKinematic = true;
    }

    public virtual void OnDrop(Vector3 direction) {
		rb.isKinematic = false;
		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.AddForce(direction*throwForce, ForceMode.Impulse);
	}
}
