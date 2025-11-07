using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode; // ADICIONE ESTA LINHA

public class PlayerController : NetworkBehaviour // MUDE MonoBehaviour para NetworkBehaviour
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
        // ADICIONE ESTA VERIFICA플O
        if (!IsOwner) return;

        direction = context.ReadValue<Vector2>();
    }

    public void Pular(InputAction.CallbackContext context)
    {
        // ADICIONE ESTA VERIFICA플O
        if (!IsOwner) return;

        if (context.performed && canJump)
        {
            //pula
            canJump = false;
        }
    }

    void Start()
    {
        // ADICIONE ESTA VERIFICA플O
        if (!IsOwner) return;

        myCamera = Camera.main.transform;
    }

    void FixedUpdate()
    {
        // ADICIONE ESTA VERIFICA플O
        if (!IsOwner) return;

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
        // ADICIONE ESTA VERIFICA플O (opcional, mas recomendado)
        if (!IsOwner) return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}