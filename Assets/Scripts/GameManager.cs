using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    private static int _resets;
    private static int _currentLevel;

    private static GameObject _winText;
    private static GameObject _lostText;
    private static GameObject _resetText;

    private static GameObject _activeText;
    public static int MaxResets { get; } = 4;
    public static int TargetCounter { get; set; }

    public static PlayerControl Player { get; set; }

    public static void TogglePlayerMovement ()
    {
        if ( Player != null )
            Player.Pause = !Player.Pause;
    }

    public static void SetLevel (int levelNumber)
    {
        if ( levelNumber == _currentLevel )
        {
            _resets++;
            return;
        }

        TargetCounter = 0;
        _currentLevel = levelNumber;
        _resets = 0;
    }

    private static int GetTargetScene ()
    {
        return TargetCounter switch
        {
            0 => (_currentLevel + 1) % SceneManager.sceneCountInBuildSettings,
            _ => _resets == MaxResets ? 0 : _currentLevel
        };
    }

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
            _ => _resets == MaxResets ? _lostText : _resetText
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

    public static void SetTexts (GameObject winText, GameObject lostText, GameObject resetText)
    {
        DeactivateText();
        _winText = winText;
        _lostText = lostText;
        _resetText = resetText;
    }
}