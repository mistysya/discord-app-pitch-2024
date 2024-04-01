using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    // My player
    public GameObject MyPlayer;
    // Other players
    public Dictionary<string, GameObject> Players = new();
    // Prefab
    public GameObject PlayerPrefab;
    private ConnectionManager _connectionManager;

    public async void Start()
    {
        // Initialize game room.
        _connectionManager = gameObject.AddComponent<ConnectionManager>();
        await _connectionManager.JoinOrCreateGame();
        Debug.Log($"Initialized Connection Manager");

        // Assigning listener for incoming messages
        _connectionManager.GameRoom.OnMessage<string>("welcomeMessage", message =>
        {
            Debug.Log(message);
        });

        // Register whole game room state change event
        _connectionManager.GameRoom.OnStateChange += OnGameRoomStateChangeHandler;

        // Register player add event
        _connectionManager.GameRoom.State.players.OnAdd((key, player) =>
        {
            Debug.Log($"Player {key} has joined the Game!");
            if (key == _connectionManager.userId)
            {
                // Register own player change event
                player.OnChange(() =>
                {
                    Debug.Log($"##My Player {key} is at {player.x}, {player.y}");
                });
            }
            else
            {
                // Add other player instance to game scene
                GameObject playerInstance = Instantiate(PlayerPrefab, new Vector3(player.x, player.y, 0), Quaternion.identity);
                playerInstance.GetComponent<PlayerController>().enabled = false;
                playerInstance.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value, 1.0f);
                Players.Add(key, playerInstance);
                // Register other players change event
                player.OnChange(() =>
                {
                    Debug.Log($"--Player {key} is at {player.x}, {player.y}");
                    Players[key].transform.position = new Vector3(player.x, player.y, 0);
                });
            }
        });
    }

    private static void OnGameRoomStateChangeHandler(GameRoomState state, bool isFirstState)
    {
        // Handle state change
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
    }
}