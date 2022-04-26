using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject switchUI;
    [SerializeField] GameObject[] doorParts;
 
    private ObjectiveManager objectiveManager;
    private AudioManager audioManager;

    private bool entered = false;
    private bool opened = false;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }
    
    void Update()
    {
        if (entered)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                opened = true;
                PlayAudio("Switch");
                switchUI.SetActive(false);

                for (int i = 0; i < doorParts.Length; i++)
                {
                    doorParts[i].GetComponent<Animation>().Play();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollionInteraction(collision, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        HandleCollionInteraction(collision, false);
    }

    private void HandleCollionInteraction(Collision collision, bool enter)
    {
        ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

        bool obj5Completed = obj.Equals(ObjectiveManager.LastCompletedObjective.OBJ5);

        if (!opened && collision.gameObject.tag == "Player" && obj5Completed)
        {
            entered = enter;
            switchUI.SetActive(enter);
        }
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }
}