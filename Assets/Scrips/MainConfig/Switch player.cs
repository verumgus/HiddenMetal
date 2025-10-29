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
        // Buscar automaticamente se n�o estiver configurado
        if (cameraController == null)
            cameraController = Object.FindAnyObjectByType<CamFollowSwitch>();

        if (playerObjectS == null)
            Debug.LogWarning("PlayerObjectS (Seek) n�o est� configurado!");

        if (playerObjectH == null)
            Debug.LogWarning("PlayerObjectH (Hide) n�o est� configurado!");

        
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
            Debug.Log("Agora voc� est� se escondendo");
            playerObjectS.enabled = false;
            playerObjectH.enabled = true;
        }
        else if (isHide)
        {
            SetSeekerState();
            Debug.Log("Agora voc� est� procurando");
            playerObjectS.enabled = true;
            playerObjectH.enabled = false;
        }
    }


    public void SetSeekerState()
    {
        
        // Atualiza estados
        isSeek = true;
        isHide = false;

        // Configura c�mera
        cameraController.SetSeeking(true);
        cameraController.SetHiding(false);
    }

 
    public void SetHiderState()
    {
        

        // Atualiza estados
        isSeek = false;
        isHide = true;

        // Configura c�mera
        cameraController.SetSeeking(false);
        cameraController.SetHiding(true);
        
    }

    public void Timewait()
    {
        playerObjectS.enabled = false;
        playerObjectH.enabled = false;
    }

    // M�todos p�blicos para acesso externo
   /* public PlayerController GetCurrentActivePlayer()
    {
        return isSeek ? playerObjectS : playerObjectH;
    }*/

   
}