using UnityEngine;

public class SizeChangePortals : MonoBehaviour
{
    
    public void SizePortal(GameObject item,GameObject entrypoint, Transform exitPoint, float offset)
    {
        if (item.GetComponentInParent<Transform>() != null) // checks for parent
        {
            item = item.GetComponentInParent<Transform>().gameObject;
        }
        if (exitPoint.localScale.x > transform.localScale.x) // checks if the exit portal is lager.
        {
            item.transform.localScale = item.transform.localScale * exitPoint.localScale.x; // makes object big
            item.GetComponent<Rigidbody>().mass *= exitPoint.localScale.x; // makes mass of object heavy
        }
        else
        {
            item.transform.localScale = item.transform.localScale / entrypoint.transform.localScale.x; // makes object small
            item.GetComponent<Rigidbody>().mass /= entrypoint.transform.localScale.x; //makes mass of object lit
        }
        float size = item.transform.localScale.x;
        item.transform.position = exitPoint.position + exitPoint.forward * (offset + size); // change position
        Vector3 rotate = exitPoint.localRotation.eulerAngles - this.transform.localRotation.eulerAngles + item.transform.rotation.eulerAngles + new Vector3(0, 180, 0);
        item.transform.rotation = Quaternion.Euler(rotate); // change rotation
    }
}
