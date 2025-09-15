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

    public float SpeedMove => speedMove;
    public float SpeedRot => speedRot;
    public float Gravity => gravity;

    

    // Input system
    private Vector2 inputVector;
    private InputAction moveAction;
    


    [SerializeField] private int currentMeshIndex = 0; // �ndice da mesh atual


    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;

        SetupInputSystem();



    }

    void SetupInputSystem()
    {
        // Configura��o do movimento
        moveAction = new InputAction("move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => inputVector = Vector2.zero;
        moveAction.Enable();

       


    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 movimento = new Vector3(inputVector.x, 0, inputVector.y);
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0f;

        controller.Move(speedMove * Time.fixedDeltaTime * movimento);
        controller.Move(new Vector3(0, gravity, 0) * Time.fixedDeltaTime);

        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.fixedDeltaTime * speedRot);
        }
    }

    /// <summary>
    /// Alterna a mesh do objeto quando a tecla E � pressionada
    /// </summary>
    private void ToggleObjectMesh()
    {

    }

    /// <summary>
    /// Retorna para a mesh original
    /// </summary>
    
        // Se voc� quiser salvar a mesh original, pode armazen�-la no Start
       
    

    /// <summary>
    /// M�todo p�blico para trocar para uma mesh espec�fica
    /// </summary>
    public void SetMesh(int meshIndex)
    {
       
    }

    /// <summary>
    /// M�todo p�blico para adicionar meshes alternativas via c�digo
    /// </summary>
    public void AddAlternateMesh(Mesh newMesh)
    {
       
    }

    /// <summary>
    /// M�todo para restaurar a mesh original
    /// </summary>
    public void RestoreOriginalMesh()
    {
    }

    // M�todos de velocidade
    public void SetSpeedMove(float newSpeed) { speedMove = newSpeed; }
    public void SetSpeedRot(float newSpeedRot) { speedRot = newSpeedRot; }
    public void SetGravity(float newGravity) { gravity = newGravity; }

    void OnDestroy()
    {
        // Limpeza dos inputs
        if (moveAction != null)
            moveAction.Disable();

        
    }

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

    /// <summary>
    /// Retorna o �ndice da mesh atual
    /// </summary>
    public int GetCurrentMeshIndex() => currentMeshIndex;

    /// <summary>
    /// Retorna o nome da mesh atual
    /// </summary>
    //public string GetCurrentMeshName() => meshFilter != null ? meshFilter.mesh.name : "Nenhuma";
}