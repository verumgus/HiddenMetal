using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScanHidden : MonoBehaviour
{
    [SerializeField] private float TimeScan;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0)) {

            if (other.CompareTag("Hidden"))
            {
                MetamorfMesh hidden = other.GetComponent<MetamorfMesh>();
                hidden.HitMe();

                print("Acho");
                Debug.Log("Supeito encontrado");
            }
            else
            {
                print("Nada aqui");
                Debug.Log("Não foi encotrado nada");
            }
        }
        
    }



   
}
