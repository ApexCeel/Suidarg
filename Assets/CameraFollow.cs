using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float smoothing = 5f; // How quickly the camera should move to its target position
    public float cameraSpeed = 5f; // How quickly the camera should move forward

    private Vector3 offset; // Distance between camera and player at the start of the game

    private void Start()
    {
        // Calculate the initial offset between the camera and the player
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        // Calculate the position the camera should be in based on the player's position and the offset
        Vector3 targetPosition = target.position + offset;

        // Move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);

        // Move the camera forward
        transform.Translate(Vector3.forward * (Time.deltaTime * cameraSpeed));
    }
}