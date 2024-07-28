using UnityEngine;
using Unity.Netcode;
using System;

public class RpcTest: NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsServer && IsOwner)
            TestServerRpc(0, NetworkObjectId);
    }

    [Rpc(SendTo.ClientsAndHost)]
    void TestClientRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Client Recieved the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
        if (IsOwner)
            TestServerRpc(value + 1, sourceNetworkObjectId);
    }

    [Rpc(SendTo.Server)]
    private void TestServerRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Server Recieved the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
        TestClientRpc(value, sourceNetworkObjectId);
    }
}
