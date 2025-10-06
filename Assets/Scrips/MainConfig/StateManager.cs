using UnityEngine;

public class StateManager
{
    private static StateManager _instance;
    

    public static StateManager Instance
    {
        get
        {
            if(_instance == null)
                _instance = new StateManager();
            return _instance;
        }

    }
    public GameState CurrentGameState { get; private set; } 

    public delegate void StateChangedHandler(GameState newGameState);
    public event StateChangedHandler OnGameStateChanged;

    private StateManager()
    {

    }

    public void SetState(GameState newGameState) 
    {
        if(newGameState == CurrentGameState) 
            return;
        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }
}
