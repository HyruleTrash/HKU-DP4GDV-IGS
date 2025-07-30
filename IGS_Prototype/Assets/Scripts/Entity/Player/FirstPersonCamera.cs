using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "FirstPersonCamera", menuName = "FPS/FirstPersonCamera")]
public class FirstPersonCamera : ScriptableObject
{
    [SerializeField] private GameObject cameraPrefab;
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private float mouseSensitivity = 100f;
    
    // Rotation limits
    [SerializeField] private float minVerticalAngle = -90f;
    [SerializeField] private float maxVerticalAngle = 90f;
    
    private Vector2 rotation;
    private Camera camera;
    
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
        camera = Instantiate(cameraPrefab, playerEntityBody.transform).GetComponent<Camera>();

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
        Vector2 input = lookAction.action.ReadValue<Vector2>();
        Vector2 mouse = Vector2.zero;
        mouse.x = input.x * Time.deltaTime * mouseSensitivity;
        mouse.y = input.y * Time.deltaTime * mouseSensitivity;
        
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

    public Transform GetTransform()
    {
        return camera?.transform;
    }
}
