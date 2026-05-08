using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEditor.Experimental;
using Unity.VisualScripting;
using System;

public class ObjectPortalWarpCount : MonoBehaviour
{
    [SerializeField]
    private int max = 5;
    public Vector3 startSize;
    public float MaxSizeX { get { return startSize.x * max; } }
    public float MinSizeX { get { return startSize.x / max; } }
    public float MaxSizeY { get { return startSize.y * max; } }
    public float MinSizeY { get { return startSize.y / max; } }
    public float MaxSizeZ { get { return startSize.z * max; } }
    public float MinSizeZ { get { return startSize.z / max; } }

    private void Awake()
    {
        startSize = transform.localScale;
    }

    
}
