using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Main menu runner. for now as simple as it can go. in the future should be able to control multiple levels.
 */
public class StartMenuManager : MonoBehaviour
{
    private void Start ()
    {
        GameManager.SetLevel(0);
    }

    // Update is called once per frame
    private void Update ()
    {
        if ( Input.GetKeyDown(KeyCode.Space) ) GameManager.SwitchToTargetScene();

        if ( Input.GetKeyDown(KeyCode.Q) ) Application.Quit();
    }
}