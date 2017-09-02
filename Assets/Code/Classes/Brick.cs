using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
    private Transform _Transform = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        _Transform = GetComponent<Transform> ();
    }

    public bool IsPushed (Vector2 dir)
    {
        if (CanMove (dir))
        {
            _Transform.position += (Vector3)dir;
            return true;
        }

        return false;
    }

    private bool CanMove (Vector2 dir)
    {
        var end = (Vector2)_Transform.position + dir;
        var info = new RaycastHit ();

        if (Physics.Linecast ((Vector2)_Transform.position, end, out info))
            if (info.collider.CompareTag ("Wall") || info.collider.CompareTag("Brick"))
                return false;

        return true;
    }
}
