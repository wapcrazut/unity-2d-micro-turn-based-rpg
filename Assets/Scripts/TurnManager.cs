using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Character[] characters;
    [SerializeField] private float nextTurnDelay = 1.0f;

    private int currentCharacterIndex = -1;
    private Character currentCharacter;

    // Events.
    public event UnityAction<Character> OnBeginTurn;
    public event UnityAction<Character> OnEndTurn;

    public static TurnManager Instance;

    public Character GetCurrentCharacter() { return currentCharacter; }

    private void Awake()
    {
        // Define singleton.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } 
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        Character.OnDie += OnCharacterDie;
    }

    private void OnDisable()
    {
        Character.OnDie -= OnCharacterDie;
    }

    private void Start()
    {
        BeginNextTurn();
    }

    public void BeginNextTurn()
    {
        currentCharacterIndex++;

        if (currentCharacterIndex == characters.Length)
        {
            currentCharacterIndex = 0;
        }

        currentCharacter = characters[currentCharacterIndex];
        OnBeginTurn?.Invoke(currentCharacter);
    }

    public void EndTurn()
    {
        OnEndTurn?.Invoke(currentCharacter);
        Invoke(nameof(BeginNextTurn), nextTurnDelay);
    }

    private void OnCharacterDie(Character character)
    {
        if (character.IsPlayer)
        {
            Debug.Log("Game Over");
        }
        else
        {
            Debug.Log("More blood!");
        }
    }
}
