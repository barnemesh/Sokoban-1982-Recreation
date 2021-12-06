using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.GameCenter;

public class LevelGameManager : MonoBehaviour
{
    #region Inspector

    [SerializeField]
    private GameObject winText;

    [SerializeField]
    private GameObject resetText;

    [SerializeField]
    private GameObject loseText;

    [SerializeField]
    private int levelNumber;

    [SerializeField]
    private int maxResetCount = 4;

    [SerializeField]
    private LevelData levelData;

    [SerializeField]
    private GameObject boxPrefab;

    [SerializeField]
    private GameObject playerPrefab;

    #endregion


    #region Private Fields

    // todo: create a "GameManager" that creates LevelManager based on scene? and will be in control of F1 functions
    private static LevelGameManager _shared; // todo: remove from static?
    private static int _resets;
    private int _targetCounter;
    private Movement _player;
    private bool _waitingForInput;
    private int _targetScene;

    #endregion


    #region Properties

    /**
     * <summary>The player Movement script in this level</summary>
     */
    public static Movement Player
    {
        get => _shared._player;
        set => _shared._player = value;
    }

    /**
     * <summary>The number of incomplete targets in the level</summary>
     */
    public static int TargetCounter
    {
        // todo: if no longer static, find a different solution?
        get => _shared._targetCounter;
        set
        {
            _shared._targetCounter = value;
            if ( _shared._targetCounter == 0 ) Debug.Log("Won. use f1 to do stuff.");
        }
    }

    #endregion


    #region Monobehaviour

    private void Awake ()
    {
        _shared = this;
        _targetScene = levelNumber;
    }

    private void Start ()
    {
        resetText.SetActive(false);
        winText.SetActive(false);
        loseText.SetActive(false);

        Instantiate(playerPrefab, levelData.player, Quaternion.identity);
        // todo: count targets here and not at box start
        // todo: if not static, give boxes the manager as variable?
        GameObject box = new GameObject("Boxes");
        foreach ( var boxPosition in levelData.boxes )
        {
            Instantiate(boxPrefab, boxPosition, Quaternion.identity, box.transform);
        }
    }

    private void Update ()
    {
        // Use f1 to do stuff.
        if ( !_waitingForInput && Input.GetKeyDown(KeyCode.F1) )
        {
            _waitingForInput = true;
            _player.Pause = true;
            if ( _targetCounter == 0 )
            {
                winText.SetActive(true);
                _targetScene = (levelNumber + 1) % SceneManager.sceneCountInBuildSettings;
            }
            else if ( _resets == maxResetCount )
            {
                loseText.SetActive(true);
                _targetScene = 0;
            }
            else
            {
                resetText.SetActive(true);
                _targetScene = levelNumber;
            }
        }

        // if already waiting for input, check if there is input.
        if ( !_waitingForInput ) return;
        if ( Input.GetKeyDown(KeyCode.Q) )
        {
            Application.Quit();
        }

        if ( Input.GetKeyDown(KeyCode.Y) )
        {
            _resets = _targetScene == levelNumber ? _resets + 1 : 0;
            print(_resets);
            SceneManager.LoadScene(_targetScene);
        }

        if ( Input.GetKeyDown(KeyCode.N) )
        {
            _waitingForInput = false;
            _player.Pause = false;
            winText.SetActive(false);
            resetText.SetActive(false);
            loseText.SetActive(false);
        }
    }

    #endregion
}