using UnityEngine;

public class SizeChangePortals : MonoBehaviour
{
    public void SizePortal(GameObject item, Transform exitPoint, float offset)
    {
        if (item.GetComponentInParent<Transform>() != null)
        {
            item = item.GetComponentInParent<Transform>().gameObject;
        }
        if (exitPoint.localScale.x > transform.localScale.x)
        {
            item.transform.localScale = item.transform.localScale * transform.localScale.x;
        }
        else
        {
            item.transform.localScale = item.transform.localScale / transform.localScale.x;
        }
        float size = item.transform.localScale.x;
        item.transform.position = exitPoint.position + exitPoint.forward * (offset + size);
        Vector3 rotate = exitPoint.rotation.eulerAngles + item.transform.rotation.eulerAngles;
        item.transform.rotation = Quaternion.Euler(rotate);
    }
}
