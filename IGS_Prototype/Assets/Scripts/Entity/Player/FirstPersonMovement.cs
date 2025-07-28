using UnityEngine;

[CreateAssetMenu(fileName = "FirstPersonMovement", menuName = "FPS/FirstPersonMovement")]
public class FirstPersonMovement : ScriptableObject
{
    [SerializeField] 
    private float moveSpeed = 2f;
    [SerializeField] 
    private float maxMoveSpeed = 7f;
    [SerializeField] 
    private float groundDrag = 5f;

    private Rigidbody rb;
    
    public void Load(GameObject playerEntityBody)
    {
        rb = playerEntityBody.AddComponent<Rigidbody>();
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
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // TODO: convert to new input system
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = playerTransform.right * horizontalInput + playerTransform.forward * verticalInput;
        
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
