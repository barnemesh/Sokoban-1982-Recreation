using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGameManager : MonoBehaviour
{
    #region Inspector

    [SerializeField]
    private GameObject winText;

    [SerializeField]
    private GameObject resetText;

    [SerializeField]
    private int levelNumber;

    #endregion

    #region Private Fields

    private static LevelGameManager _shared;
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
        get => _shared._targetCounter;
        set
        {
            _shared._targetCounter = value;
            if (_shared._targetCounter == 0) Debug.Log("Won. use f1 to do stuff.");
        }
    }

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        _shared = this;
        _targetScene = levelNumber;
    }

    private void Start()
    {
        resetText.SetActive(false);
        winText.SetActive(false);
    }

    private void Update()
    {
        // Use f1 to do stuff.
        if (!_waitingForInput && Input.GetKeyDown(KeyCode.F1))
        {
            _waitingForInput = true;
            _player.Pause = true;
            switch (_targetCounter)
            {
                case 0:
                    winText.SetActive(true);
                    _targetScene = (levelNumber + 1) % SceneManager.sceneCountInBuildSettings;
                    break;
                default:
                    resetText.SetActive(true);
                    _targetScene = levelNumber;
                    break;
            }
        }

        // if already waiting for input, check if there is input.
        if (!_waitingForInput) return;
        if (Input.GetKeyDown(KeyCode.Q)) Application.Quit();

        if (Input.GetKeyDown(KeyCode.Y)) SceneManager.LoadScene(_targetScene);

        if (Input.GetKeyDown(KeyCode.N))
        {
            _waitingForInput = false;
            _player.Pause = false;
            winText.SetActive(false);
            resetText.SetActive(false);
        }
    }

    #endregion
}