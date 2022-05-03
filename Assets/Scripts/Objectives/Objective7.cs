using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Objective7 : MonoBehaviour
{
    [SerializeField] private GameObject flora;
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private Vector3 roamPosition;

    private ObjectiveManager objectiveManager;
    private CharacterAI characterAI;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        characterAI = flora.GetComponent<CharacterAI>();
        navMeshAgent = flora.GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerCar")
        {
            ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

            if (obj.Equals(ObjectiveManager.LastCompletedObjective.OBJ6))
            {
                fadeOutPanel.SetActive(true);
                fadeOutPanel.GetComponent<Animation>().Play();
		        CompleteObject7();
                StartCoroutine(HandleObjectveFlow());
            }
        }
    }

    private void CompleteObject7()
    {
        objectiveManager.SetLastCompletedObjective(ObjectiveManager.LastCompletedObjective.OBJ7);
        objectiveManager.SetObjectivePanel("Objective 7 completed", 7);
    }

    IEnumerator HandleObjectveFlow()
    {
        yield return new WaitForSeconds(3f);
        navMeshAgent.enabled = false;
        characterAI.enabled = false;
        flora.SetActive(false);
    }
}