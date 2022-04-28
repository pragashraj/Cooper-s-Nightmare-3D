using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Objective4 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private GameObject characterFlora;

    private ObjectiveManager objectiveManager;
    private AudioManager audioManager;
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private CharacterAI characterAI;
    private CharacterFloraController characterFloraController;

    private string[] conversations = new string[] {
        "Oh flora i found you",
        "Copper whats going on",
        "I don't know, just come with me"
    };

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        characterAI = characterFlora.GetComponent<CharacterAI>();
        characterFloraController = characterFlora.GetComponent<CharacterFloraController>();
    }

    private void Start()
    {
        agent = characterFlora.GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

        if (obj.Equals(ObjectiveManager.LastCompletedObjective.OBJ4))
        {
            FindTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

            if (obj.Equals(ObjectiveManager.LastCompletedObjective.OBJ3))
            {
                CompleteObject4();
                StartCoroutine(HandleObjectveFlow());
            }
        }
    }

    private void CompleteObject4()
    {
        objectiveManager.SetLastCompletedObjective(ObjectiveManager.LastCompletedObjective.OBJ4);
        objectiveManager.SetObjectivePanel("Objective 4 completed", 4);
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    IEnumerator HandleObjectveFlow()
    {
        for (int i = 0; i < conversations.Length; i++)
        {
            PlayAudio(conversations[i]);
            yield return new WaitForSeconds(2f);
        }
    }

    private void FindTarget()
    {
        if (Vector3.Distance(characterFlora.transform.position, player.transform.position) <= 3f)
        {
            agent.isStopped = true;
            characterFloraController.HandleIdleAnim();
        }
        else if (Vector3.Distance(characterFlora.transform.position, player.transform.position) <= 10f)
        {
            agent.isStopped = false;
            characterFloraController.HandleWalkAnim();
            Chase(walkingSpeed);
        }
        else
        {
            agent.isStopped = false;
            characterFloraController.HandleRunAnim();
            Chase(runningSpeed);
        }
    }

    private void Chase(float speed)
    {
        characterFlora.transform.LookAt(player.transform);
        targetPosition = player.transform.position + offset;
        characterAI.MoveTo(targetPosition);
        agent.speed = speed;
    }
}