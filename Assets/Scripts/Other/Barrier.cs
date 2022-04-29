using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Barrier : MonoBehaviour
{
    [SerializeField] private GameObject flora;
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private GameObject carCam;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 newPlayerPos;
    [SerializeField] private Vector3 newFloraPos;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject car;

    private ObjectiveManager objectiveManager;
    private CharacterAI characterAI;
    private CharacterFloraController characterFloraController;
    private Vector3 targetPosition;
    private ThirdPersonCharacterControl thirdPersonCharacterControl;
    private NavMeshAgent agent;
    private bool carStopped;

    private void Awake()
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        characterFloraController = flora.GetComponent<CharacterFloraController>();
        thirdPersonCharacterControl = player.GetComponent<ThirdPersonCharacterControl>();
        characterAI = flora.GetComponent<CharacterAI>();
        agent = flora.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(carStopped)
        {
            FindTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerCar")
        {
            ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

            if (obj.Equals(ObjectiveManager.LastCompletedObjective.OBJ7))
            {
                fadeOutPanel.SetActive(true);
                fadeOutPanel.GetComponent<Animation>().Play();
                StartCoroutine(HandleFlow());
            }
        }
    }

    private void Chase(float speed)
    {
        flora.transform.LookAt(player.transform);
        targetPosition = player.transform.position + offset;
        characterAI.MoveTo(targetPosition);
        agent.speed = speed;
    }

    private void FindTarget()
    {
        if (Vector3.Distance(flora.transform.position, player.transform.position) <= 2.5f)
        {
            agent.isStopped = true;
            characterFloraController.HandleIdleAnim();
        }
        else if (Vector3.Distance(flora.transform.position, player.transform.position) <= 10f)
        {
            agent.isStopped = false;
            characterFloraController.HandleWalkAnim();
            Chase(1.5f);
        }
        else
        {
            agent.isStopped = false;
            characterFloraController.HandleRunAnim();
            Chase(2f);
        }
    }

    IEnumerator HandleFlow()
    {
        yield return new WaitForSeconds(3f);
        player.SetActive(true);
        player.transform.position = newPlayerPos;
        thirdPersonCharacterControl.IsStoryMode = false;
        flora.SetActive(true);
        characterFloraController.HandleStandAndIdle();
        flora.transform.position = newFloraPos;
        car.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = false;

        yield return new WaitForSeconds(1f);
        carCam.SetActive(false);
        characterFloraController.HandleIdleAnim();

        yield return new WaitForSeconds(2f);
        fadeOutPanel.SetActive(false);
        carStopped = true;
    }
}