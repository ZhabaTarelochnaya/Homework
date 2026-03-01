using System.Collections;
using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.CoroutineRunner;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.GrenadeService
{
    public class GrenadeService : IGrenadeService
    {
        readonly ObjectPool<Grenade> _grenadePool;
        readonly GrenadeConfig _grenadeConfig;
        readonly ICoroutineRunnerService _coroutineRunnerService;

        public GrenadeService()
        {
            _grenadeConfig = ServiceLocator.Current.Get<IConfigService>().Get<GrenadeConfig>();
            _grenadePool = new ObjectPool<Grenade>(_grenadeConfig.GrenadePrefab);
            _coroutineRunnerService = ServiceLocator.Current.Get<ICoroutineRunnerService>();
        }
        public void ThrowGrenade(PlayerState playerState, Vector3 origin, Vector3 direction)
        {
            var isRemoved = playerState.Pickups.Remove(PickupName.Grenade);
            if (!isRemoved) return;
            if (_grenadePool.Get(out Grenade grenade))
            {
                NetworkServer.Spawn(grenade.gameObject);
            }
            else
            {
                grenade.RpcSetActive(true);
            }
            grenade.transform.position = origin;
            grenade.transform.rotation = Quaternion.identity;
            grenade.Throw(direction);
            _coroutineRunnerService.StartCoroutine(WaitReturnGrenade(grenade));
        }
        IEnumerator WaitReturnGrenade(Grenade grenade)
        {
            yield return new WaitForSeconds(_grenadeConfig.ThrowDuration 
                                            + _grenadeConfig.ExplosionDelay 
                                            + _grenadeConfig.ExplosionDuration);
            grenade.RpcSetActive(false);
            _grenadePool.Return(grenade);
        }
    }
}