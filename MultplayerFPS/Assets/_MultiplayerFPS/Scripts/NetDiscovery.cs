using System;
using System.Net;
using Mirror;
using Mirror.Discovery;
using UnityEngine;


public struct DiscoveryRequest : NetworkMessage
{
    // Add public fields (not properties) for whatever information you want
    // sent by clients in their broadcast messages that servers will use.
}

public struct DiscoveryResponse : NetworkMessage
{
    public IPEndPoint EndPoint { get; set; }
    public long ServerId;
    public Uri Uri;
    public string HostPlayerName;
    public int PlayerCount;
}

public class NetDiscovery : NetworkDiscoveryBase<DiscoveryRequest, DiscoveryResponse>
{
    #region Unity Callbacks

#if UNITY_EDITOR
    public override void OnValidate()
    {
        base.OnValidate();
    }
#endif

    public override void Start()
    {
        base.Start();
    }

    #endregion

    #region Server

    /// <summary>
    /// Process the request from a client
    /// </summary>
    /// <remarks>
    /// Override if you wish to provide more information to the clients
    /// such as the name of the host player
    /// </remarks>
    /// <param name="request">Request coming from client</param>
    /// <param name="endpoint">Address of the client that sent the request</param>
    /// <returns>A message containing information about this server</returns>
    protected override DiscoveryResponse ProcessRequest(DiscoveryRequest request, IPEndPoint endpoint)
    {
        try
        {
            // this is an example reply message,  return your own
            // to include whatever is relevant for your game
            return new DiscoveryResponse
            {
                ServerId = ServerId,
                Uri = transport.ServerUri(),
                PlayerCount = NetworkServer.connections.Count,
                HostPlayerName = NetManager.singleton.GetHostName()
            };
        }
        catch (NotImplementedException)
        {
            Debug.LogError($"Transport {transport} does not support network discovery");
            throw;
        }
    }

    #endregion

    #region Client

    /// <summary>
    /// Create a message that will be broadcasted on the network to discover servers
    /// </summary>
    /// <remarks>
    /// Override if you wish to include additional data in the discovery message
    /// such as desired game mode, language, difficulty, etc... </remarks>
    /// <returns>An instance of ServerRequest with data to be broadcasted</returns>
    protected override DiscoveryRequest GetRequest() => new();

    /// <summary>
    /// Process the answer from a server
    /// </summary>
    /// <remarks>
    /// A client receives a reply from a server, this method processes the
    /// reply and raises an event
    /// </remarks>
    /// <param name="response">Response that came from the server</param>
    /// <param name="endpoint">Address of the server that replied</param>
    protected override void ProcessResponse(DiscoveryResponse response, IPEndPoint endpoint)
    {
        // we received a message from the remote endpoint
        response.EndPoint = endpoint;
        
        // although we got a supposedly valid url, we may not be able to resolve
        // the provided host
        // However we know the real ip address of the server because we just
        // received a packet from it,  so use that as host.
        UriBuilder realUri = new UriBuilder(response.Uri)
        {
            Host = response.EndPoint.Address.ToString()
        };
        response.Uri = realUri.Uri;
        OnServerFound.Invoke(response);
    }

    #endregion

}
