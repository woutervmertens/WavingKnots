using UnityEngine;
using System.Collections;

/* -------------------------------------------------------------------------------
	Floater
 
	This component works with the Oceanv1 component to make a RigidBody float.
	When this GameObject moves beneath the Oceanv1 surface it pushes the
	referenced RigidBody towards the Oceanv1 Normal with an acceleration
	based on the depth.
 
	Tip: Use multiple Floaters spread far apart to make a RigidBody stable.
 ------------------------------------------------------------------------------ */
public class Floater : MonoBehaviour
{

    public float LiftAcceleration = 1;
    public float MaxDistance = 3;
    public Rigidbody Body;
    private Oceanv1 Oceanv1;

    void Start()
    {
        Oceanv1 = (Oceanv1)GameObject.FindObjectOfType(typeof(Oceanv1));
    }

    void FixedUpdate()
    {
        Vector3 p = transform.position;
        float waterHeight = Oceanv1.GetHeightAtLocation(p.x, p.z);
        Vector3 waterNormal = Oceanv1.GetNormalAtLocation(p.x, p.z);
        float forceFactor = Mathf.Clamp(1f - (p.y - waterHeight) / MaxDistance, 0, 1);
        transform.parent.GetComponent<Rigidbody>().AddForceAtPosition(waterNormal * forceFactor * LiftAcceleration * Body.mass, p);

        if (!Debug.isDebugBuild)
            return;
        Debug.DrawLine(p, p + waterNormal * forceFactor * 5, Color.green);
        Debug.DrawLine(p + waterNormal * forceFactor * 5, p + waterNormal * 5, Color.red);
    }

}
