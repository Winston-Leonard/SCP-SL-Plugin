using System.Collections.Generic;
using Plugin.API;
using InventorySystem.Items.Pickups;
using MEC;
using Mirror;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using PlayerRoles;
using Plugin_Config_CN;

namespace Plugin
{
    public class Plugin
    {
        [PluginConfig]
        public Config PluginConfig;
        private static CoroutineHandle _coroutineHandle;
        private static CoroutineHandle _coroutine;
        [PluginEntryPoint("Plugin", "", "0.5.2", "Charles de Gaulle")]
        void Engbled()
        {
            EventManager.RegisterEvents(this);
            Log.Warning("English text is translated using a translator, please let me know if it is inaccurate");
        }
        [PluginEvent]
        public void OnRoundStart(RoundStartEvent ev)
        {
            _coroutineHandle = Timing.RunCoroutine(eee());
            _coroutine = Timing.RunCoroutine(clean());
            Log.Info("Round Start");
        }
        [PluginEvent]
        public void OnRoundEnd(RoundEndEvent ev)
        {
            Timing.KillCoroutines(_coroutineHandle);
            Timing.KillCoroutines(_coroutine);
            Log.Info("Round End");
        }
        [PluginEvent]
        public void OnWarheadDetonation(WarheadDetonationEvent ev)
        {
            Log.Info("Alpha Warhead Detonated");
            Timing.KillCoroutines(_coroutineHandle);
        }
        IEnumerator<float> eee()
        {
            yield return Timing.WaitForSeconds(PluginConfig.MinutesTillAlphaWarhead * 60);
            AlphaWarheadController.Singleton.InstantPrepare();
            AlphaWarheadController.Singleton.StartDetonation(false);
            AlphaWarheadController.Singleton.IsLocked = true;
            Server.ClearBroadcasts();
            Server.SendBroadcast(PluginConfig.WarheadAnnouncement, PluginConfig.WarheadAnnouncementDuration);
            Log.Info("Alpha Warhead Detonation Sequence Initiated, CANNOT Be Shut Down");
            yield break;
        }
        private static IEnumerator<float> clean()
        {
            yield return Timing.WaitForSeconds(1);
            int time = 0;
            while (Round.IsRoundStarted)
            {
                yield return Timing.WaitForSeconds(1);
                time++;
                switch (time)
                {
                    case 100:
                        Server.SendBroadcast("<color=#00FFFF>Ground will be cleared in 300 seconds.</color>", 5);
                        break;
                    case 395:
                        Server.ClearBroadcasts();
                        for (int i = 5; i >= 0; i--)
                        {
                            Server.SendBroadcast("<color=#00FFFF>Ground will be cleared in " + i + " s</color>", 1);
                        }
                        break;
                    case 400:
                        Server.ClearBroadcasts();
                        Server.SendBroadcast("<color=#00FFFF>Cleaning up</color>", 3);
                        break;
                    case 405:
                        time = 0;
                        int ragdollsnum = 0;
                        int itemnum = 0;
                        BasicRagdoll[] ragdolls = UnityEngine.Object.FindObjectsOfType<BasicRagdoll>();
                        foreach (BasicRagdoll basicRagdoll in ragdolls)
                        {
                            ragdollsnum++;
                            NetworkServer.Destroy(basicRagdoll.gameObject);
                        }
                        ItemPickupBase[] itemPickupBases = UnityEngine.Object.FindObjectsOfType<ItemPickupBase>();
                        foreach (ItemPickupBase itemPickupBase in itemPickupBases)
                        {
                            if (itemPickupBase.Info.ItemId.IsScpItem() || itemPickupBase.Info.ItemId.IsKeycard() || itemPickupBase.Info.ItemId.IsMedical() || itemPickupBase.Info.ItemId.IsGun() || itemPickupBase.Info.ItemId.IsOther() || itemPickupBase.Info.ItemId.IsArmour() || itemPickupBase.Info.ItemId.IsAmmo())
                            {
                                continue;
                            }
                            NetworkServer.Destroy(itemPickupBase.gameObject);
                            itemnum++;
                        }
                        Server.SendBroadcast("<color=#00FFFF>A total of " + itemnum + " item and " + ragdollsnum + " body cleared</color>", 5);
                        Log.Info(+ itemnum + " item and" + ragdollsnum + " body cleared");
                        break;
                }
            }
        }
        [PluginEvent(ServerEventType.PlayerSpawn)]
        void OnPlayerSpawn(Player player, RoleTypeId roleTypeId)
        {
            Timing.CallDelayed(0.3f, () =>
            {
                switch (roleTypeId)
                {
                    case RoleTypeId.ClassD:
                        player.AddItem(PluginConfig.ClassDItem);
                        break;
                    case RoleTypeId.Scientist:
                        player.AddItem(PluginConfig.ScientistItem);
                        break;
                    case RoleTypeId.FacilityGuard:
                        player.AddItem(PluginConfig.GuardItem);
                        break;
                    case RoleTypeId.NtfCaptain:
                        player.AddItem(PluginConfig.CommanderItem);
                        break;
                }
            });
        }
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoin(Player player)
        {
            player.SendBroadcast("<color=#00FFFF>Welcome </color><color=#FFFF00>" + player.Nickname + "</color><color=#00FFFF>to the server</color>", 5);
        }
        [PluginEvent]
        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            Log.Info("Plugin Running");
        }

    }
}
