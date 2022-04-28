using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Objective6 : MonoBehaviour
{
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private GameObject cutSceneCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyRobot;
    [SerializeField] private Vector3 roamPosition;
    [SerializeField] private GameObject car;

    private ObjectiveManager objectiveManager;
    private AudioManager audioManager;
    private ThirdPersonCharacterControl thirdPersonCharacterControl;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        thirdPersonCharacterControl = player.GetComponent<ThirdPersonCharacterControl>();
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
        cutSceneCamera.SetActive(true);
        enemyRobot.SetActive(true);
        PlayAudio("RobotMovement");
        SetSpiderBotDestination();
        player.SetActive(false);
	car.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = true;

        yield return new WaitForSeconds(3f);
        cutSceneCamera.GetComponent<Animation>().Play();
        fadeOutPanel.SetActive(false);

        yield return new WaitForSeconds(2f);
        CompleteObject6();
    }

    private void SetSpiderBotDestination()
    {
        EnemyAI enemyAI = enemyRobot.GetComponent<EnemyAI>();

        enemyAI.MoveTo(roamPosition);
        enemyRobot.GetComponent<NavMeshAgent>().speed = 3f;
    }
}