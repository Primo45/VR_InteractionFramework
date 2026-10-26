using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatientInteraction : MonoBehaviour
{
    [Header("UI Canvas")]
    [Tooltip("Drag PatientInfoCanvas here")]
    public GameObject patientInfoCanvas;

    [Header("VR Camera")]
    [Tooltip("Drag CenterEyeAnchor here")]
    public Transform vrCamera;

    [Header("Audio Settings")]
    [Tooltip("Drag SoundManager or AudioSource here")]
    public AudioSource clickSound;
    public float soundDelay = 0.2f;

    [Header("Scene Navigation")]
    [Tooltip("Next scene name")]
    public string nextSceneName = "Scene_05_PhysicalExam";
    [Tooltip("Previous scene name")]
    public string previousSceneName = "Scene_03_Tutorial";

    [Header("Spawn Settings")]
    [Tooltip("Distance from camera (meters)")]
    public float distanceFromCamera = 0.9f;
    [Tooltip("Height offset relative to camera")]
    public float heightOffset = 0.0f;

    private void Start()
    {
        // Hide Canvas at start
        if (patientInfoCanvas != null)
        {
            patientInfoCanvas.SetActive(false);
        }

        // Auto-find camera if not assigned
        if (vrCamera == null && Camera.main != null)
        {
            vrCamera = Camera.main.transform;
        }
    }

    // Open Canvas (called when player clicks or presses button)
    public void OpenPatientUI()
    {
        if (patientInfoCanvas != null)
        {
            if (vrCamera == null && Camera.main != null)
            {
                vrCamera = Camera.main.transform;
            }

            if (vrCamera != null)
            {
                // Calculate forward direction at eye level
                Vector3 forwardDir = vrCamera.forward;
                forwardDir.y = 0;
                forwardDir.Normalize();

                Vector3 targetPos = vrCamera.position + (forwardDir * distanceFromCamera);
                targetPos.y = vrCamera.position.y + heightOffset;

                patientInfoCanvas.transform.position = targetPos;
                patientInfoCanvas.transform.rotation = Quaternion.LookRotation(forwardDir);
            }

            patientInfoCanvas.SetActive(true);
        }
    }

    // Next button -> play sound then load next scene
    public void OnClickNext()
    {
        StartCoroutine(PlaySoundAndChangeScene(nextSceneName));
    }

    // Back button -> play sound then load previous scene
    public void OnClickBack()
    {
        StartCoroutine(PlaySoundAndChangeScene(previousSceneName));
    }

    private IEnumerator PlaySoundAndChangeScene(string sceneName)
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }

        yield return new WaitForSeconds(soundDelay);

        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
