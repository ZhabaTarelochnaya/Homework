using System.Threading.Tasks;
using UnityEngine;

namespace NPCEventNotification.Scripts.Utils.Extansions
{
    public static class UnityTask
    {
        public static async Task WaitForSeconds(float seconds)
        {
            var time =  Time.time + seconds;
            while (Time.time < time)
            {
                await Task.Yield();
            }
        }
    }
}