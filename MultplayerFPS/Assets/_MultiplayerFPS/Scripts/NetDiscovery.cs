using System;
using System.Net;
using _MultiplayerFPS.Scripts.Utils.ExceptionPopUp;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
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
    protected override DiscoveryResponse ProcessRequest(DiscoveryRequest request, IPEndPoint endpoint)
    {
        try
        {
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
            string msg = $"Transport {transport} does not support network discovery";
            ServiceLocator.Current.Get<IExceptionUIService>().ShowError("NotImplementedException", msg);
            return default;
        }
    }
    protected override DiscoveryRequest GetRequest() => new();
    protected override void ProcessResponse(DiscoveryResponse response, IPEndPoint endpoint)
    {
        response.EndPoint = endpoint;
        UriBuilder realUri = new UriBuilder(response.Uri)
        {
            Host = response.EndPoint.Address.ToString()
        };
        response.Uri = realUri.Uri;
        OnServerFound.Invoke(response);
    }
}
