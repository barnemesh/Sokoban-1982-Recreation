using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public static class GameManager
{
    #region Private Variables

    private const string ScoreFormat = "Moves: {0}  \t \t Resets: {1}";
    private static bool _levelWon;

    private static int _resets;
    private static int _currentLevel;
    private static int _targetCounter;
    private static int _movesInLevel;

    private static GameObject _winText;
    private static GameObject _lostText;
    private static GameObject _resetText;

    private static GameObject _activeText;
    private static PlayerControl _playerControl;
    
    #endregion


    #region Properties
    
    public static TextMeshProUGUI ScoreText { get; set; }

    public static int MaxResets { get; } = 4;

    public static int MoveCounter
    {
        get => _movesInLevel;
        set
        {
            if (_levelWon) return;
            _movesInLevel = value;
            UpdateScore();
        } 
    }

    public static int TargetCounter
    {
        get => _targetCounter;
        set
        {
            _targetCounter = value;
            _levelWon = _targetCounter == 0;
        }
    }

    public static PlayerControl Player
    {
        get => _playerControl;
        set
        {
            _playerControl = value;
            _levelWon = false;
            MoveCounter = 0;
        }
    }
    
    #endregion


    #region Setter Methods

    public static void SetTexts (GameObject winText, GameObject lostText, GameObject resetText)
    {
        DeactivateText();
        _winText = winText;
        _lostText = lostText;
        _resetText = resetText;
        // todo: set score texts
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
        _levelWon = false;
        _currentLevel = levelNumber;
        UpdateScore(); // todo: refactor player resets and this?
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

    private static void UpdateScore ()
    {
        if ( ScoreText == null ) return;
        ScoreText.text = string.Format(ScoreFormat, _movesInLevel, MaxResets - _resets);
    }
    #endregion
}