using UnityEngine;
using UnityEngine.TextCore.Text;

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

    void Start()
    {
        InitializePlayers();
        SetupInitialState();
    }

    private void InitializePlayers()
    {
        // Buscar automaticamente se não estiver configurado
        if (cameraController == null)
            cameraController = Object.FindAnyObjectByType<CamFollowSwitch>();

        if (playerObjectS == null)
            Debug.LogWarning("PlayerObjectS (Seek) não está configurado!");

        if (playerObjectH == null)
            Debug.LogWarning("PlayerObjectH (Hide) não está configurado!");

        
    }

    
    private void SetupInitialState()
    {
        playerObjectS.enabled = true;
        playerObjectH.enabled = false;
        if (startAsSeeker)
        {
            SetSeekerState();
            
        }
        else
        {
            SetHiderState();
            
        }
    }

    public void HandleInput()
    {   
        SwitchCharacter();
    }

 
    private void SwitchCharacter()
    {
        
        if (isSeek)
        {
            SetHiderState();
            Debug.Log("Agora você está se escondendo");
            playerObjectS.enabled = false;
            playerObjectH.enabled = true;
        }
        else if (isHide)
        {
            SetSeekerState();
            Debug.Log("Agora você está procurando");
            playerObjectS.enabled = true;
            playerObjectH.enabled = false;
        }
    }


    public void SetSeekerState()
    {
        
        // Atualiza estados
        isSeek = true;
        isHide = false;

        // Configura câmera
        cameraController.SetSeeking(true);
        cameraController.SetHiding(false);
    }

 
    public void SetHiderState()
    {
        

        // Atualiza estados
        isSeek = false;
        isHide = true;

        // Configura câmera
        cameraController.SetSeeking(false);
        cameraController.SetHiding(true);
        
    }

    public void Timewait()
    {
        playerObjectS.enabled = false;
        playerObjectH.enabled = false;
    }

    // Métodos públicos para acesso externo
   /* public PlayerController GetCurrentActivePlayer()
    {
        return isSeek ? playerObjectS : playerObjectH;
    }*/

   
}