using UnityEngine;
using TMPro;
using BNG;

/// <summary>
/// Attach to the grabbable Clipboard in Scene 4.
/// When grabbed, pops up PatientInfoCanvas showing one random patient's
/// player-facing medical history (excluding measurements, labs, answers, and advice).
/// </summary>
public class ClipboardPatientPopup : GrabbableEvents
{
    [Header("UI References")]
    [Tooltip("Drag PatientInfoCanvas here")]
    public GameObject patientInfoCanvas;

    [Tooltip("Drag PatientData_Text (TextMeshProUGUI) here")]
    public TextMeshProUGUI patientDataText;

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

        // Pick one random patient for this scene visit
        selectedPatient = PatientRecordDatabase.SelectRandomPatient();

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
    /// Displays the selected random patient's player-facing history on PatientInfoCanvas.
    /// </summary>
    public void ShowPatientPopup()
    {
        if (selectedPatient == null)
        {
            selectedPatient = PatientRecordDatabase.SelectRandomPatient();
        }

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
        patientDataText.enableAutoSizing = true;
        patientDataText.fontSizeMin = 16f;
        patientDataText.fontSizeMax = 28f;
        patientDataText.lineSpacing = 6f;
        patientDataText.text = selectedPatient.FormatPlayerHistory();
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
