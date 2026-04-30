using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEditor.Experimental;
using Unity.VisualScripting;

public class ObjectPortalWarpCount : MonoBehaviour
{
    public int warpLimt;
    List<PortalCounting> portalWarps;
    PortalCounting bootless = new PortalCounting();

    private void Start()
    {
        portalWarps = new List<PortalCounting>();
        bootless.GameObject = gameObject;
        bootless.PortalWarpCount = 22;
        portalWarps.Add(bootless);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<SizeChangePortals>() != null)
        {
            GameObject parent = collision.gameObject;
            foreach (PortalCounting portal in portalWarps)
            {
                if (parent == portal.GameObject)
                {
                    if (portal.PortalWarpCount + collision.gameObject.GetComponent<Portal>().counter_value > warpLimt)
                    {
                        gameObject.transform.position = collision.transform.forward;
                    }
                    portal.PortalWarpCount += collision.gameObject.GetComponent<Portal>().counter_value;
                    return;
                }
            }
            PortalCounting newPortal = new PortalCounting();
            newPortal.GameObject = parent;
            newPortal.PortalWarpCount = collision.gameObject.GetComponent<Portal>().counter_value;
            portalWarps.Add(newPortal);
            Debug.Log(newPortal.ToString());
        }
    }

}
