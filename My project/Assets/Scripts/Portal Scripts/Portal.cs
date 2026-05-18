using UnityEditor.Experimental;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject exitPortal; // the portal its connected to
    Transform exitPoint; // exit point for objects
    public float offset; // the distace away from the portal
    public Vector3 exitOffset;
	//public Transform cameraTransform;
	//public Transform playerTransform;

	public bool portalIsActivated = true;

	// avv maren (Liza 2026)


	private void Start() {
		exitPoint = exitPortal.transform;
	}

	/*public void Update() {
		cameraTransform.localRotation = playerTransform.rotation*Quaternion.Inverse(transform.rotation);
	}*/

	private void OnTriggerEnter(Collider collider) 
    {
        GameObject item = collider.gameObject; // object that gets teleported
        if (GetComponentInParent<SizeChangePortals>() != null) // checks if the portal needs to do something differnt
        {
            if (portalIsActivated && exitPortal.GetComponent<Portal>().portalIsActivated)
            {
                GetComponentInParent<SizeChangePortals>().SizePortal(item, gameObject, exitPoint, offset);
            }
        }
        else
        {
            if (portalIsActivated && exitPortal.GetComponent<Portal>().portalIsActivated)
            {
                PortalTypeDefult(item, exitPoint, offset);

            }
        }
    }

    void PortalTypeDefult(GameObject item, Transform exitPoint, float offset) // normal teleport
    {
        {
            Transform newItem = item.GetComponentInParent<Transform>();
            if (newItem != null) // checks for parent
            {
                item = newItem.gameObject;
            }
		}
		Quaternion teleRotation = Quaternion.Inverse(transform.rotation)*exitPoint.rotation*Quaternion.Euler(new Vector3(0, 180, 0));
		item.transform.position = exitPoint.position + exitOffset + offset*exitPoint.right; // change position
		item.transform.rotation *= teleRotation; // change rotation
        if (item.TryGetComponent(out Rigidbody rb)) {
            rb.linearVelocity = teleRotation*rb.linearVelocity; //change velocity
        }
    }
}