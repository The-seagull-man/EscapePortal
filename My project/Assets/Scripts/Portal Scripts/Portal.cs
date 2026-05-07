using UnityEditor.Experimental;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject exitPortal; // the portal its connected to
    Transform exitPoint; // exit point for objects
    public float offset; // the distace away from the portal
    public int counter_value;
    public bool portalIsActivated = true;

    // avv maren (Liza 2026)


    private void Start()
    {
        exitPoint = exitPortal.transform;
    }

    private void OnCollisionEnter(Collision collision) 
    {
        GameObject item = collision.gameObject; // object that gets teleported
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
        if (item.GetComponentInParent<Transform>() != null) // checks for parent
        {
            item = item.GetComponentInParent<Transform>().gameObject;
        }
        item.transform.position = exitPoint.position + exitPoint.forward * offset; // change position 
        Vector3 rotate = exitPoint.localRotation.eulerAngles - this.transform.localRotation.eulerAngles + item.transform.rotation.eulerAngles + new Vector3(0,180,0);
        item.transform.rotation = Quaternion.Euler(rotate); // change rotation
    }




}
