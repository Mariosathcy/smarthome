using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class SlidingDoor : MonoBehaviour
{
    public Transform door;                                   // the moving leaf (Door_01)
    public Vector3 openOffset = new Vector3(0f, 0f, 1.22f);  // how far it slides, in local space
    public float speed = 2f;                                 // metres per second

    Vector3 closedPos;
    bool isOpen;

    void Awake()
    {
        closedPos = door.localPosition;
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(args => Toggle());
    }

    public void Toggle() { isOpen = !isOpen; Debug.Log("Door toggled, open = " + isOpen); }

    void Update()
    {
        Vector3 target = isOpen ? closedPos + openOffset : closedPos;
        door.localPosition = Vector3.MoveTowards(door.localPosition, target, speed * Time.deltaTime);
    }
}