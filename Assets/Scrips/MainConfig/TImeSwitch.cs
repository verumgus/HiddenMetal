using UnityEngine;
using UnityEngine.UI;

public class TImeSwitch : MonoBehaviour
{
    [Header("Time Configurações")]
    [SerializeField] private Image timerBar;
    [SerializeField] private float maxTime;
    [SerializeField] private float timeleft;
    public GameObject timeUpText;

     SwitchPlayer playerSwitch;



    void Start()
    {
        timeUpText.SetActive(false);
        playerSwitch = GetComponent<SwitchPlayer>();
        timeleft = maxTime;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timeleft > 0)
        {
            timeleft -= Time.deltaTime;
            timerBar.fillAmount = timeleft/maxTime;
        }
        else
        {
            timeUpText.SetActive(true);
            Time.timeScale = 0;
            timeleft = 0;
            
        }
        if(timeleft == 0) { playerSwitch.HandleInput(); }
        
    }
}
