using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
    [SerializeField] private Color _DefaultColor = Color.red;
    [SerializeField] private Color _GoalColor = Color.magenta;

    private bool _IsMoving = false;
    private bool _WasInGoal = false;
    private Vector3 _CurrentPosition = Vector3.zero;
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
            EventManager.PushBrick (1, false);
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
        if(isInGoal && _WasInGoal == false)
        {
            _Renderer.material.color = _GoalColor;
            EventManager.UpdateGoalAmount (-1);
            _WasInGoal = true;
        }
        else if(!isInGoal && _WasInGoal == true)
        {
            _Renderer.material.color = _DefaultColor;
            EventManager.UpdateGoalAmount (1);
            _WasInGoal = false;
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
        _CurrentPosition = newPos;
        StopAllCoroutines ();
        _IsMoving = false;
    }

    private void Update ()
    {
        // Hacky way to check if in goal.
        if(_Transform.position != _CurrentPosition)
            CheckIfInGoal ();
    }
}
