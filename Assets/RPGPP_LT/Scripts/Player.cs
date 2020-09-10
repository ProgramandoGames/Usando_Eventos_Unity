using UnityEngine;

public class Player : MonoBehaviour {

    FirstPersonCamera fpsCamera;

    #region MOVEMENT_ATTRIBS

    CharacterController controller;

    Vector3 groundVelocity;
    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;

    // ground movement variables
    float walkSpeed = 3f;
    float runSpeed = 8f;
    float speedTransition = 10f;

    float maxSpeed = 0;
    float acceleration = 50f;
    float desceleartion = 20f;
    float currentVelocity;

    bool isMoving = false;
    bool running = false;

    // air variables
    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;

    #endregion


    public delegate void ItemCollectedHandler(string msg);

    public event ItemCollectedHandler ItemCollected;

    bool inventoryOpen = false;

    void Start() {

        #region GRAVITY_CALCULATIONS
        controller = GetComponent<CharacterController>();
        gravity    = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed  = (2 * maxJumpHeight) / timeToMaxHeight;
        #endregion

        fpsCamera = Camera.main.GetComponent<FirstPersonCamera>();


    }

    void LateUpdate() {
        fpsCamera._LateUpdate();
    }

    void Update() {

        if (inventoryOpen) return;

        fpsCamera._Update();

        if (Input.GetKeyDown(KeyCode.Mouse0)) {

            RaycastHit hitInfo;
            if (Physics.Raycast(fpsCamera.transform.position, 
                                fpsCamera.transform.forward, out hitInfo, 
                                3f, LayerMask.GetMask("interactable"))) {

                IInteractable obj = hitInfo.transform.gameObject.GetComponent<IInteractable>();
                
                if (obj != null) {

                    string name = obj.Interact();

                    if (ItemCollected != null) {
                        ItemCollected(name);
                    }

                }
            }

        }
    
        #region MOVEMENT

        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        isMoving = (Mathf.Abs(forwardInput) >= Mathf.Epsilon || Mathf.Abs(strafeInput) >= Mathf.Epsilon) ? true : false;

        if (isMoving) {
            running = Input.GetKey(KeyCode.LeftShift);
        } else {
            running = false;
        }

        forward = forwardInput * controller.transform.forward;
        strafe = strafeInput * controller.transform.right;

        groundVelocity += (forward + strafe).normalized * acceleration * Time.deltaTime;

        if (running) {
            maxSpeed = Mathf.Lerp(maxSpeed, runSpeed, speedTransition * Time.deltaTime);
        } else {
            maxSpeed = Mathf.Lerp(maxSpeed, walkSpeed, speedTransition * Time.deltaTime);
        }

        if (groundVelocity.magnitude >= maxSpeed) {
            groundVelocity = groundVelocity.normalized * maxSpeed;
        }

        if (!isMoving) {
            groundVelocity = Vector3.Lerp(groundVelocity, Vector3.zero, desceleartion * Time.deltaTime);
        }

        vertical += gravity * Time.deltaTime * Vector3.up;

        if (controller.isGrounded) {
            vertical = Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) {
            vertical = jumpSpeed * Vector3.up;
        }

        if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0) {
            vertical = Vector3.zero;
        }

        Vector3 finalVelocity = groundVelocity + vertical;

        controller.Move(finalVelocity * Time.deltaTime);

        #endregion

    }

    public void OnInventoryOpened() {
        inventoryOpen = !inventoryOpen;
    }


}
