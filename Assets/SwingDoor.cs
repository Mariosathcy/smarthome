using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class SwingDoor : MonoBehaviour
{
    public Vector3 closedEuler;       // hinge rotation when closed
    public Vector3 openEuler;         // hinge rotation when fully open
    public float speed = 120f;        // degrees per second

    bool isOpen;
    Quaternion closedRot, openRot;

    void Awake()
    {
        closedRot = Quaternion.Euler(closedEuler);
        openRot = Quaternion.Euler(openEuler);
        transform.localRotation = closedRot;
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(args => isOpen = !isOpen);
    }

    void Update()
    {
        Quaternion target = isOpen ? openRot : closedRot;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, target, speed * Time.deltaTime);
    }

    [ContextMenu("Save Closed Pose")]
    void SaveClosed()
    {
        closedEuler = transform.localEulerAngles;
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    [ContextMenu("Save Open Pose")]
    void SaveOpen()
    {
        openEuler = transform.localEulerAngles;
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}