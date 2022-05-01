using UnityEngine.AI;
using UnityEngine;

public class RandomCharacterMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private CharacterAI characterAI;
    private bool walking = false;

    private void Awake()
    {
        characterAI = gameObject.GetComponent<CharacterAI>();
    }

    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = characterAI.GetRoamingPosition(startingPosition);
    }

    private void Update()
    {
        if (!walking)
        {
            HandleWalk();
        }

        if (Vector3.Distance(transform.position, roamPosition) < 1f)
        {
            walking = false;
        }
    }

    private void HandleWalk()
    {
        walking = true;
        roamPosition = characterAI.GetRoamingPosition(startingPosition);
        characterAI.MoveTo(roamPosition);
    }
}