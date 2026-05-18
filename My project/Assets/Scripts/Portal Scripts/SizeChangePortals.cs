using UnityEngine;
using System.Collections.Generic;

public class SizeChangePortals : MonoBehaviour
{

    

    public void SizePortal(GameObject item,GameObject entryPoint, Transform exitPoint, float offset)
    {
        if (item.GetComponentInParent<Transform>() != null) // checks for parent
        {
            item = item.GetComponentInParent<Transform>().gameObject;
        }
        if (item.TryGetComponent(out ObjectPortalWarpCount warpCount))  
        {
            if (exitPoint.localScale.x > transform.localScale.x) // checks if the exit portal is lager.
            {
                float scaleX = Mathf.Clamp(item.transform.localScale.x * exitPoint.localScale.x, warpCount.MinSizeX, warpCount.MaxSizeX);
                float scaleY = Mathf.Clamp(item.transform.localScale.y * exitPoint.localScale.y, warpCount.MinSizeY, warpCount.MaxSizeY);
                float scaleZ = Mathf.Clamp(item.transform.localScale.z * exitPoint.localScale.z, warpCount.MinSizeZ, warpCount.MaxSizeZ);

                item.transform.localScale = new Vector3(scaleX, scaleY, scaleZ); // makes object big
                item.GetComponent<Rigidbody>().mass *= entryPoint.transform.localScale.x; // makes mass of object heavy
            }
            else
            {
                float scaleX = Mathf.Clamp(item.transform.localScale.x / entryPoint.transform.localScale.x, warpCount.MinSizeX, warpCount.MaxSizeX);
                float scaleY = Mathf.Clamp(item.transform.localScale.y / entryPoint.transform.localScale.y, warpCount.MinSizeY, warpCount.MaxSizeY);
                float scaleZ = Mathf.Clamp(item.transform.localScale.z / entryPoint.transform.localScale.z, warpCount.MinSizeZ, warpCount.MaxSizeZ);

                item.transform.localScale = new Vector3(scaleX, scaleY, scaleZ); // makes object small
                item.GetComponent<Rigidbody>().mass /= entryPoint.transform.localScale.x; //makes mass of object lit
            }
        }
        
        float size = item.transform.localScale.x;
        item.transform.position = exitPoint.position + exitPoint.right * -1 * (offset + size); // change position
        Vector3 rotate = exitPoint.localRotation.eulerAngles - this.transform.localRotation.eulerAngles + item.transform.rotation.eulerAngles + new Vector3(0, 90, 0);
        item.transform.rotation = Quaternion.Euler(rotate); // change rotation
    }
}
