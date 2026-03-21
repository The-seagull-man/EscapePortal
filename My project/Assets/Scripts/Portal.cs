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
        item.transform.rotation = exitPoint.rotation;

    }





}
