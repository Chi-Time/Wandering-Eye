using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
    private Transform _Transform = null;

    private void Awake ()
    {
        this.tag = "Brick";

        AssignReferences ();
    }

    private void AssignReferences ()
    {
        _Transform = GetComponent<Transform> ();
    }

    public bool CanBePushed (Vector3 dir)
    {
        if (CanMove (dir))
        {
            _Transform.position += dir;
            return true;
        }

        return false;
    }

    private bool CanMove (Vector3 dir)
    {
        var end = _Transform.position + dir;
        var info = new RaycastHit ();

        if (Physics.Linecast (_Transform.position, end, out info))
            if (info.collider.CompareTag ("Wall") || info.collider.CompareTag("Brick"))
                return false;

        return true;
    }
}
