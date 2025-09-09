using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterControllerDisabler : MonoBehaviour
{
    [Header("Personagens")]
    [SerializeField] private PlayerController playerObjectS; 
    [SerializeField] private PlayerController playerObjectH;
    //colocar um set true para o seek ou hide ao iniciar o jogo
    [SerializeField] private bool isSeek;
    [SerializeField] private bool isHide;

    [Header("Valores configurações")]
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
            print("O botão foi press");


        }

    }
    // tem como melhorar alem de verificar interação
    public void ChangeHide()
    {
        playerObjectH.SetSpeedMove(noSpeedMove);
        playerObjectH.SetSpeedRot(noSpeedRot);
        //dar valor ao caçador
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