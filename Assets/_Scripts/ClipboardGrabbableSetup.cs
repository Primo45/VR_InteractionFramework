using UnityEngine;
using BNG;

/// <summary>
/// Automatically configures the Clipboard object in Scene 4 so it can be grabbed in VR
/// using the BNG Interaction Framework.
/// </summary>
public class ClipboardGrabbableSetup : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Name of the clipboard GameObject to make grabbable")]
    public string clipboardObjectName = "Clipboard";

    [Header("Grab Settings")]
    public GrabButton grabButton = GrabButton.GripOrTrigger;
    public GrabPhysics grabPhysics = GrabPhysics.Kinematic;
    public bool remoteGrabbable = false;
    public float minWorldColliderThickness = 0.025f;

    private void Awake()
    {
        GameObject clipboard = FindClipboard();
        if (clipboard == null)
        {
            Debug.LogWarning($"ClipboardGrabbableSetup: Could not find GameObject named '{clipboardObjectName}'.");
            return;
        }

        EnsureCollider(clipboard);
        EnsureRigidbody(clipboard);
        EnsureGrabbable(clipboard);
    }

    private GameObject FindClipboard()
    {
        GameObject office = GameObject.Find("DoctorsOffice2");
        if (office != null)
        {
            Transform found = office.transform.Find(clipboardObjectName);
            if (found != null)
            {
                return found.gameObject;
            }
        }

        return GameObject.Find(clipboardObjectName);
    }

    private void EnsureCollider(GameObject target)
    {
        if (target.GetComponent<Collider>() != null)
        {
            return;
        }

        BoxCollider box = target.AddComponent<BoxCollider>();
        MeshFilter mf = target.GetComponent<MeshFilter>();
        if (mf != null && mf.sharedMesh != null)
        {
            Bounds b = mf.sharedMesh.bounds;
            Vector3 scale = target.transform.lossyScale;
            float minX = Mathf.Abs(scale.x) > 0.0001f ? minWorldColliderThickness / Mathf.Abs(scale.x) : 0.025f;
            float minY = Mathf.Abs(scale.y) > 0.0001f ? minWorldColliderThickness / Mathf.Abs(scale.y) : 0.025f;
            float minZ = Mathf.Abs(scale.z) > 0.0001f ? minWorldColliderThickness / Mathf.Abs(scale.z) : 0.025f;

            box.center = b.center;
            box.size = new Vector3(
                Mathf.Max(b.size.x, minX),
                Mathf.Max(b.size.y, minY),
                Mathf.Max(b.size.z, minZ)
            );
        }
    }

    private void EnsureRigidbody(GameObject target)
    {
        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = target.AddComponent<Rigidbody>();
        }

        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void EnsureGrabbable(GameObject target)
    {
        Grabbable existing = target.GetComponent<Grabbable>();
        if (existing != null)
        {
            return;
        }

        Grabbable grab = target.AddComponent<Grabbable>();
        grab.GrabButton = grabButton;
        grab.Grabtype = HoldType.Inherit;
        grab.GrabPhysics = grabPhysics;
        grab.GrabMechanic = GrabType.Precise;
        grab.RemoteGrabbable = remoteGrabbable;
        grab.CanBeDropped = true;
        grab.ForceDisableKinematicOnDrop = false;
        grab.ParentHandModel = true;
        grab.SnapHandModel = true;
    }
}
