using UnityEngine;

public class Cut3Trigger : MonoBehaviour
{
    private GameController gameController;
    
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainAlien")
        {
            gameController.isCut3Triggered = true;
        }
    }
}