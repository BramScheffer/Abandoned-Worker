using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public float pickupRange = 3f; // How far you can pick up objects
    public Transform holdPoint; // Assign in the inspector (child of player)
    public float moveSpeed = 15f; // Speed of moving the object to the hold point
    public float rotationSpeed = 10f; // Rotation smoothing speed

    private GameObject heldObject;
    private Rigidbody heldRb;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Press E to pick up/drop
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                DropObject();
            }
        }

        if (heldObject != null)
        {
            MoveObjectToHoldPoint();
        }
    }

    void TryPickupObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.gameObject.CompareTag("Pickable")) // Ensure objects have this tag
            {
                heldObject = hit.collider.gameObject;
                heldRb = heldObject.GetComponent<Rigidbody>();

                if (heldRb != null)
                {
                    heldRb.useGravity = false;
                    heldRb.freezeRotation = true;
                    heldRb.velocity = Vector3.zero;
                    heldRb.angularVelocity = Vector3.zero;

                    // Parent to holdPoint so it rotates with the player
                    heldObject.transform.SetParent(holdPoint);
                }
            }
        }
    }

    void MoveObjectToHoldPoint()
    {
        // Smoothly move the object to the hold position
        heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, holdPoint.position, moveSpeed * Time.deltaTime);

        // Smoothly rotate the object to match the hold point’s rotation
        heldObject.transform.rotation = Quaternion.Slerp(heldObject.transform.rotation, holdPoint.rotation, rotationSpeed * Time.deltaTime);
    }

    void DropObject()
    {
        if (heldRb != null)
        {
            heldRb.useGravity = true;
            heldRb.freezeRotation = false;
            heldObject.transform.parent = null; // Unparent the object
            heldRb = null;
        }
        heldObject = null;
    }
}
