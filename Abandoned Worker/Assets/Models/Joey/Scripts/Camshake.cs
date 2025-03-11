using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform player; // Assign your player GameObject here
    public float sensitivity = 100f;
    public float bobIntensity = 0.05f;
    public float bobFrequency = 6f;

    private float xRotation = 0f;
    private float bobTimer = 0f;
    private Vector3 originalLocalPosition;
    private PlayerMovement playerMovement;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalLocalPosition = transform.localPosition;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement == null) return;

        // Camera movement
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);

        ApplyHeadBob();
    }

    void ApplyHeadBob()
    {
        if (playerMovement == null) return;

        float speed = playerMovement.GetComponent<Rigidbody>().velocity.magnitude;
        if (speed > 0.1f && playerMovement.isGrounded)
        {
            bobTimer += Time.deltaTime * bobFrequency;
            float yOffset = Mathf.Sin(bobTimer) * bobIntensity * (speed / playerMovement.speed);
            transform.localPosition = originalLocalPosition + new Vector3(0, yOffset, 0);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalLocalPosition, Time.deltaTime * 5f);
            bobTimer = 0f;
        }
    }
}