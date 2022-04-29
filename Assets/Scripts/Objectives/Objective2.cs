using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Objective2 : MonoBehaviour
{
    [SerializeField] private GameObject aliensRoot;
    [SerializeField] private GameObject particlesRoot;
    [SerializeField] private GameObject cutCam5;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private GameObject characterFlora;
    [SerializeField] private GameObject characters;
    [SerializeField] private Vector3 newPos;
    [SerializeField] private Material skybox;
    [SerializeField] private GameObject directLight;
    [SerializeField] private GameObject damagedVehicles;

    private ObjectiveManager objectiveManager;
    private ThirdPersonCharacterControl thirdPersonCharacterControl;
    private ThirdPersonOrbitCamBasic thirdPersonOrbitCamBasic;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        thirdPersonCharacterControl = player.GetComponent<ThirdPersonCharacterControl>();
        thirdPersonOrbitCamBasic = playerCamera.GetComponent<ThirdPersonOrbitCamBasic>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

            if (obj.Equals(ObjectiveManager.LastCompletedObjective.OBJ1))
            {
                characterFlora.GetComponent<NavMeshAgent>().enabled = false;
                characterFlora.GetComponent<CharacterAI>().enabled = false;
                characterFlora.SetActive(false);
                thirdPersonCharacterControl.IsStoryMode = true;
                aliensRoot.SetActive(true);
                particlesRoot.SetActive(true);
                fadeOutPanel.SetActive(true);
                fadeOutPanel.GetComponent<Animation>().Play();
                thirdPersonOrbitCamBasic.enabled = false;
                RenderSettings.skybox = skybox;
                StartCoroutine(HandleObjectveFlow());
            }
        }
    }

    private void CompleteObject2()
    {
        objectiveManager.SetLastCompletedObjective(ObjectiveManager.LastCompletedObjective.OBJ2);
        objectiveManager.SetObjectivePanel("Objective 2 completed", 2);
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    IEnumerator HandleObjectveFlow()
    {
        yield return new WaitForSeconds(2.5f);
        PlayAudio("Explosion");
        directLight.SetActive(false);
        damagedVehicles.SetActive(true);
	characters.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        cutCam5.SetActive(true);

        yield return new WaitForSeconds(1f);
        PlayAudio("WhatsGoingOn");

        yield return new WaitForSeconds(8f);
        CompleteObject2();
        thirdPersonOrbitCamBasic.enabled = true;
        fadeOutPanel.SetActive(false);
        cutCam5.SetActive(false);
        thirdPersonCharacterControl.IsStoryMode = false;
        damagedVehicles.SetActive(true);
        objectiveManager.PlayThemeForAliens();
    }
}