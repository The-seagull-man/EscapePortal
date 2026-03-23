using UnityEditor.Experimental;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject exitPortal; // the portal its connected to
    Transform exitPoint;
    public float offset; // the distace away from the portal

    // avv maren (Liza 2026)


    private void Start()
    {
        exitPoint = exitPortal.transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject item = collision.gameObject;
        
        item.transform.position = exitPoint.position + exitPoint.forward * offset;
        if (item.GetComponentInParent<Transform>() != null)
        {
            item = item.GetComponentInParent<Transform>().gameObject;
        }
        Vector3 rotate = exitPoint.rotation.eulerAngles + item.transform.rotation.eulerAngles;
        item.transform.rotation = Quaternion.Euler(rotate);
    }
    void PortalType()
    {
        gameObject.GetComponentInParent<Transform>();

    }





}
