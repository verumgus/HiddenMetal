using UnityEngine;
using UnityEngine.InputSystem;

public class MetamorfMesh : MonoBehaviour
{

    [SerializeField] private SwitchPlayer switchPlayer;

    [Header("Mesh variaveis")]

    [SerializeField] private MeshFilter originalMesh;
    [SerializeField] private MeshFilter myMesh;
    [SerializeField] private Mesh[] meshList;

    [SerializeField] private bool isHide;


    private InputAction toggleAction;

    private int currentModel = 1;




    public void Start()
    {
        SetupInputSystem();
        originalMesh = myMesh;
        meshList[0] = originalMesh.mesh;
    }






    public void TrocarForma()
    {

        isHide = switchPlayer.isHide;
        if (myMesh == null) { Debug.Log("Não tem como trocar de mesh") ;return; }

        if (isHide)
        {
            myMesh.mesh = meshList[currentModel];
            currentModel++;
            if (currentModel >= meshList.Length) { currentModel = 0; }
        }
        else { return; }

    }

    void SetupInputSystem()
    {
        

        toggleAction = new InputAction("Toggle", InputActionType.Button);
        toggleAction.AddBinding("<Keyboard>/e");
        toggleAction.AddBinding("<Gamepad>/buttonSouth"); // Botão X do gamepad

        toggleAction.performed += ctx => TrocarForma();

        // Habilita as ações

        toggleAction.Enable();


    }
}