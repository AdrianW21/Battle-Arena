using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/*
 * This script done by Rémi Dijoux gives instructions on how characters have to behave.
 * Some conditions were added by Adrian Wisniewski to the script following the restructuring
 * of the project.
 */

public class CharacterBehaviour : MonoBehaviour
{
    public float maxMoveDistance = 5f;
    public float attackRange = 2f;
    public int attackPoints = 10;
    public GameObject attackEffect;
    public float moveSpeed = 2f;
    public float rotateSpeed = 5f;
    public LayerMask enemyLayer;
    public int maxHealth = 100;
    private Vector3 initialPosition;
    private bool isWalking = false;
    private bool isAttacking = false;
    private Animator characterAnimator;
    public float attackEffectDelay = 0.5f;
    public bool isAlive = true;
    public bool IsDead { get; private set; } = false;

    private NavMeshAgent agent;

    public bool IsWalking
    {
        get { return isWalking; }
    }

    private Vector3 destinationPoint;
    private int currentHealth;

    void Start()
    {
        initialPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        characterAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isWalking)
        {
            float distanceToDestination = Vector3.Distance(transform.position, destinationPoint);
            if (distanceToDestination <= 0.1f)
            {
                isWalking = false;
                characterAnimator.SetBool("isWalking", false);
            }
        }

        if (isAttacking)
        {
            
        }
    }

    public IEnumerator MoveTowards(Vector3 targetPoint, bool playerTurn)
    {
        isWalking = true;
        characterAnimator.SetBool("isWalking", true);
        destinationPoint = targetPoint;

        Vector3 startPosition = transform.position;
        Vector3 direction = (destinationPoint - startPosition).normalized;
        float journeyLength = Vector3.Distance(startPosition, destinationPoint);
        float distanceToCheck = Mathf.Min(journeyLength, maxMoveDistance);

        RaycastHit hit;
        
        // Now, start moving towards the destination
        float startTime = Time.time;
        float fractionOfJourney = 0;
        agent.isStopped = false;
        if (playerTurn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                    while (fractionOfJourney < 1)
                    {

                        float distanceCovered = (Time.time - startTime) * moveSpeed;
                        fractionOfJourney = distanceCovered / journeyLength;

                        if (Vector3.Distance(transform.position, startPosition) > maxMoveDistance)
                        {
                            agent.isStopped = true;
                            break;
                        }

                        yield return null;
                    }
                }
            }
        }
        else
        {
            agent.SetDestination(destinationPoint);
            while (fractionOfJourney < 1)
            {

                float distanceCovered = (Time.time - startTime) * moveSpeed;
                fractionOfJourney = distanceCovered / journeyLength;

                if (Vector3.Distance(transform.position, startPosition) > maxMoveDistance)
                {
                    agent.isStopped = true;
                    break;
                }

                yield return null;
            }
        }
        

        initialPosition = transform.position;
        isWalking = false;
        characterAnimator.SetBool("isWalking", false);
    }



    public IEnumerator AttackMove(Transform targetTransform)
    {
        if (!isAlive) yield break;
        isAttacking = true;
        characterAnimator.SetBool("isAttacking", true);

        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation((targetTransform.position - transform.position).normalized);
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * rotateSpeed;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            yield return null;
        }

        if (attackEffect)
        {
            yield return new WaitForSeconds(attackEffectDelay);

            Vector3 effectPosition = targetTransform.position + new Vector3(0, 1f, 0);
            var effect = Instantiate(attackEffect, effectPosition, Quaternion.identity);

            float scaleFactor = 3f;
            effect.transform.localScale *= scaleFactor;

            targetTransform.gameObject.GetComponent<Animator>().SetTrigger("isHit");
            Destroy(effect, 2.0f);
        }

        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
        characterAnimator.SetBool("isAttacking", false);
    }

    IEnumerator ResetIsHit()
    {
        yield return new WaitForSeconds(1);
        characterAnimator.ResetTrigger("isHit");
    }

    IEnumerator DestroyAfterDeathAnimation(float animationDuration)
    {
        yield return new WaitForSeconds(animationDuration);
        Destroy(gameObject);
    }

    public void ReceiveDamage(int damagePoints)
    {
        currentHealth -= damagePoints;
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            IsDead = true;
            characterAnimator.SetBool("isDead", true);
            StartCoroutine(DestroyAfterDeathAnimation(3.5f));
        }
        else
        {
            characterAnimator.SetTrigger("isHit");
            StartCoroutine(ResetIsHit());
        }
    }
}