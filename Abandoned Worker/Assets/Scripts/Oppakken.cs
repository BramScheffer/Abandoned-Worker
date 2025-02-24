using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Transform holdPosition; // Position where the object will be held
    public float pickupRange = 3f; // Distance at which the player can pick up objects
    public LayerMask pickupLayer; // Layer for pickable objects
    public KeyCode pickupKey = KeyCode.E; // Key to pick up and drop objects

    private Rigidbody heldObject;

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (heldObject == null)
            {
                TryPickup();
            }
            else
            {
                DropObject();
            }
        }

        if (heldObject != null)
        {
            MoveHeldObject();
        }
    }

    void TryPickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange, pickupLayer))
        {
            if (hit.collider.gameObject.TryGetComponent(out Rigidbody rb))
            {
                heldObject = rb;
                heldObject.useGravity = false;
                heldObject.freezeRotation = true;
            }
        }
    }

    void MoveHeldObject()
    {
        Vector3 targetPosition = holdPosition.position;
        Vector3 direction = (targetPosition - heldObject.position) * 10f;
        heldObject.velocity = direction;
    }

    void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.useGravity = true;
            heldObject.freezeRotation = false;
            heldObject = null;
        }
    }
}
