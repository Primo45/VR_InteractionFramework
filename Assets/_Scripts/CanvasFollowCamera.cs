using UnityEngine;

/// <summary>
/// Attach this to a world-space Canvas so it smoothly follows the VR camera
/// using Lerp for position and Slerp for rotation.
/// </summary>
public class CanvasFollowCamera : MonoBehaviour
{
    [Header("Target Camera")]
    [Tooltip("Leave empty to auto-find Camera.main at Start")]
    public Transform targetCamera;

    [Header("Follow Settings")]
    [Tooltip("Distance in front of the camera (2m is optimal for VR UI viewing)")]
    public float followDistance = 2f;

    [Tooltip("Vertical offset from the camera eye level")]
    public float heightOffset = -0.05f;

    [Tooltip("How quickly the canvas follows (higher = snappier)")]
    [Range(0.5f, 20f)]
    public float positionLerpSpeed = 6f;

    [Tooltip("How quickly the canvas rotates to face the camera")]
    [Range(0.5f, 20f)]
    public float rotationLerpSpeed = 6f;

    [Header("Rotation Constraints")]
    [Tooltip("Only follow the camera's Y-axis rotation (keeps canvas upright)")]
    public bool followYawOnly = true;

    private void Start()
    {
        FindTargetCamera();
    }

    private void FindTargetCamera()
    {
        if (targetCamera != null) return;

        // 1. Try finding BNG Framework CenterEyeAnchor (the actual VR headset camera)
        GameObject centerEye = GameObject.Find("CenterEyeAnchor");
        if (centerEye != null && centerEye.activeInHierarchy)
        {
            targetCamera = centerEye.transform;
            return;
        }

        // 2. Try Camera.main
        Camera mainCam = Camera.main;
        if (mainCam != null && mainCam.gameObject.activeInHierarchy)
        {
            targetCamera = mainCam.transform;
            return;
        }

        // 3. Fallback: Search all active cameras and prefer any under XR rig
        Camera[] cameras = FindObjectsOfType<Camera>();
        foreach (Camera cam in cameras)
        {
            if (cam.gameObject.activeInHierarchy)
            {
                if (cam.name.Contains("Center") || cam.name.Contains("Eye") || cam.name.Contains("VR") || cam.transform.root.name.Contains("XR"))
                {
                    targetCamera = cam.transform;
                    return;
                }
            }
        }

        if (cameras.Length > 0)
        {
            targetCamera = cameras[0].transform;
        }
    }

    private void LateUpdate()
    {
        if (targetCamera == null)
        {
            FindTargetCamera();
            if (targetCamera == null) return;
        }

        // Calculate desired position: in front of the camera at followDistance
        Vector3 forward = targetCamera.forward;

        if (followYawOnly)
        {
            // Project forward onto the horizontal plane
            forward.y = 0f;
            forward.Normalize();

            if (forward.sqrMagnitude < 0.001f)
            {
                forward = targetCamera.forward;
                forward.y = 0f;
                forward.Normalize();
            }
        }

        Vector3 desiredPosition = targetCamera.position
            + forward * followDistance
            + Vector3.up * heightOffset;

        // Smoothly move the canvas toward the desired position using Lerp
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            Time.deltaTime * positionLerpSpeed
        );

        // Calculate desired rotation: face the same direction as the camera
        Quaternion desiredRotation;

        if (followYawOnly)
        {
            // Only rotate around Y axis (keep canvas upright)
            float yaw = targetCamera.eulerAngles.y;
            desiredRotation = Quaternion.Euler(0f, yaw, 0f);
        }
        else
        {
            desiredRotation = targetCamera.rotation;
        }

        // Smoothly rotate the canvas using Slerp
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            Time.deltaTime * rotationLerpSpeed
        );
    }
}
