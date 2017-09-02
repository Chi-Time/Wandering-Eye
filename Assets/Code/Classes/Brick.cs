using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
    [SerializeField] private Color _DefaultColor = Color.red;
    [SerializeField] private Color _GoalColor = Color.magenta;

    private Renderer _Renderer = null;
    private Transform _Transform = null;

    private void Awake ()
    {
        this.tag = "Brick";

        AssignReferences ();
    }

    private void AssignReferences ()
    {
        _Renderer = GetComponent<Renderer> ();
        _Transform = GetComponent<Transform> ();
    }

    private void Start ()
    {
        _Renderer.material.color = _DefaultColor;
    }

    public bool CanBePushed (Vector3 dir)
    {
        if (CanMove (dir))
        {
            _Transform.position += dir;
            CheckForGoal ();
            return true;
        }

        return false;
    }

    private bool CanMove (Vector3 dir)
    {
        var info = new RaycastHit ();

        if (Physics.Linecast (_Transform.position, _Transform.position + dir, out info))
        {
            if (info.collider.CompareTag ("Wall") || info.collider.CompareTag ("Brick"))
                return false;
        }

        return true;
    }

    private void CheckForGoal ()
    {
        var info = new RaycastHit ();

        if (Physics.Linecast (_Transform.position, _Transform.position + Vector3.forward, out info))
        {
            if (info.collider.CompareTag ("Goal"))
                InGoal (true);
            else
                InGoal (false);
        }
    }

    private void InGoal (bool isInGoal)
    {
        //TODO: Inform level manager brick is in place.

        if(isInGoal)
        {
            _Renderer.material.color = _GoalColor;
        }
        else
        {
            _Renderer.material.color = _DefaultColor;
        }
    }
}
