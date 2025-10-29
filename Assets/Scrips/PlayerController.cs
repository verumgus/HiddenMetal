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


    void Awake()
    {

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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}