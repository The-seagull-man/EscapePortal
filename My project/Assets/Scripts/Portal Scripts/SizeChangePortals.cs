using UnityEngine;
using System.Collections.Generic;

public class SizeChangePortals : MonoBehaviour
{
    [SerializeField]
    List<PortalCounting> portalWarps;

    PortalCounting bootless = new PortalCounting();
    

    private void Start()
    {
        portalWarps = new List<PortalCounting>();
        bootless.GameObject = gameObject;
        bootless.PortalWarpCount = 22; // this doesnt do anything 
        portalWarps.Add(bootless); // nor this. all it does is make the list not empty, witch we need.
    }

    public void SizePortal(GameObject item,GameObject entrypoint, Transform exitPoint, float offset)
    {
        if (item.GetComponentInParent<Transform>() != null) // checks for parent
        {
            item = item.GetComponentInParent<Transform>().gameObject;
        }
        if (item.GetComponent<ObjectPortalWarpCount>() != null)  
        {
            bool isFound = false;
            foreach (PortalCounting portalItemWarps in portalWarps)
            {
                if (item == portalItemWarps.GameObject)
                {
                    isFound = true;
                    if (portalItemWarps.PortalWarpCount + entrypoint.GetComponent<Portal>().counter_value > item.GetComponent<ObjectPortalWarpCount>().warpLimt || portalItemWarps.PortalWarpCount + entrypoint.GetComponent<Portal>().counter_value < -item.GetComponent<ObjectPortalWarpCount>().warpLimt)
                    {
                        return;
                    } 
                    portalItemWarps.PortalWarpCount += item.GetComponent<Portal>().counter_value;                    
                }
            }
            if (!isFound)
            {
                PortalCounting newPortal = new PortalCounting();
                newPortal.GameObject = item;
                newPortal.PortalWarpCount = entrypoint.gameObject.GetComponent<Portal>().counter_value;
                newPortal.CanWarp = true;
                portalWarps.Add(newPortal);
            }
        }
        if (exitPoint.localScale.x > transform.localScale.x) // checks if the exit portal is lager.
        {
            float scaleX = Mathf.Clamp(item.transform.localScale.x * exitPoint.localScale.x, item.GetComponent<ObjectPortalWarpCount>().MinSize, item.GetComponent<ObjectPortalWarpCount>().MaxSize);
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
