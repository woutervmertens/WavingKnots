﻿using UnityEngine;
using System.Collections;

/* -------------------------------------------------------------------------------
	Floater
 
	This component works with the Ocean component to make a RigidBody float.
	When this GameObject moves beneath the Ocean surface it pushes the
	referenced RigidBody towards the Ocean Normal with an acceleration
	based on the depth.
 
	Tip: Use multiple Floaters spread far apart to make a RigidBody stable.
 ------------------------------------------------------------------------------ */
public class Floater : MonoBehaviour
{

    public float LiftAcceleration = 1;
    public float MaxDistance = 3;
    public Rigidbody Body;
    private Ocean ocean;

    void Start()
    {
        ocean = (Ocean)GameObject.FindObjectOfType(typeof(Ocean));
    }

    void FixedUpdate()
    {
        Vector3 p = transform.position;
        float waterHeight = ocean.GetHeightAtLocation(p.x, p.z);
        Vector3 waterNormal = ocean.GetNormalAtLocation(p.x, p.z);
        float forceFactor = Mathf.Clamp(1f - (p.y - waterHeight) / MaxDistance, 0, 1);
        transform.parent.GetComponent<Rigidbody>().AddForceAtPosition(waterNormal * forceFactor * LiftAcceleration * Body.mass, p);

        if (!Debug.isDebugBuild)
            return;
        Debug.DrawLine(p, p + waterNormal * forceFactor * 5, Color.green);
        Debug.DrawLine(p + waterNormal * forceFactor * 5, p + waterNormal * 5, Color.red);
    }

}
