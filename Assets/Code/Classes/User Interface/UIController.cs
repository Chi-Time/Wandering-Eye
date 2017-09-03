using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject[] _Screens = null;

    private void Awake ()
    {
        AssignReferences ();

        EventManager.OnStateChanged += StateChanged;

        EventManager.ChangeState (GameStates.InGame);
    }

    private void AssignReferences ()
    {
        _Screens = new GameObject[transform.childCount];

        for (int i = 0; i < _Screens.Length; i++)
            _Screens[i] = transform.GetChild (i).gameObject;
    }

    private void StateChanged (GameStates state)
    {
        switch (state)
        {
            case GameStates.Menu:
                SwitchScreen (0);
                break;
            case GameStates.Options:
                SwitchScreen (1);
                break;
            case GameStates.Credits:
                SwitchScreen (2);
                break;
            case GameStates.LevelSelect:
                SwitchScreen (3);
                break;
            case GameStates.InGame:
                SwitchScreen (4);
                break;
            case GameStates.LevelComplete:
                SwitchScreen (5);
                break;
            case GameStates.InGameOptions:
                SwitchScreen (6);
                break;
        }
    }

    private void SwitchScreen (int screenNumber)
    {
        for (int i = 0; i < _Screens.Length; i++)
        {
            if (i == screenNumber)
                _Screens[i].SetActive (true);
            else
                _Screens[i].SetActive (false);
        }
    }

    private void OnDestroy ()
    {
        EventManager.OnStateChanged -= StateChanged;
    }
}
