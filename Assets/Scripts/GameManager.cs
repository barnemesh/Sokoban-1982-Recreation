using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Main menu runner. for now as simple as it can go. in the future should be able to control multiple levels.
 */
public class GameManager : MonoBehaviour
{
    private static GameManager _shared;

    private int _currentLevel = 1;

    // private String _currentScene;
    // private bool _wonLevel;
    //
    // public static void SetWon ()
    // {
    //     _shared._wonLevel = true;
    // }

    private void Awake()
    {
        _shared = this;
    }

    private void Start()
    {
        _currentLevel = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) SceneManager.LoadScene(_currentLevel);

        if (Input.GetKeyDown(KeyCode.Q)) Application.Quit();
    }
}