using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "FirstPersonMovement", menuName = "FPS/FirstPersonMovement")]
public class FirstPersonMovement : ScriptableObject
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [Header("Physics")]
    [SerializeField] private float mass = 2f;
    [SerializeField] private float groundDrag = 5f;
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 7f;

    private Rigidbody rb;
    
    public void Load(GameObject playerEntityBody)
    {
        rb = playerEntityBody.AddComponent<Rigidbody>();
        rb.mass = mass;
        rb.freezeRotation = true;
        rb.linearDamping = groundDrag;
    }

    public bool Active
    {
        get => active;
        set => active = value;
    }
    private bool active;
    
    public void CustomUpdateAtFixedRate(GameObject playerEntityBody)
    {
        if (!Active)
            return;
        
        HandleInput(playerEntityBody.transform);
        LimitHorizontalMovement();
    }
    
    private void HandleInput(Transform playerTransform)
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 movementDirection = playerTransform.right * input.x + playerTransform.forward * input.y;
        
        rb.AddForce(movementDirection.normalized * moveSpeed, ForceMode.Force);
    }
    
    private void LimitHorizontalMovement()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        
        if (flatVel.magnitude > maxMoveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxMoveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
