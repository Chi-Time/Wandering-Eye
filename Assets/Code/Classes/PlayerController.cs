using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _Speed = .075f;
    [SerializeField] private bool _UseJoystick = false;

    private bool _StickMoved = false;
    private bool _IsMoving = false;
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
        GetKeybaordInput ();
        GetJoyStickInput ();
    }

    private void GetKeybaordInput ()
    {
        if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            Move (Vector2.left);
        else if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow))
            Move (Vector2.right);
        else if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow))
            Move (Vector2.down);
        else if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow))
            Move (Vector2.up);
    }

    private void GetJoyStickInput ()
    {
        if (Input.GetAxis ("Horizontal") > 0.0f && !_StickMoved)
        {
            Move (Vector2.right);
            _StickMoved = true;
        }
        else if (Input.GetAxis ("Horizontal") < 0.0f && !_StickMoved)
        {
            Move (Vector2.left);
            _StickMoved = true;
        }
        else if (Input.GetAxis ("Vertical") > 0.0f && !_StickMoved)
        {
            Move (Vector2.up);
            _StickMoved = true;
        }
        else if (Input.GetAxis ("Vertical") < 0.0f && !_StickMoved)
        {
            Move (Vector2.down);
            _StickMoved = true;
        }
        else if (Input.GetAxis ("Horizontal") == 0.0f && Input.GetAxis ("Vertical") == 0.0f)
            _StickMoved = false;
    }

    private void Move (Vector3 dir)
    {
        if (SpaceIsEmpty (dir))
            StartCoroutine (MoveToPosition (_Transform.position + dir, _Speed));
    }

    private IEnumerator MoveToPosition (Vector3 newPos, float speed)
    {
        EventManager.PlayerMoved (1, false);
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
    }

    private bool SpaceIsEmpty (Vector3 dir)
    {
        var end = _Transform.position + dir;
        var info = new RaycastHit ();

        if (Physics.Linecast (_Transform.position, end, out info))
        {
            // There's a wall, we can't move here.
            if (info.collider.CompareTag ("Wall"))
                return false;
            // There's a brick, let's check if we can push it.
            else if (info.collider.CompareTag ("Brick"))
                return CanPushBrick (dir, info);
        }

        return true;
    }

    private bool CanPushBrick (Vector3 dir, RaycastHit info)
    {
        // Grab the brick and see if it can be pushed.
        var brick = info.collider.gameObject.GetComponent<Brick> ();

        if (brick.CanBePushed (dir, _Speed))
            return true;

        return false;
    }
}
