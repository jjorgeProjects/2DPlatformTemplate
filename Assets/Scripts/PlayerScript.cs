using UnityEngine;
//Import namespace to use callback
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerScript : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpForce = 10.0f;
    private Vector2 move;
    Rigidbody2D rb;
    PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check the input on each frame
        //Rewrite this "Vector2 move = callbackContext.ReadValue<Vector2>();" as:
        move = playerInput.actions["Movement"].ReadValue<Vector2>();
        //Debug.Log("Input modifified by el processor: " + move);


        //To test some behaviour we can still use the old way, only for testing!!
        //Keyboard, Gamepad, Mouse, ...
        //Gamepad.current.buttonSouth.wasPressedThisFrame
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("E key pressed");
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            Debug.Log("Changing action map");
            playerInput.SwitchCurrentActionMap("FlyingPlayerActionMap");
            Debug.Log(playerInput.currentActionMap);
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            Debug.Log("Changing action map");
            playerInput.SwitchCurrentActionMap("PlayerActionMap");
            Debug.Log(playerInput.currentActionMap);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(move.x, 0f) * speed, ForceMode2D.Impulse);

    }

    /*
    //Should be public
    public void Jump()
    {
        //This method is called 3 times:
        // - Starter: Starting Key down
        // - Performed: Performed Key down 
        // - Canceled: When key is released
        Debug.Log("Jumping");
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    */
    public void Jump(InputAction.CallbackContext callbackContext)
    {
        //This method is called 3 times:
        // - Starter: Starting Key down
        // - Performed: Performed Key down 
        // - Canceled: When key is released
        if (callbackContext.performed) { 
            Debug.Log("Jumping");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //To check the interactions and phase understanding
        Debug.Log(callbackContext.phase);
    }
    /*
    // Multiple pressing to perform movement...
    public void Move(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Vector2 move = callbackContext.ReadValue<Vector2>();
            Debug.Log(move);
            rb.AddForce(new Vector2(move.x, 0f) * speed, ForceMode2D.Impulse);
        }
    }
    */

    public void ControlsChanged(PlayerInput playerInput)
    {
        Debug.Log("Controller changed: " + playerInput.currentControlScheme);
    }


    //A different action
    public void Fly(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Debug.Log("Flying");
            rb.AddForce(Vector2.up * 1000.0f, ForceMode2D.Impulse);
        }
    }
}
