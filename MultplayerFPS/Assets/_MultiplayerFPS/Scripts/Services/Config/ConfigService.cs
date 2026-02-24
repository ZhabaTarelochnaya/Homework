using System;
using System.Collections.Generic;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.Config
{
    public class ConfigService : IConfigService
    {
        readonly Dictionary<Type, ScriptableObject> _configs;

        public ConfigService(ConfigsSO configsSO)
        {
            _configs = new Dictionary<Type, ScriptableObject>();

            foreach (var config in configsSO.Configs)
            {
                var type = config.GetType();

                if (_configs.ContainsKey(type))
                {
                    Debug.LogWarning($"Duplicate config of type {type}");
                    continue;
                }

                _configs.Add(type, config);
            }
        }

        public T Get<T>() where T : ScriptableObject
        {
            var type = typeof(T);

            if (_configs.TryGetValue(type, out var config))
                return (T)config;

            Debug.LogError($"Couldn't find config of type {type}");
            return null;
        }
    }
}