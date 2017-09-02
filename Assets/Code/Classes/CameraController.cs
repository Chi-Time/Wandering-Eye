using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    /// <summary>The target object to follow. </summary>
    [Tooltip ("The target object to follow.")]
    public Transform Target;
    /// <summary>The distance to keep from the target.</summary>
    [Tooltip ("The distance to keep from the target.")]
    public float Distance = 3.0f;
    /// <summary>How high we are from the target.</summary>
    [Tooltip ("How high we are from the target.")]
    public float Height = 3.0f;
    /// <summary>How fast we rotate to keep focus.</summary>
    [Tooltip ("How fast we rotate to keep focus.")]
    public float Damping = 5.0f;
    /// <summary>Are we using smooth rotation?</summary>
    [Tooltip ("Are we using smooth rotation?")]
    public bool SmoothRotation = true;
    /// <summary>Should we follow behind the target.</summary>
    [Tooltip ("Should we follow behind the target.")]
    public bool FollowBehind = true;
    /// <summary>How fast we rotate.</summary>
    [Tooltip ("How fast we rotate.")]
    public float RotationDamping = 10.0f;
    [Tooltip ("Should the component look for a player object?")]
    public bool FindPlayer = true;

    void Start ()
    {
        if (FindPlayer)
            Target = GameObject.FindGameObjectWithTag ("Player").transform;
    }

    void LateUpdate ()
    {
        if (Target != null)
        {
            Follow ();
            RotateCamera ();
        }
        else
        {
            if (FindPlayer)
                Target = GameObject.FindGameObjectWithTag ("Player").transform;
        }
    }

    private void Follow ()
    {
        Vector3 wantedPosition;

        if (FollowBehind)
            wantedPosition = Target.TransformPoint (0, Height, -Distance);
        else
            wantedPosition = Target.TransformPoint (0, Height, Distance);

        transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * Damping);
    }

    private void RotateCamera ()
    {
        if (SmoothRotation)
        {
            Quaternion wantedRotation = Quaternion.LookRotation (Target.position - transform.position, Target.up);

            transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * RotationDamping);
        }
        else
        {
            transform.LookAt (Target, Target.up);
        }
    }
}
