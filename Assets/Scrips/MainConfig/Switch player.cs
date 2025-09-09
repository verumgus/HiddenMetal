using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SwitchPlayer : MonoBehaviour
{
    [Header("Personagens")]
    [SerializeField]private CamFollowSwitch camFollowSwitch;

    [SerializeField] private PlayerController playerObjectS;
    
     
    [SerializeField] private PlayerController playerObjectH;

    //colocar um set true para o seek ou hide ao iniciar o jogo
    [SerializeField] public bool isSeek;
    [SerializeField] public bool isHide;

    [Header("Valores configura��es")]
    public float noSpeedMove = 0f;
    public float noSpeedRot = 0f;
    public float moreSpeedMove = 5f;
    public float moreSpeedRot = 10f;


    void Start()
    {
        
        
       
        
           
    }

    void FixedUpdate()
    {
        ChangeSwitch();

    }
    void ChangeSwitch()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isSeek)
            {
                ChangeHide();
                print("Voce se esconde");
            }
            else if (isHide)
            {
                ChangeSeek();
                print("voce preocura");
            }
            print("O bot�o foi press");


        }

    }
    // tem como melhorar alem de verificar intera��o
    public void ChangeHide()
    {
        
        playerObjectH.SetSpeedMove(noSpeedMove);
        playerObjectH.SetSpeedRot(noSpeedRot);
        //dar valor ao ca�ador
        playerObjectS.SetSpeedMove(moreSpeedMove);
        playerObjectS.SetSpeedRot(moreSpeedRot);
        isHide = true;
        isSeek = false;
        

    }
    public void ChangeSeek()
    {
        
        playerObjectS.SetSpeedMove(noSpeedMove);
        playerObjectS.SetSpeedRot(noSpeedRot);
        //Dar valor ao Procurado
        playerObjectH.SetSpeedMove(moreSpeedMove);
        playerObjectH.SetSpeedRot(moreSpeedRot);
        isHide = false;
        isSeek = true;
        

    }

}