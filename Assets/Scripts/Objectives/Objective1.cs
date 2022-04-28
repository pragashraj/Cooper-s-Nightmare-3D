using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Objective1 : MonoBehaviour
{
    [SerializeField] private GameObject characterFlora;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private GameObject storyTextPanel;
    [SerializeField] private GameObject cutSceneCamera;
    [SerializeField] private Text storyText;
    [SerializeField] private Vector3 pos;
    [SerializeField] private Vector3 rotation;

    private ObjectiveManager objectiveManager;
    private CharacterFloraController characterFloraController;
    private ThirdPersonCharacterControl thirdPersonCharacterControl;
    private ThirdPersonOrbitCamBasic thirdPersonOrbitCamBasic;
    private CharacterAI characterAI;
    private CharacterMovement characterMovement;

    private List<Conversation> conversations = new List<Conversation>();

    private string[] messages = new string[] {
        "Hey flora, where are you been?",
        "Why cooper? whats up, i came for a morning walk",
        "How do i tell you. i had a really weird dream",
        "Dream ? Are you seroius coop.",
        "yes, thats seem so real, and im worried",
        "Ha ha ha, Dont worry its just dream",
        "Ya, it should be",
        "Ok brought some woods and don't worry about those dream"
    };

    private void Awake()
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        thirdPersonCharacterControl = player.GetComponent<ThirdPersonCharacterControl>();
        thirdPersonOrbitCamBasic = playerCamera.GetComponent<ThirdPersonOrbitCamBasic>();
        characterFloraController = characterFlora.GetComponent<CharacterFloraController>();
        characterMovement = characterFlora.GetComponent<CharacterMovement>();
        characterAI = characterFlora.GetComponent<CharacterAI>();
    }

    private void Start()
    {
        characterMovement.enabled = false;
        characterAI.enabled = false;

        for (int i = 0; i < messages.Length; i++)
        {
            Conversation conversation = new Conversation();
            conversation.speaker = i % 2 == 0 ? "Cooper" : "Flora";
            conversation.message = messages[i];
            conversations.Add(conversation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

            if (obj.Equals(ObjectiveManager.LastCompletedObjective.NONE))
            {
                characterFloraController.HandleStandAndIdle();
                thirdPersonCharacterControl.IsStoryMode = true;
                fadeOutPanel.SetActive(true);
                fadeOutPanel.GetComponent<Animation>().Play();
                thirdPersonOrbitCamBasic.enabled = false;
                StartCoroutine(HandleObjectveFlow());
            }
        }
    }

    private void CompleteObject1()
    {
        objectiveManager.SetLastCompletedObjective(ObjectiveManager.LastCompletedObjective.OBJ1);
        objectiveManager.SetObjectivePanel("Objective 1 completed", 1);
        characterFloraController.HandleWalkAnim();
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
        yield return new WaitForSeconds(3f);
        characterFlora.transform.position = pos;
        characterFlora.transform.Rotate(rotation);
        cutSceneCamera.SetActive(true);
        yield return new WaitForSeconds(2f);
        storyTextPanel.SetActive(true);

        for (int i = 0; i < conversations.Count; i++)
        {
            SetConversation(i);
            yield return new WaitForSeconds(4f);
        }

        thirdPersonOrbitCamBasic.enabled = true;
        fadeOutPanel.SetActive(false);
        storyTextPanel.SetActive(false);
        cutSceneCamera.SetActive(false);
        thirdPersonCharacterControl.IsStoryMode = false;
        CompleteObject1();
        characterMovement.enabled = true;
        characterAI.enabled = true;
    }
}