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
    private Animator animator;
    private bool objCompleted = false;

    private void Awake()
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        characterAI = flora.GetComponent<CharacterAI>();
        navMeshAgent = flora.GetComponent<NavMeshAgent>();
        animator = flora.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!objCompleted)
        {
            if (Vector3.Distance(flora.transform.position, roamPosition) < 2f)
            {
                fadeOutPanel.SetActive(true);
                fadeOutPanel.GetComponent<Animation>().Play();
                objCompleted = true;
                StartCoroutine(HandleObjectveFlow());
            }
        }
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
                SetFloraDestination();
            }
        }
    }

    private void CompleteObject7()
    {
        objectiveManager.SetLastCompletedObjective(ObjectiveManager.LastCompletedObjective.OBJ7);
        objectiveManager.SetObjectivePanel("Objective 7 completed", 7);
    }

    private void SetFloraDestination()
    {
        navMeshAgent.enabled = true;
        characterAI.enabled = true;

        characterAI.MoveTo(roamPosition);
        navMeshAgent.speed = 2f;

        animator.SetFloat("Movement", 1f, 0.1f, Time.deltaTime);
    }

    IEnumerator HandleObjectveFlow()
    {
        yield return new WaitForSeconds(2f);
        flora.SetActive(false);
    }
}