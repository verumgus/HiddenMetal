using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Quase isso devo da uma lida no codigo e corrigir ele 
/// </summary>
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

    [Header("Mesh Settings")]
    [SerializeField] private MeshFilter meshFilter; // Refer�ncia para o MeshFilter
    [SerializeField] private Mesh[] alternateMeshes; // Meshes alternativas
    [SerializeField] private bool useAlternateMesh = false; // Estado atual da mesh
    [SerializeField] private int currentMeshIndex = 0; // �ndice da mesh atual

    // Input system
    private Vector2 inputVector;
    private InputAction moveAction;
    private InputAction toggleMeshAction;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;

        // Buscar automaticamente o MeshFilter se n�o estiver configurado
        if (meshFilter == null)
            meshFilter = GetComponent<MeshFilter>();

        SetupInputSystem();

        // Verificar se h� meshes alternativas configuradas
        if (alternateMeshes != null && alternateMeshes.Length > 0)
        {
            Debug.Log("Meshes alternativas dispon�veis: " + alternateMeshes.Length);
        }
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

        // Configura��o da tecla para trocar mesh
        toggleMeshAction = new InputAction("toggleMesh", InputActionType.Button);
        toggleMeshAction.AddBinding("<Keyboard>/e");
        toggleMeshAction.performed += ctx => ToggleObjectMesh();
        toggleMeshAction.Enable();
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
        if (meshFilter == null)
        {
            Debug.LogWarning("MeshFilter n�o encontrado no objeto!");
            return;
        }

        if (alternateMeshes != null && alternateMeshes.Length > 0)
        {
            useAlternateMesh = !useAlternateMesh;

            if (useAlternateMesh)
            {
                // Usa a pr�xima mesh alternativa
                currentMeshIndex = (currentMeshIndex + 1) % alternateMeshes.Length;
                meshFilter.mesh = alternateMeshes[currentMeshIndex];
                Debug.Log("Mesh alterada para: " + alternateMeshes[currentMeshIndex].name + " (�ndice: " + currentMeshIndex + ")");
            }
            else
            {
                // Volta para a mesh original
                meshFilter.mesh = GetOriginalMesh();
                Debug.Log("Mesh original restaurada");
            }
        }
        else
        {
            Debug.LogWarning("Nenhuma mesh alternativa configurada!");
        }
    }

    /// <summary>
    /// Retorna para a mesh original
    /// </summary>
    private Mesh GetOriginalMesh()
    {
        // Se voc� quiser salvar a mesh original, pode armazen�-la no Start
        Mesh originalMesh = meshFilter.mesh;
        currentMeshIndex = 0;
        return originalMesh;
    }

    /// <summary>
    /// M�todo p�blico para trocar para uma mesh espec�fica
    /// </summary>
    public void SetMesh(int meshIndex)
    {
        if (meshFilter != null && alternateMeshes != null && meshIndex >= 0 && meshIndex < alternateMeshes.Length)
        {
            meshFilter.mesh = alternateMeshes[meshIndex];
            currentMeshIndex = meshIndex;
            useAlternateMesh = true;
            Debug.Log("Mesh alterada para: " + alternateMeshes[meshIndex].name);
        }
        else
        {
            Debug.LogWarning("�ndice de mesh inv�lido ou n�o configurado!");
        }
    }

    /// <summary>
    /// M�todo p�blico para adicionar meshes alternativas via c�digo
    /// </summary>
    public void AddAlternateMesh(Mesh newMesh)
    {
        if (alternateMeshes == null)
        {
            alternateMeshes = new Mesh[1] { newMesh };
        }
        else
        {
            System.Array.Resize(ref alternateMeshes, alternateMeshes.Length + 1);
            alternateMeshes[alternateMeshes.Length - 1] = newMesh;
        }

        Debug.Log("Mesh alternativa adicionada. Total: " + alternateMeshes.Length);
    }

    /// <summary>
    /// M�todo para restaurar a mesh original
    /// </summary>
    public void RestoreOriginalMesh()
    {
        if (meshFilter != null)
        {
            meshFilter.mesh = GetOriginalMesh();
            useAlternateMesh = false;
            Debug.Log("Mesh original restaurada");
        }
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

        if (toggleMeshAction != null)
            toggleMeshAction.Disable();
    }

    void OnDisable()
    {
        // Desabilitar inputs quando o objeto for desativado
        if (moveAction != null)
            moveAction.Disable();

        if (toggleMeshAction != null)
            toggleMeshAction.Disable();
    }

    void OnEnable()
    {
        // Reabilitar inputs quando o objeto for ativado
        if (moveAction != null)
            moveAction.Enable();

        if (toggleMeshAction != null)
            toggleMeshAction.Enable();
    }

    /// <summary>
    /// Retorna o �ndice da mesh atual
    /// </summary>
    public int GetCurrentMeshIndex() => currentMeshIndex;

    /// <summary>
    /// Retorna o nome da mesh atual
    /// </summary>
    public string GetCurrentMeshName() => meshFilter != null ? meshFilter.mesh.name : "Nenhuma";
}