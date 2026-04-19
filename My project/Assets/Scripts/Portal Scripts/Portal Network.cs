using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PortalNetwork : MonoBehaviour
{
    Portal[] portals; // array of portals
    Transform portalNetwork; // it's own transform
    void Awake()
    {
        // asigns the portal connections
        portalNetwork = gameObject.GetComponent<Transform>();
        portals = new Portal[portalNetwork.childCount];
        for (int i = 0; i < portalNetwork.childCount; i++)
        {
            Transform curent = gameObject.GetComponentInChildren<Transform>().GetChild(i);
            portals[i] = curent.gameObject.GetComponent<Portal>();
            Transform other = gameObject.GetComponentInChildren<Transform>().GetChild(portalNetwork.childCount - i-1); // - i - 1 is to find the other portal 
            portals[i].exitPortal = other.gameObject;
        }
        if (gameObject.GetComponent<SizeChangePortals>() != null) // checks if the portals change size of objects
        {
            portals[0].counter_value = 1;
            portals[1].counter_value = -1;

        }
    }
}
