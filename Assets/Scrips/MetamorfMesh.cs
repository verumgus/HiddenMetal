using UnityEngine;
using UnityEngine.InputSystem;

public class MetamorfMesh : MonoBehaviour
{
    [SerializeField] private SwitchPlayer switchPlayer;

    [Header("Mesh variaveis")]
    [SerializeField] private MeshFilter originalMesh;
    [SerializeField] private MeshFilter myMesh;
    [SerializeField] private MeshRenderer myMeshRenderer;
    [SerializeField] private Mesh[] meshList;
    [SerializeField] private Material[] materialList;
    [SerializeField] private GameObject[] prefabList;

    [SerializeField] private bool isHide;

    private InputAction toggleAction;
    private int currentModel = 1;

    public void Start()
    {
        SetupInputSystem();
        originalMesh = myMesh;
        myMeshRenderer = GetComponent<MeshRenderer>();

        // Se tiver prefabs na lista, pega as meshes e materiais deles
        if (prefabList != null && prefabList.Length > 0)
        {
            InitializeFromPrefabs();
        }
        else
        {
            // Mant�m o comportamento original se n�o houver prefabs
            meshList[0] = originalMesh.mesh;
        }
    }

    private void InitializeFromPrefabs()
    {
        meshList = new Mesh[prefabList.Length];
        materialList = new Material[prefabList.Length];

        for (int i = 0; i < prefabList.Length; i++)
        {
            if (prefabList[i] != null)
            {
                MeshFilter prefabMeshFilter = prefabList[i].GetComponent<MeshFilter>();
                MeshRenderer prefabMeshRenderer = prefabList[i].GetComponent<MeshRenderer>();

                if (prefabMeshFilter != null)
                {
                    meshList[i] = prefabMeshFilter.sharedMesh;
                }

                if (prefabMeshRenderer != null)
                {
                    materialList[i] = prefabMeshRenderer.sharedMaterial;
                }
            }
        }
    }

    public void TrocarForma()
    {
        isHide = switchPlayer.isHide;
        if (myMesh == null) { Debug.Log("N�o tem como trocar de mesh"); return; }

        if (isHide)
        {
            myMesh.mesh = meshList[currentModel];

            // Aplica o material se dispon�vel
            if (myMeshRenderer != null && materialList != null && currentModel < materialList.Length && materialList[currentModel] != null)
            {
                myMeshRenderer.material = materialList[currentModel];
            }

            currentModel++;
            if (currentModel >= meshList.Length) { currentModel = 0; }
        }
        else { return; }
    }

    void SetupInputSystem()
    {
        toggleAction = new InputAction("Toggle", InputActionType.Button);
        toggleAction.AddBinding("<Keyboard>/e");
        toggleAction.AddBinding("<Gamepad>/buttonSouth");

        toggleAction.performed += ctx => TrocarForma();
        toggleAction.Enable();
    }
}