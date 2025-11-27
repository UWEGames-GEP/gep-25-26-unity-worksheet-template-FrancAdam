using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCharacterController : ThirdPersonController
{
    public GameManager manager;

    //private void Start()
    //{
    //    manager = FindAnyObjectByType<GameManager>();
    //}
    private void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Paused Game.");
            manager.PauseGame(); // has to be assigned in unity under player armature
        } 
    }
    private void OnRemoveItem(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Remove Item");
            GetComponent<Inventory>().removeItem();
        }
    }
}
