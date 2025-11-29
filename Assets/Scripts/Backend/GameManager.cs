using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject InventoryUI;
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
        InventoryUI.SetActive(false);

        
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
                    Time.timeScale = 1.0f; // time runs

                    InventoryUI.SetActive(false); // turns off inventory ui while game is running
                    Cursor.lockState = CursorLockMode.Locked; //disables mouse while gameplay
                    break;
                
                case GameState.PAUSE:
                    Time.timeScale = 0.0f; // time stops

                    InventoryUI.SetActive(true); // turns on inventory ui when paused
                    Cursor.lockState = CursorLockMode.None; // enables mouse while paused
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
