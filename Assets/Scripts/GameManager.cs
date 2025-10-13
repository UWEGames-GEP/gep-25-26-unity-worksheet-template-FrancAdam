using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GAMEPLAY,
        PAUSE
    }
    public GameState state;
    bool changed_state = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        state = GameState.GAMEPLAY;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.GAMEPLAY)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                state = GameState.PAUSE;
                changed_state = true;
            }
        }
        else if (state == GameState.PAUSE)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                state = GameState.GAMEPLAY;
                changed_state = true;
            }
        }
    }
    private void LateUpdate()
    {
        // check to see if we have changed game state
        if (changed_state)
        {
            changed_state = false;

            //apply behaviour based on the new game state
            if (state == GameState.GAMEPLAY)
            {
                Time.timeScale = 1.0f;
            }
            else if (state == GameState.PAUSE)
            {
                Time.timeScale = 0.0f;
            }
        }
    }
}
