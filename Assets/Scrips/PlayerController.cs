using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Quase isso devo da uma lida no codigo e corrigir ele 
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Entradas de Classe")]
    private CharacterController controller;
    private Transform myCamera;


    [Header("Status do Jogador")]
    public float speedMove;
    public float speedRot;
    public float gravity;
    private Vector2 direction;
    private bool canJump;

    public float SpeedMove => speedMove;
    public float SpeedRot => speedRot;
    public float Gravity => gravity;

    

    // Input system
    private Vector2 inputVector;
    private InputAction moveAction;
    


    [SerializeField] private int currentMeshIndex = 0; // Índice da mesh atual



    void Awake()
    {
       StateManager.Instance.OnGameStateChanged += OnGameStateChanged;
       controller = GetComponent<CharacterController>();
    }



    public void Mover(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void Pular(InputAction.CallbackContext context) 
    {
        if(context.performed && canJump)
        {
            //pula
            canJump = false;
        }
    }

    void Start()
    {   
        myCamera = Camera.main.transform;
    }

  
    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 movimento = new Vector3(direction.x, 0, direction.y);
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0f;

        controller.Move(speedMove * Time.fixedDeltaTime * movimento);
        controller.Move(new Vector3(0, gravity, 0) * Time.fixedDeltaTime);

        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.fixedDeltaTime * speedRot);
        }
    }

    private void ToggleObjectMesh()
    {

    }

    public void SetMesh(int meshIndex)
    {
       
    }

    public void AddAlternateMesh(Mesh newMesh)
    {
       
    }

    public void RestoreOriginalMesh()
    {
    }

    public void SetSpeedMove(float newSpeed) { speedMove = newSpeed; }
    public void SetSpeedRot(float newSpeedRot) { speedRot = newSpeedRot; }
    public void SetGravity(float newGravity) { gravity = newGravity; }


    void OnDisable()
    {
        // Desabilitar inputs quando o objeto for desativado
        if (moveAction != null)
            moveAction.Disable();

        
    }

    void OnEnable()
    {
        // Reabilitar inputs quando o objeto for ativado
        if (moveAction != null)
            moveAction.Enable();

        
    }


    public int GetCurrentMeshIndex() => currentMeshIndex;

    

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.GamePlay;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}