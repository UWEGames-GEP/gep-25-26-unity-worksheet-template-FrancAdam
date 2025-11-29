using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GAMEPLAY,
        PAUSE
    }
    public GameState current_game_state;
    bool changed_state = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        current_game_state = GameState.GAMEPLAY;

        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        // check to see if we have changed game current_game_state
        if (changed_state)
        {
            changed_state = false;

            switch (current_game_state)
            {
                case GameState.GAMEPLAY:
                    Time.timeScale = 1.0f;
                    break;
                
                case GameState.PAUSE:
                    Time.timeScale = 0.0f;
                    break;

            }

        }
    }
    public void PauseGame()
    {
        switch (current_game_state)
        {
            case GameState.GAMEPLAY:
                current_game_state = GameState.PAUSE;
                changed_state = true;

                break;

            case GameState.PAUSE:
                current_game_state = GameState.GAMEPLAY;
                changed_state = true;

                break;
        }
    }
 

}
