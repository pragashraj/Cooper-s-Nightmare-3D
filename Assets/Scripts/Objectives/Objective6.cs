using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Objective6 : MonoBehaviour
{
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private GameObject carCamera;
    [SerializeField] private GameObject playerRoot;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyRobot;
    [SerializeField] private Vector3 roamPosition;
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject[] doorParts;
    [SerializeField] private GameObject playerMap;
    [SerializeField] private GameObject carMap;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject playerIndicator;
    [SerializeField] private GameObject carIndicator;
    [SerializeField] private GameObject carMapCamera;
    [SerializeField] private GameObject flora;

    private ObjectiveManager objectiveManager;
    private AudioManager audioManager;
    private ThirdPersonCharacterControl thirdPersonCharacterControl;
    private CharacterAI characterAI;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        thirdPersonCharacterControl = player.GetComponent<ThirdPersonCharacterControl>();
        characterAI = flora.GetComponent<CharacterAI>();
        navMeshAgent = flora.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Vector3.Distance(enemyRobot.transform.position, roamPosition) < 2f)
        {
            enemyRobot.GetComponent<EnemyController>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

            if (obj.Equals(ObjectiveManager.LastCompletedObjective.OBJ5))
            {
                fadeOutPanel.SetActive(true);
                fadeOutPanel.GetComponent<Animation>().Play();
                thirdPersonCharacterControl.IsStoryMode = true;
                SetFloraDestination();
                StartCoroutine(HandleObjectveFlow());
            }
        }
    }

    private void CompleteObject6()
    {
        objectiveManager.SetLastCompletedObjective(ObjectiveManager.LastCompletedObjective.OBJ6);
        objectiveManager.SetObjectivePanel("Objective 6 completed", 6);
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    IEnumerator HandleObjectveFlow()
    {
        yield return new WaitForSeconds(3f);
        carCamera.SetActive(true);
        enemyRobot.SetActive(true);
        PlayAudio("RobotMovement");
        SetSpiderBotDestination();
        playerRoot.SetActive(false);
	    healthBar.SetActive(false);
        playerMap.SetActive(false);
        carMap.SetActive(true);
        carMapCamera.SetActive(true);
        playerIndicator.SetActive(false);
	    carIndicator.SetActive(true);
	
	    for(int i = 0; i < doorParts.Length; i++) {
	         doorParts[i].SetActive(false);
	    }

        yield return new WaitForSeconds(2f);
        fadeOutPanel.SetActive(false);
        CompleteObject6();
        car.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = true;
    }

    private void SetSpiderBotDestination()
    {
        EnemyAI enemyAI = enemyRobot.GetComponent<EnemyAI>();

        enemyAI.MoveTo(roamPosition);
        enemyRobot.GetComponent<NavMeshAgent>().speed = 3f;
    }

    private void SetFloraDestination()
    {
        navMeshAgent.enabled = true;
        characterAI.enabled = true;

        characterAI.MoveTo(roamPosition);
        navMeshAgent.speed = 4f;

        flora.GetComponent<Animator>().SetFloat("Movement", 1f, 0.1f, Time.deltaTime);
    }
}