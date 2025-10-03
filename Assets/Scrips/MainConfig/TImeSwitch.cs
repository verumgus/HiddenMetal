using UnityEngine;
using UnityEngine.UI;

public class TImeSwitch : MonoBehaviour
{
    [Header("Time Configurações")]
    [SerializeField] private Image timerBar;
    [SerializeField] private float maxTime;
    [SerializeField] private float timeleft;
    [Header("Pause TIme Switch")]
    [SerializeField] private float pauseTime;
    [SerializeField] private float maxPauseTime;

    public GameObject timeUpText;



     public SwitchPlayer playerSwitch;



    void Start()
    {
        timeUpText.SetActive(false);
        playerSwitch = GetComponent<SwitchPlayer>();
        timeleft = maxTime;
        pauseTime = maxPauseTime;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameCountTurn();
        
    }

    public void TimesCont(float time, float maxTime)
    {           
        timeleft -= Time.deltaTime;
        
        timerBar.fillAmount = time / maxTime;
    }
    public void GameCountTurn()
    {
        if (timeleft > 0)
        {
            TimesCont(timeleft, maxTime);
        }
        else
        {
            timeUpText.SetActive(true);
            //Time.timeScale = 0;
            timeleft = 0;
            //PauseSwitch();
            Debug.Log("Troca feita");
            Invoke("PauseSwitch",5f);// metodo de pause provisorio

        }
        if (timeleft == 0) {  }

    }

    public void PauseSwitch()
    {   
        playerSwitch.HandleInput();
        timeleft = maxTime;
        timeUpText.SetActive(false);
        

    }
}
