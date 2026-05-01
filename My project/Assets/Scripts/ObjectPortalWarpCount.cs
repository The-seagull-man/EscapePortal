using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEditor.Experimental;
using Unity.VisualScripting;

public class ObjectPortalWarpCount : MonoBehaviour
{
    public int warpLimt;
    public List<PortalCounting> portalWarps;
    PortalCounting bootless = new PortalCounting();
    
    private void Start()
    {
        portalWarps = new List<PortalCounting>();
        bootless.GameObject = gameObject;
        bootless.PortalWarpCount = 22; // this doesnt do anything 
        portalWarps.Add(bootless); // nor this. all it does is make the list not empty
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<SizeChangePortals>() != null)
        {
            GameObject parent = collision.gameObject.GetComponentInParent<Transform>().gameObject;
            foreach (PortalCounting portal in portalWarps)
            {
                if (parent == portal.GameObject)
                {
                    if (portal.PortalWarpCount + collision.gameObject.GetComponent<Portal>().counter_value > warpLimt || portal.PortalWarpCount + collision.gameObject.GetComponent<Portal>().counter_value < -warpLimt)
                    {                        
                        portal.CanWarp = false;
                        return;
                    }                   
                    portal.PortalWarpCount += collision.gameObject.GetComponent<Portal>().counter_value;
                    return;
                }
            }
            PortalCounting newPortal = new PortalCounting();
            newPortal.GameObject = parent;
            newPortal.PortalWarpCount = collision.gameObject.GetComponent<Portal>().counter_value;
            newPortal.CanWarp = true;
            portalWarps.Add(newPortal);
            Debug.Log(newPortal.ToString());
        }
    }

}
