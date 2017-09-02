using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
    [SerializeField] private Color _DefaultColor = Color.red;
    [SerializeField] private Color _GoalColor = Color.magenta;

    private bool _IsMoving = false;
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

    public bool CanBePushed (Vector3 dir, float speed)
    {
        if (CanMove (dir) && !_IsMoving)
        {
            StartCoroutine (MoveToPosition (_Transform.position + dir, speed));
            
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

    private void CheckIfInGoal ()
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

    private IEnumerator MoveToPosition (Vector3 newPos, float speed)
    {
        _IsMoving = true;
        var elapsedTime = 0f;
        var startingPos = _Transform.position;

        while (elapsedTime < speed)
        {
            transform.position = Vector3.Lerp (startingPos, newPos, (elapsedTime / speed));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame ();
        }

        _Transform.position = newPos;
        StopAllCoroutines ();
        _IsMoving = false;
        CheckIfInGoal ();
    }
}
