using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Colyseus;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public string userId = "";
    private string discordId = "";
    private string discordName = "";
    private string channelName = "";
    private static ColyseusClient _client = null;
    private static ColyseusRoom<GameRoomState> _room = null;
    private const string HostAddress = "ws://localhost:13001";
    private const string GameName = "game";

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")] private static extern string GetPlayerId();
    [DllImport("__Internal")] private static extern string GetPlayerName();
    [DllImport("__Internal")] private static extern string GetChannelName();
#else
    // something else to emulate what you want to do
    private static string GetPlayerId() { return "tset-playerId"; }
    private static string GetPlayerName() { return "test-playerName"; }
    private static string GetChannelName() { return "test-channelName"; }
#endif

    public void Initialize()
    {
        // Log the host address to the console.
        _client = new ColyseusClient(HostAddress);
        Debug.Log($"Initialized to {HostAddress}");
        // Wait until get non empty discord user info
        while (string.IsNullOrEmpty(discordId))
        {
            discordId = GetPlayerId();
            Debug.Log($"discordId: {discordId}");
            //UniTask.Delay(1000);
        }
        while (string.IsNullOrEmpty(discordName))
        {
            discordName = GetPlayerName();
            Debug.Log($"discordName: {discordName}");
            //UniTask.Delay(1000);
        }
        while (string.IsNullOrEmpty(channelName))
        {
            channelName = GetChannelName();
            Debug.Log($"channelName: {channelName}");
            //UniTask.Delay(1000);
        }
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
