using UnityEngine;
using TMPro;
using BNG;

/// <summary>
/// Attach to the grabbable Clipboard in Scene 4 and Scene 6.
/// When grabbed, pops up PatientInfoCanvas showing the active patient's
/// player-facing history (and physical exam/lab results when includeMeasurements is true).
/// </summary>
public class ClipboardPatientPopup : GrabbableEvents
{
    [Header("UI References")]
    [Tooltip("Drag PatientInfoCanvas here")]
    public GameObject patientInfoCanvas;

    [Tooltip("Drag PatientData_Text (TextMeshProUGUI) here")]
    public TextMeshProUGUI patientDataText;

    [Header("Content Mode")]
    [Tooltip("False in Scene 4 (history only). True in Scene 6 (adds Physical Exam & Lab Results).")]
    public bool includeMeasurements = false;

    [Tooltip("True in Scene 4 (picks a new random patient). False in Scene 6 (reuses Scene 4's patient).")]
    public bool selectNewRandomPatientOnStart = true;

    [Header("VR Camera Positioning")]
    [Tooltip("Leave empty to auto-find CenterEyeAnchor or Camera.main")]
    public Transform vrCamera;
    public float distanceFromCamera = 0.85f;
    public float heightOffset = -0.05f;

    [Header("Popup Behavior")]
    [Tooltip("If true, hides the canvas when releasing the clipboard")]
    public bool hideOnRelease = true;

    [Header("Audio")]
    public AudioSource popupSound;

    private PatientRecordDatabase.PatientRecord selectedPatient;

    private void Start()
    {
        if (vrCamera == null)
        {
            FindVRCamera();
        }

        selectedPatient = selectNewRandomPatientOnStart
            ? PatientRecordDatabase.SelectRandomPatient()
            : PatientRecordDatabase.GetOrSelectCurrentPatient();

        if (patientDataText != null)
        {
            ApplyPatientText();
        }

        if (patientInfoCanvas != null)
        {
            patientInfoCanvas.SetActive(false);
        }
    }

    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);
        ShowPatientPopup();
    }

    public override void OnRelease()
    {
        base.OnRelease();

        if (hideOnRelease && patientInfoCanvas != null)
        {
            patientInfoCanvas.SetActive(false);
        }
    }

    /// <summary>
    /// Displays the active patient's information on PatientInfoCanvas.
    /// </summary>
    public void ShowPatientPopup()
    {
        selectedPatient = PatientRecordDatabase.GetOrSelectCurrentPatient();

        if (patientDataText != null)
        {
            ApplyPatientText();
        }

        if (patientInfoCanvas == null) return;

        if (!patientInfoCanvas.activeSelf)
        {
            PositionCanvasInFrontOfCamera();
            patientInfoCanvas.SetActive(true);
        }

        if (popupSound != null)
        {
            popupSound.Play();
        }
    }

    private void ApplyPatientText()
    {
        patientDataText.alignment = TextAlignmentOptions.TopLeft;
        patientDataText.enableAutoSizing = false;
        patientDataText.fontSize = 35f;
        patientDataText.lineSpacing = 4f;
        patientDataText.text = includeMeasurements
            ? selectedPatient.FormatPlayerHistoryWithMeasurements()
            : selectedPatient.FormatPlayerHistory();
    }

    private void PositionCanvasInFrontOfCamera()
    {
        if (vrCamera == null)
        {
            FindVRCamera();
        }

        if (vrCamera == null) return;

        Vector3 forwardDir = vrCamera.forward;
        forwardDir.y = 0f;

        if (forwardDir.sqrMagnitude < 0.001f)
        {
            forwardDir = Vector3.forward;
        }
        else
        {
            forwardDir.Normalize();
        }

        Vector3 targetPos = vrCamera.position + (forwardDir * distanceFromCamera);
        targetPos.y = vrCamera.position.y + heightOffset;

        patientInfoCanvas.transform.position = targetPos;
        patientInfoCanvas.transform.rotation = Quaternion.LookRotation(forwardDir);
    }

    private void FindVRCamera()
    {
        GameObject centerEye = GameObject.Find("CenterEyeAnchor");
        if (centerEye != null && centerEye.activeInHierarchy)
        {
            vrCamera = centerEye.transform;
            return;
        }

        if (Camera.main != null)
        {
            vrCamera = Camera.main.transform;
        }
    }
}
