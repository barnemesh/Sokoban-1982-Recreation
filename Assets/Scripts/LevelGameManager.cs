using Scriptable_Objects.Level_Datas;
using TMPro;
using UnityEngine;

public class LevelGameManager : MonoBehaviour
{
    #region Inspector

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject winText;

    [SerializeField]
    private GameObject resetText;

    [SerializeField]
    private GameObject lostText;

    [SerializeField]
    private int levelNumber;

    [SerializeField]
    private LevelData levelData;

    [SerializeField]
    private GameObject boxPrefab;

    [SerializeField]
    private GameObject playerPrefab;

    #endregion


    #region Private Fields

    private static int _resets;
    private bool _waitingForInput;

    #endregion


    #region Monobehaviour

    private void Start ()
    {
        // Update GameManager with current level data
        resetText.SetActive(false);
        winText.SetActive(false);
        lostText.SetActive(false);
        GameManager.SetTexts(winText, lostText, resetText);
        GameManager.ScoreText = scoreText; // todo refactor to texts
        GameManager.SetLevel(levelNumber);

        // Create player and Boxes
        Instantiate(playerPrefab, levelData.player, Quaternion.identity);

        var box = new GameObject("Boxes");
        foreach ( var boxPosition in levelData.boxes )
            Instantiate(boxPrefab, boxPosition, Quaternion.identity, box.transform);
    }

    private void Update ()
    {
        // Use f1 to do stuff.
        if ( !_waitingForInput && Input.GetKeyDown(KeyCode.F1) )
        {
            _waitingForInput = true;
            GameManager.TogglePlayerMovement();
            GameManager.ActivateText();
        }

        // if already waiting for input, check if there is input.
        if ( !_waitingForInput ) return;
        if ( Input.GetKeyDown(KeyCode.Q) )
            Application.Quit();

        if ( Input.GetKeyDown(KeyCode.Y) )
            GameManager.SwitchToTargetScene();

        if ( Input.GetKeyDown(KeyCode.N) )
        {
            _waitingForInput = false;
            GameManager.TogglePlayerMovement();
            GameManager.DeactivateText();
        }
    }

    #endregion
}