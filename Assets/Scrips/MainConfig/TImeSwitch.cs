using System.Linq;
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

    private bool isPaused = false;

    void Start()
    {
        timeUpText.SetActive(false);
        playerSwitch = GetComponent<SwitchPlayer>();
        timeleft = maxTime;
        pauseTime = maxPauseTime;
        isPaused = false;
    }

    void FixedUpdate()
    {
        GameCountTurn();
    }

    public void TimesCont(float time, float maxTime)
    {
        timeleft -= Time.deltaTime;
        timerBar.fillAmount = time / maxTime;
    }

    public void PauseCount()
    {
        pauseTime -= Time.deltaTime;

        if (pauseTime <= 0)
        {
            pauseTime = 0;
            ResumeGame();
        }
    }

    public void GameCountTurn()
    {
        if (isPaused)
        {
            PauseCount(); // Conta o tempo de pausa
            return;
        }

        if (timeleft > 0)
        {
            TimesCont(timeleft, maxTime); // Tempo normal do jogo
        }
        else
        {
            StartPause(); // Inicia o estado de pausa
        }
    }

    public void StartPause()
    {
        timeUpText.SetActive(true);
        timeleft = 0;
        isPaused = true;
        PauseSystem();
        Debug.Log("Iniciando pausa - Troca em breve");
    }

    public void ResumeGame()
    {
        playerSwitch.HandleInput(); // Executa a troca de personagem
        timeleft = maxTime; // Reseta o timer principal
        pauseTime = maxPauseTime; // Reseta o timer de pausa
        timeUpText.SetActive(false); // Esconde o texto
        isPaused = false; // Volta ao estado normal
        Debug.Log("Pausa finalizada - Jogo retomado");
        PauseSystem(); 
    }

    public void PauseSystem()
    {
        GameState currentGameState = StateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.GamePlay
            ?GameState.Paused
            :GameState.GamePlay;

        StateManager.Instance.SetState(newGameState);
    }
    
}