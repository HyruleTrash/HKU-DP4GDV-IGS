using UnityEngine;

[CreateAssetMenu(fileName = "FirstPersonCamera", menuName = "FPS/FirstPersonCamera")]
public class FirstPersonCamera : ScriptableObject
{
    [SerializeField] private GameObject cameraPrefab;
    private Camera camera;
    
    // Sensitivity settings
    [SerializeField] private float mouseSensitivity = 100f;
    
    // Rotation limits
    [SerializeField] private float minVerticalAngle = -90f;
    [SerializeField] private float maxVerticalAngle = 90f;
    
    // Private variables for tracking rotation
    private Vector2 rotation;
    
    public bool Active
    {
        get
        {
            return active;
        }
        set
        {
            active = value;

            if (value == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                camera.enabled = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    private bool active;

    public void Load(GameObject playerEntityBody)
    {
        camera = Instantiate(cameraPrefab, playerEntityBody.transform, true).GetComponent<Camera>();

        if (camera != null)
        {
            rotation.x = camera.transform.rotation.x;
            rotation.y = playerEntityBody.transform.eulerAngles.y;
        }
    }

    public void CustomUpdate(GameObject playerEntityBody)
    {
        if (!Active)
            return;
        
        HandleMouseInput();
        ApplyRotation(playerEntityBody);
    }

    private void HandleMouseInput()
    {
        Vector2 mouse = Vector2.zero;
        mouse.x = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity; // TODO: convert to new input system
        mouse.y = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity;

        rotation.y += mouse.x;
        rotation.x -= mouse.y; 
        
        rotation.x = Mathf.Clamp(rotation.x, minVerticalAngle, maxVerticalAngle);
    }

    private void ApplyRotation(GameObject playerEntityBody)
    {
        // Apply horizontal rotation to parent (player)
        playerEntityBody.transform.localRotation = Quaternion.Euler(0, rotation.y, 0);
        
        // Apply vertical rotation to camera itself
        camera.transform.localRotation = Quaternion.Euler(
            rotation.x,
            0,
            0
        );
    }
}
