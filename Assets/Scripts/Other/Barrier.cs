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
    [SerializeField] private GameObject playerMap;
    [SerializeField] private GameObject carMap;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject carMapCamera;
    [SerializeField] private GameObject playerIndicator;

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
        thirdPersonCharacterControl = player.GetComponentInChildren<ThirdPersonCharacterControl>();
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
                car.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = false;
                StartCoroutine(HandleFlow());
            }
        }
    }

    private void Chase(float speed)
    {
        flora.transform.LookAt(player.transform);
        targetPosition = newFloraPos + offset;
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
        flora.transform.position = newFloraPos;
        characterAI.enabled = true;
        agent.enabled = true;

        healthBar.SetActive(true);
        playerMap.SetActive(true);
        playerIndicator.SetActive(true);
        carMap.SetActive(false);
        carMapCamera.SetActive(false);

        yield return new WaitForSeconds(1f);
        characterFloraController.HandleStandAndIdle();
        carCam.SetActive(false);
        characterFloraController.HandleIdleAnim();

        yield return new WaitForSeconds(3f);
        fadeOutPanel.SetActive(false);
        carStopped = true;
    }
}