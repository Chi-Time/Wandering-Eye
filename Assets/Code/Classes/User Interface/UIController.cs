using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject[] _Screens = null;

    private void Start ()
    {
        AssignReferences ();

        EventManager.OnStateChanged += StateChanged;

        EventManager.ChangeState (GameStates.InGame);
    }

    private void AssignReferences ()
    {
        _Screens = new GameObject[transform.childCount];

        for (int i = 0; i < _Screens.Length; i++)
        {
            var go = transform.GetChild (i).gameObject;
            _Screens[i] = go;
        }
    }

    private void StateChanged (GameStates state)
    {
        switch (state)
        {
            case GameStates.Start:
                SwitchScreen (0);
                break;
            case GameStates.InGame:
                SwitchScreen (1);
                break;
            case GameStates.LevelComplete:
                SwitchScreen (2);
                break;
            case GameStates.InGameOptions:
                SwitchScreen (3);
                break;
            case GameStates.Fade:
                SwitchScreen (4);
                break;
        }
    }

    private void SwitchScreen (int screenNumber)
    {
        for (int i = 0; i < _Screens.Length; i++)
        {
            if (i == screenNumber)
                _Screens[i].SetActive (true);
            else if (i != _Screens.Length - 1)
                _Screens[i].SetActive (false);
        }
    }

    private void OnDestroy ()
    {
        EventManager.OnStateChanged -= StateChanged;
    }
}
