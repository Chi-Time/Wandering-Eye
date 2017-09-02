using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Vector3 _Direction = Vector3.zero;
    private Transform _Transform = null;

    private void Awake ()
    {
        this.tag = "Player";

        AssignReferences ();
    }

    private void AssignReferences ()
    {
        _Transform = GetComponent<Transform> ();
    }

    private void Update ()
    {
        GetInput ();
    }

    private void GetInput ()
    {
        if (Input.GetKeyDown (KeyCode.A))
            Move (Vector2.left);
        else if (Input.GetKeyDown (KeyCode.D))
            Move (Vector2.right);
        else if (Input.GetKeyDown (KeyCode.S))
            Move (Vector2.down);
        else if (Input.GetKeyDown (KeyCode.W))
            Move (Vector2.up);
    }

    private void Move (Vector3 dir)
    {
        if (CanMove (dir))
            _Transform.position += dir;
    }

    private bool CanMove (Vector3 dir)
    {
        // We don't need the Z axis.
        var end = (Vector2)_Transform.position + (Vector2)dir;
        var info = new RaycastHit ();

        if (Physics.Linecast ((Vector2)_Transform.position, end, out info))
            if (info.collider.CompareTag ("Finish"))
                return false;

        return true;
    }
}
