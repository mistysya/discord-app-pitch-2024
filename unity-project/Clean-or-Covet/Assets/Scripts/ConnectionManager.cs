using System.Collections.Generic;
using System.Threading.Tasks;
using Colyseus;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public string userId = "";
    private static ColyseusClient _client = null;
    private static ColyseusRoom<GameRoomState> _room = null;
    private const string HostAddress = "ws://localhost:13001";
    private const string GameName = "game";

    public void Initialize()
    {
        // Log the host address to the console.
        _client = new ColyseusClient(HostAddress);
        Debug.Log($"Initialized to {HostAddress}");
    }

    public async Task JoinOrCreateGame()
    {
        // Will create a new game room if there is no available game rooms in the server.
        Debug.Log($"Joining or creating game room:");
        Client.GetType();
        int randomUserId = Random.Range(0, 1000);
        Debug.Log($"randomUserId: {randomUserId}");
        userId = "userIdTest_" + randomUserId;
        Dictionary<string, object> joinOptions = new Dictionary<string, object>
        {
            { "roomName", "roomName" },
            { "channelId", "channelId" },
            { "userId", userId}
        };
        _room = await Client.JoinOrCreate<GameRoomState>(GameName, joinOptions);
        Debug.Log($"Joined or created game room:");
    }

    public ColyseusClient Client
    {
        get
        {
            // Initialize Colyseus client, if the client has not been initiated yet or input values from the Menu have been changed.
            if (_client == null)
            {
                Initialize();
                Debug.Log($"Initialized Colyseus Client");
            }
            return _client;
        }
    }

    public ColyseusRoom<GameRoomState> GameRoom
    {
        get
        {
            if (_room == null)
            {
                Debug.LogError("Room hasn't been initialized yet!");
            }
            return _room;
        }
    }

    public void PlayerPosition(Vector2 position)
    {
        _ = GameRoom.Send("position", new { x = position.x, y = position.y });
    }
}
