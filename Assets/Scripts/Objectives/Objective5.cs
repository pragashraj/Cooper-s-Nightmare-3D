using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Objective5 : MonoBehaviour
{
    [SerializeField] private GameObject flora;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject storyTextPanel;
    [SerializeField] private Text storyText;
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private Vector3 pos;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private GameObject cutSceneCamera;

    private ObjectiveManager objectiveManager;
    private AudioManager audioManager;
    private NavMeshAgent agent;
    private ThirdPersonCharacterControl thirdPersonCharacterControl;
    private CharacterFloraController characterFloraController;

    private List<Conversation> conversations = new List<Conversation>();

    private string[] messages = new string[] {
        "There are few people hiding",
        "There is no time for that",
        "But..",
        "No there are no one around here, all of are dead"
    };

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        thirdPersonCharacterControl = player.GetComponent<ThirdPersonCharacterControl>();
        characterFloraController = flora.GetComponent<CharacterFloraController>();
    }


    private void Start()
    {
        agent = flora.GetComponent<NavMeshAgent>();

        for (int i = 0; i < messages.Length; i++)
        {
            Conversation conversation = new Conversation();
            conversation.speaker = i % 2 != 0 ? "Cooper" : "Flora";
            conversation.message = messages[i];
            conversations.Add(conversation);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

            if (obj.Equals(ObjectiveManager.LastCompletedObjective.OBJ4))
            {
                PlayAudio("Stay here");
                fadeOutPanel.SetActive(true);
                fadeOutPanel.GetComponent<Animation>().Play();
                flora.GetComponent<NavMeshAgent>().enabled = false;
                flora.GetComponent<CharacterAI>().enabled = false;
                flora.SetActive(false);
                thirdPersonCharacterControl.IsStoryMode = true;
                StartCoroutine(HandleObjectveFlow());
            }
        }
    }


    private void CompleteObject5()
    {
        objectiveManager.SetLastCompletedObjective(ObjectiveManager.LastCompletedObjective.OBJ5);
        objectiveManager.SetObjectivePanel("Objective 5 completed", 5);
    }


    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    private void SetConversation(int i)
    {
        Conversation conversation = conversations[i];
        if (conversation != null)
        {
            storyText.text = conversation.speaker + " : " + conversation.message;
        }
    }

    IEnumerator HandleObjectveFlow()
    {
        yield return new WaitForSeconds(2f);
        flora.transform.position = pos;
        flora.transform.Rotate(rotation);
        flora.SetActive(true);
        characterFloraController.HandleStandAndIdle();
        yield return new WaitForSeconds(2f);
        characterFloraController.HandleIdleAnim();
        flora.GetComponent<CharacterMovement>().enabled = false;
        cutSceneCamera.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        storyTextPanel.SetActive(true);

        for (int i = 0; i < conversations.Count; i++)
        {
            SetConversation(i);
            yield return new WaitForSeconds(3f);
        }

        thirdPersonCharacterControl.IsStoryMode = false;
        fadeOutPanel.SetActive(false);
        storyTextPanel.SetActive(false);
        cutSceneCamera.SetActive(false);
        CompleteObject5();
    }
}