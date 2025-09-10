using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Status de player")]
    private CharacterController controller;
    private Transform myCamera;

    public float speedMove;
    public float speedRot;
    public float gravity;

    public float SpeedMove => speedMove;
    public float SpeedRot => speedRot;
    public float Gravity => gravity;

    //teste de input system
    private Vector2 inputVector;
    private InputAction moveAction;
    //private InputAction toggleAction;

    
   

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
        //Teste
        SetupInputSystem();
        
    }
    //memorando
    //quero que o player foque em objetos relevantes quando passar por eles ou aperta um botão

    // Update is called once per frame
    

    void SetupInputSystem()
    {
        moveAction = new InputAction("move",InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx=> inputVector = Vector2.zero;

        moveAction.Enable();

    }

    void FixedUpdate()
    {
        HandleMovement();

    }

   
    void HandleMovement()
    {
        Vector3 movimento = new (inputVector.x, 0, inputVector.y);
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0f;

        controller.Move(speedMove * Time.fixedDeltaTime * movimento);
        controller.Move(new Vector3(0, gravity, 0) * Time.fixedDeltaTime);

        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.fixedDeltaTime * speedRot);
        }

    }
    public void SetSpeedMove(float newSpeed){speedMove = newSpeed;}
    public void SetSpeedRot(float newSpeedRot){speedRot = newSpeedRot;}
    public void SetGravity(float newGravity){gravity = newGravity;}



    //guardar aqui
    /*
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(horizontal,0, vertical);
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0f;

        controller.Move(movimento*Time.deltaTime*speedMove);
        controller.Move(new Vector3(0, gravity, 0) * Time.deltaTime);//gravidade

        if(movimento != Vector3.zero) 
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * speedRot);
    */
}
