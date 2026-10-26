using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Universal scene navigation manager.
/// Attach to any GameObject in a scene to enable sequential scene navigation.
/// Automatically determines next/previous scene based on the defined order.
/// </summary>
public class SceneNavigationManager : MonoBehaviour
{
    // Define the sequential scene flow order
    private static readonly string[] sceneOrder = new string[]
    {
        "Scene_01_Login",
        "Scene_02_Register",
        "Scene_03_Tutorial",
        "Scene_04_PatientInfo",
        "Scene_05_PhysicalExam",
        "Scene_06_SelectPingPong",
        "Scene_07_HealthAdvice"
        // Scene_08 and Scene_09 are result screens, navigated to by PingPongSelector
    };

    [Header("Audio Settings")]
    [Tooltip("ลาก SoundManager หรือ AudioSource มาใส่ตรงนี้")]
    public AudioSource clickAudioSource;
    public float soundDelay = 0.2f;

    /// <summary>
    /// Get the index of the current scene in the scene order.
    /// Returns -1 if the current scene is not in the order list.
    /// </summary>
    private int GetCurrentSceneIndex()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        for (int i = 0; i < sceneOrder.Length; i++)
        {
            if (sceneOrder[i] == currentSceneName)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Navigate to the next scene in the sequential order.
    /// If on the last scene in the order, stays on current scene.
    /// </summary>
    public void GoToNextScene()
    {
        int currentIndex = GetCurrentSceneIndex();
        if (currentIndex >= 0 && currentIndex < sceneOrder.Length - 1)
        {
            string nextScene = sceneOrder[currentIndex + 1];
            StartCoroutine(PlaySoundAndLoad(nextScene));
        }
        else
        {
            Debug.LogWarning("SceneNavigationManager: No next scene available from " + SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// Navigate to the previous scene in the sequential order.
    /// If on the first scene, stays on current scene.
    /// </summary>
    public void GoToPreviousScene()
    {
        int currentIndex = GetCurrentSceneIndex();
        if (currentIndex > 0)
        {
            string prevScene = sceneOrder[currentIndex - 1];
            StartCoroutine(PlaySoundAndLoad(prevScene));
        }
        else
        {
            Debug.LogWarning("SceneNavigationManager: No previous scene available from " + SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// Navigate to a specific scene by name (for special cases like result screens).
    /// </summary>
    public void GoToScene(string sceneName)
    {
        StartCoroutine(PlaySoundAndLoad(sceneName));
    }

    /// <summary>
    /// Navigate back to the login/main menu scene.
    /// </summary>
    public void GoToMainMenu()
    {
        StartCoroutine(PlaySoundAndLoad("Scene_01_Login"));
    }

    private IEnumerator PlaySoundAndLoad(string sceneName)
    {
        if (clickAudioSource != null)
        {
            clickAudioSource.Play();
            yield return new WaitForSeconds(soundDelay);
        }

        Debug.Log("กำลังโหลดฉาก: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
