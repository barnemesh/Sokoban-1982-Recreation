using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    #region Private Variables

    private static int _resets;
    private static int _currentLevel;
    private static int _targetCounter;
    private static int _movesWhenWon;

    private static GameObject _winText;
    private static GameObject _lostText;
    private static GameObject _resetText;

    private static GameObject _activeText;
    private static PlayerControl _playerControl;

    #endregion


    #region Properties

    public static int MaxResets { get; } = 4;

    public static int TargetCounter
    {
        get => _targetCounter;
        set
        {
            _targetCounter = value;
            if ( _targetCounter == 0 && _movesWhenWon == 0 )
            {
                _movesWhenWon = MoveCounter;
                Debug.Log($"Moves when won: {_movesWhenWon}. Dont forget F1.");
            }
        }
    }

    public static PlayerControl Player
    {
        get => _playerControl;
        set
        {
            _playerControl = value;
            MoveCounter = 0;
            _movesWhenWon = 0;
        }
    }

    public static int MoveCounter { get; set; }

    #endregion


    #region Setter Methods

    public static void SetTexts (GameObject winText, GameObject lostText, GameObject resetText)
    {
        DeactivateText();
        _winText = winText;
        _lostText = lostText;
        _resetText = resetText;
    }
    
    public static void TogglePlayerMovement ()
    {
        if ( Player != null )
            Player.Pause = !Player.Pause;
    }

    public static void SetLevel (int levelNumber)
    {
        _resets = levelNumber == _currentLevel ? _resets + 1 : 0;
        TargetCounter = 0;
        _currentLevel = levelNumber;
    }

    #endregion


    #region Manager Methods

    public static void SwitchToTargetScene ()
    {
        DeactivateText();
        SceneManager.LoadScene(GetTargetScene());
    }

    public static void ActivateText ()
    {
        DeactivateText();
        _activeText = TargetCounter switch
        {
            0 => _winText,
            _ => _resets >= MaxResets ? _lostText : _resetText
        };

        _activeText.SetActive(true);
    }

    public static void DeactivateText ()
    {
        if ( _activeText == null )
            return;

        _activeText.SetActive(false);
        _activeText = null;
    }

    #endregion


    #region Private Helper Methods

    private static int GetTargetScene ()
    {
        return TargetCounter switch
        {
            0 => (_currentLevel + 1) % SceneManager.sceneCountInBuildSettings,
            _ => _resets >= MaxResets ? 0 : _currentLevel
        };
    }

    #endregion
}