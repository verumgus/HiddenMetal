using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private PlayerController playerObjectS; // Seek player
    [SerializeField] private PlayerController playerObjectH; // Hide player
     public CamFollowSwitch cameraController;

    [Header("Initial States")]
    [SerializeField] private bool startAsSeeker = true;
     public bool isSeek;
     public bool isHide;

    [Header("Speed Settings")]
    public float noSpeedMove = 0f;
    public float noSpeedRot = 0f;
    public float moreSpeedMove = 5f;
    public float moreSpeedRot = 10f;

    [Header("Input Settings")]
    [SerializeField] private KeyCode switchKey = KeyCode.X;

    void Start()
    {
        InitializePlayers();
        SetupInitialState();
    }

    void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// Verifica e valida todas as refer�ncias necess�rias
    /// </summary>
    private void InitializePlayers()
    {
        // Buscar automaticamente se n�o estiver configurado
        if (cameraController == null)
            cameraController = Object.FindAnyObjectByType<CamFollowSwitch>();

        if (playerObjectS == null)
            Debug.LogWarning("PlayerObjectS (Seek) n�o est� configurado!");

        if (playerObjectH == null)
            Debug.LogWarning("PlayerObjectH (Hide) n�o est� configurado!");

        ValidateReferences();
    }

    /// <summary>
    /// Configura o estado inicial baseado na escolha do inspetor
    /// </summary>
    private void SetupInitialState()
    {
        if (startAsSeeker)
        {
            SetSeekerState();
        }
        else
        {
            SetHiderState();
        }
    }

    /// <summary>
    /// Gerencia o input de troca de personagem
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown(switchKey))
        {
            SwitchCharacter();
        }
    }

    /// <summary>
    /// Troca entre os personagens baseado no estado atual
    /// </summary>
    private void SwitchCharacter()
    {
        if (isSeek)
        {
            SetHiderState();
            Debug.Log("Agora voc� est� se escondendo");
        }
        else if (isHide)
        {
            SetSeekerState();
            Debug.Log("Agora voc� est� procurando");
        }
    }

    /// <summary>
    /// Configura o estado de procurador (Seeker)
    /// </summary>
    public void SetSeekerState()
    {
        if (!ValidateReferences()) return;

        // Configura velocidades
        SetPlayerSpeed(playerObjectH, noSpeedMove, noSpeedRot);  // Hider fica parado
        SetPlayerSpeed(playerObjectS, moreSpeedMove, moreSpeedRot); // Seeker se move

        // Atualiza estados
        isSeek = true;
        isHide = false;

        // Configura c�mera
        cameraController.SetSeeking(true);
        cameraController.SetHiding(false);
    }

    /// <summary>
    /// Configura o estado de escondedor (Hider)
    /// </summary>
    public void SetHiderState()
    {
        if (!ValidateReferences()) return;

        // Configura velocidades
        SetPlayerSpeed(playerObjectS, noSpeedMove, noSpeedRot);  // Seeker fica parado
        SetPlayerSpeed(playerObjectH, moreSpeedMove, moreSpeedRot); // Hider se move

        // Atualiza estados
        isSeek = false;
        isHide = true;

        // Configura c�mera
        cameraController.SetSeeking(false);
        cameraController.SetHiding(true);
    }

    /// <summary>
    /// Define a velocidade de um jogador
    /// </summary>
    private void SetPlayerSpeed(PlayerController player, float moveSpeed, float rotationSpeed)
    {
        if (player != null)
        {
            player.SetSpeedMove(moveSpeed);
            player.SetSpeedRot(rotationSpeed);
        }
    }

    /// <summary>
    /// Valida se todas as refer�ncias necess�rias est�o configuradas
    /// </summary>
    private bool ValidateReferences()
    {
        bool allValid = true;

        if (playerObjectS == null)
        {
            Debug.LogError("PlayerObjectS (Seek) n�o est� configurado!");
            allValid = false;
        }

        if (playerObjectH == null)
        {
            Debug.LogError("PlayerObjectH (Hide) n�o est� configurado!");
            allValid = false;
        }

        if (cameraController == null)
        {
            Debug.LogError("CameraController n�o est� configurado!");
            allValid = false;
        }

        return allValid;
    }

    // M�todos p�blicos para acesso externo
    public PlayerController GetCurrentActivePlayer()
    {
        return isSeek ? playerObjectS : playerObjectH;
    }

    public bool IsSeeking() => isSeek;
    public bool IsHiding() => isHide;
}