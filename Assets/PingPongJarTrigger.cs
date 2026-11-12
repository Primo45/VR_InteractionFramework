using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using BNG;

public class PingPongJarTrigger : MonoBehaviour
{
    public enum JarColor
    {
        White,      // Normal
        LightGreen, // At Risk
        DarkGreen,  // Stage 0 (Well Controlled)
        Yellow,     // Stage 1 (Mild)
        Orange,     // Stage 2 (Moderate / High)
        Red,        // Stage 3 (Severe / Critical)
        Black       // Complications
    }

    [Header("Jar Color")]
    public JarColor thisJarColor;

    [Header("Fallback Answer (used if useCurrentPatientAnswer is false)")]
    public JarColor correctAnswerColor;

    [Header("Patient Assessment")]
    [Tooltip("Check against the active patient's ping-pong answer in PatientRecordDatabase")]
    public bool useCurrentPatientAnswer = true;

    [Tooltip("Wait until the player releases the ball inside the jar before evaluating")]
    public bool waitUntilBallReleased = true;

    [Tooltip("Automatically load ResultSuccess or ResultFail after a ball is placed in the jar")]
    public bool loadResultScene = true;
    public string successSceneName = "Scene_09_ResultSuccess";
    public string failSceneName = "Scene_08_ResultFail";
    public float sceneLoadDelay = 0.6f;

    [Header("Events")]
    public UnityEvent onCorrectJar;
    public UnityEvent onWrongJar;

    private static bool hasEvaluatedInScene = false;
    private static bool sceneHookRegistered = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStaticState()
    {
        hasEvaluatedInScene = false;
    }

    private void Awake()
    {
        if (!sceneHookRegistered)
        {
            sceneHookRegistered = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hasEvaluatedInScene = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        TryEvaluateBall(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryEvaluateBall(other);
    }

    private void TryEvaluateBall(Collider other)
    {
        if (hasEvaluatedInScene) return;
        if (!other.CompareTag("PingPongBall")) return;

        if (waitUntilBallReleased)
        {
            Grabbable grabbable = other.GetComponentInParent<Grabbable>();
            if (grabbable != null && grabbable.BeingHeld)
            {
                return;
            }
        }

        hasEvaluatedInScene = true;

        JarColor expectedColor = correctAnswerColor;
        string patientInfo = "Manual Answer";

        if (useCurrentPatientAnswer)
        {
            var patient = PatientRecordDatabase.GetOrSelectCurrentPatient();
            expectedColor = patient.CorrectColor;
            patientInfo = $"{patient.Code} ({patient.FullName})";
        }

        bool isCorrect = (thisJarColor == expectedColor);

        if (isCorrect)
        {
            Debug.Log($"Correct! {patientInfo} -> Jar: {thisJarColor} (Expected: {expectedColor})");
            onCorrectJar?.Invoke();
        }
        else
        {
            Debug.Log($"Wrong! {patientInfo} -> Jar: {thisJarColor} (Expected: {expectedColor})");
            onWrongJar?.Invoke();
        }

        if (loadResultScene)
        {
            StartCoroutine(LoadResultSceneRoutine(isCorrect ? successSceneName : failSceneName));
        }
    }

    private IEnumerator LoadResultSceneRoutine(string sceneName)
    {
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(sceneName);
    }
}

