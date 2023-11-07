using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UIElements;

/*
 * This script done mainly by Rémi Dijoux allows to instantiate characters, spawn them on the map and handle the
 * the turned-based aspect of the game.
 * Adrian Wisniewski worked on spawn points and put a third parameter to GenerateCharacters(),
 * he later added the pathfinding done by Thomas Decreus.
 */

public class GameManager : MonoBehaviour
{
    public playMenu playMenu;
    public spawnPosition spawnPosition;

    public TextMeshProUGUI endTurnText;

    private List<CharacterBehaviour> playerCharacters = new List<CharacterBehaviour>();
    private List<CharacterBehaviour> enemyCharacters = new List<CharacterBehaviour>();

    private int activeCharacterIndex = 0;
    private bool playerTurn = true;
    private bool hasMoved = false;
    private bool canPressSpace = false;
    public Transform planeTransform;
    private float planeSize = 10f;
    private bool hasAttacked = false; // Nouveau booléen pour vérifier si une attaque a eu lieu
    private bool endOfCharacterTurn = false; // Pour vérifier si le personnage actif a terminé son tour

    public bool gameStarted = false;


    void Start()
    {
        if (gameStarted)
        {
            if (!planeTransform)
            {
                Debug.LogError("Plane Transform is not assigned!");
                return;
            }

            GenerateCharacters(playMenu.charactersPrefabs, playerCharacters,spawnPosition.charactersSpawnPoint);
            GenerateCharacters(playMenu.charactersPrefabs, enemyCharacters, spawnPosition.EnemiesSpawnPoint);
        }
    }

    void Update()
    {
        if (gameStarted)
        {
            if (playerTurn)
            {
                HandlePlayerTurn();
            }
            else
            {
                // rien ici car nous avons déjà appelé EnemyTurn dans HandlePlayerTurn
            }
        }
    }

    void HandlePlayerTurn()
    {
        CharacterBehaviour activeCharacter = playerCharacters[activeCharacterIndex];

        if (Input.GetMouseButtonDown(0) && !hasMoved)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                hasMoved = true;
                Vector3 moveToPoint = Vector3.MoveTowards(activeCharacter.transform.position, hit.point, activeCharacter.maxMoveDistance);
                StartCoroutine(activeCharacter.MoveTowards(moveToPoint, playerTurn));
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !activeCharacter.IsWalking)
        {
            if (!hasAttacked)
            {
                Attack();
                hasAttacked = true;
            }
            else
            {
                EndPlayerCharacterTurn();
            }
        }
    }

    void EndPlayerCharacterTurn()
    {
        hasMoved = false;
        hasAttacked = false;

        if (activeCharacterIndex < playerCharacters.Count - 1)
        {
            activeCharacterIndex++;
        }
        else
        {
            activeCharacterIndex = 0;
            playerTurn = false;
            StartCoroutine(EnemyTurn());
        }
    }

    public void Attack()
    {
        StartCoroutine(AttackSequence());
    }

    private IEnumerator AttackSequence()
    {
        CharacterBehaviour activeCharacter = playerCharacters[activeCharacterIndex];
        if (activeCharacter.IsDead)
        {
            yield break;
        }

        bool hasTargetedEnemy = false; // Booléen local pour vérifier si un ennemi a été ciblé

        foreach (var enemy in enemyCharacters.ToArray())
        {
            if (hasTargetedEnemy) // Sortir de la boucle si un ennemi a été ciblé et attaqué
            {
                break;
            }

            if (enemy.IsDead)
            {
                continue;
            }
            float distanceToEnemy = Vector3.Distance(activeCharacter.transform.position, enemy.transform.position);
            if (distanceToEnemy <= activeCharacter.attackRange)
            {
                hasTargetedEnemy = true; // Marquez qu'un ennemi a été ciblé
                yield return StartCoroutine(activeCharacter.AttackMove(enemy.transform));
                enemy.ReceiveDamage(activeCharacter.attackPoints);

                canPressSpace = true;

                yield return new WaitForSeconds(2.0f);
            }
        }

        yield return ShowEndTurnText();
        EndPlayerCharacterTurn();
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2.0f);

        foreach (var enemy in enemyCharacters.ToArray())
        {
            if (enemy.IsDead)
            {
                continue;
            }

            CharacterBehaviour closestPlayer = GetClosestPlayer(enemy);

            if (!closestPlayer)
                continue;

            float distanceToPlayer = Vector3.Distance(enemy.transform.position, closestPlayer.transform.position);
            if (distanceToPlayer > enemy.attackRange)
            {
                yield return StartCoroutine(enemy.MoveTowards(closestPlayer.transform.position,playerTurn));
                distanceToPlayer = Vector3.Distance(enemy.transform.position, closestPlayer.transform.position);
            }

            if (distanceToPlayer <= enemy.attackRange)
            {
                yield return StartCoroutine(enemy.AttackMove(closestPlayer.transform));
                closestPlayer.ReceiveDamage(enemy.attackPoints);
                yield return new WaitForSeconds(2.0f);
            }
        }

        yield return ShowEndTurnText();

        activeCharacterIndex = 0;
        playerTurn = true;
        hasMoved = false;
        hasAttacked = false;
    }

    CharacterBehaviour GetClosestPlayer(CharacterBehaviour enemy)
    {
        float minDistance = float.MaxValue;
        CharacterBehaviour closestPlayer = null;

        foreach (var player in playerCharacters)
        {
            if (player.IsDead)
            {
                continue;
            }

            float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPlayer = player;
            }
        }
        return closestPlayer;
    }




    private IEnumerator ShowEndTurnText()
    {
        if (!endTurnText)
        {
            yield break;
        }

        endTurnText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        endTurnText.gameObject.SetActive(false);
    }

    
    void GenerateCharacters(List<GameObject> prefabsList, List<CharacterBehaviour> charactersList, List<GameObject> positionsList)
    {
        if (prefabsList.Count == 0 || positionsList.Count == 0)
        {
            Debug.LogError("The prefabsList and positions lists must not be empty.");
            return;
        }

        int minCount = Mathf.Min(prefabsList.Count, positionsList.Count);

        for (int i = 0; i < minCount; i++)
        {
            GameObject prefab = prefabsList[i];
            GameObject position = positionsList[i];

            GameObject instance = Instantiate(prefab, position.transform.position, position.transform.rotation);
            CharacterBehaviour charBehaviour = instance.GetComponent<CharacterBehaviour>();
            charactersList.Add(charBehaviour);
        }
    }
    

    /*
     * At the beginning, to test the functionality of our game, characters randomly spawned on a small scene.
     * This was removed once we implemented scripts and methods to spwan on a given position a number of characters.
     
    Vector3 GetRandomPositionOnPlane()
    {
        float halfSize = planeSize * 0.5f;
        float x = Random.Range(-halfSize, halfSize);
        float z = Random.Range(-halfSize, halfSize);
        Vector3 position = new Vector3(x, 0, z) + planeTransform.position;  // Assurez-vous que le plane est à la position y = 0

        return position;
    }
    */

    public void OnDeath(CharacterBehaviour character)
    {
        // Désactiver le personnage plutôt que de le détruire
        character.gameObject.SetActive(false);

        // Retirer le personnage de la liste appropriée
        if (playerCharacters.Contains(character))
        {
            playerCharacters.Remove(character);
        }
        else if (enemyCharacters.Contains(character))
        {
            enemyCharacters.Remove(character);
        }
    }

}