using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteScreenController : MonoBehaviour
{
    [SerializeField] private Text _PushesLabel = null;
    [SerializeField] private Text _MovesLabel = null;

    private void Awake ()
    {
        EventManager.OnBrickPushed += BrickPushed;
        EventManager.OnPlayerMoved += PlayerMoved;
    }

    private void BrickPushed (int amount, bool isCaller)
    {
        _PushesLabel.text = "Pushes: " + amount.ToString ();
    }

    private void PlayerMoved (int amount, bool isCaller)
    {
        _MovesLabel.text = "Moves: " + amount.ToString ();
    }

    private void OnDestroy ()
    {
        EventManager.OnBrickPushed -= BrickPushed;
        EventManager.OnPlayerMoved -= PlayerMoved;
    }

    public void SpawnNextLevel ()
    {
        StartCoroutine (GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().NextLevel ());
    }

    public void SpawnLastLevel ()
    {
        StartCoroutine (GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().LastLevel ());
    }

    public void BackToMenu ()
    {
        //TODO: Load menu scene.
    }
}
