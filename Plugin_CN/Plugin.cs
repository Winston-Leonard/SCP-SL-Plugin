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
        [PluginEntryPoint("Plugin", "", "0.5.6", "Charles de Gaulle")]
        void Engbled()
        {
            EventManager.RegisterEvents(this);
        }
        [PluginEvent]
        public void OnRoundStart(RoundStartEvent ev)
        {
            _coroutineHandle = Timing.RunCoroutine(eee());
            _coroutine = Timing.RunCoroutine(clean());
            Log.Info("回合开始");
        }
        [PluginEvent]
        public void OnRoundEnd(RoundEndEvent ev)
        {
            Timing.KillCoroutines(_coroutineHandle);
            Timing.KillCoroutines(_coroutine);
            Log.Info("回合结束");
        }
        [PluginEvent]
        public void OnWarheadDetonation(WarheadDetonationEvent ev)
        {
            Log.Info("Alpha弹头已引爆");
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
            Log.Info("Alpha弹头已启动，不可关闭");
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
                        Server.SendBroadcast("<color=#00FFFF>服务器将于300s后清理物品与尸体</color>", 5);
                        break;
                    case 395:
                        Server.ClearBroadcasts();
                        for (int i = 5; i >= 0; i--)
                        {
                            Server.SendBroadcast("<color=#00FFFF>服务器将于" + i + "s后清理物品与尸体</color>", 1);
                        }
                        break;
                    case 400:
                        Server.ClearBroadcasts();
                        Server.SendBroadcast("<color=#00FFFF>服务器正在清理物品与尸体</color>", 3);
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
                        Server.SendBroadcast("<color=#00FFFF>本次共清理了" + itemnum + "件物品和" + ragdollsnum + "具尸体</color>", 5);
                        Log.Info("已清理" + itemnum + "件物品与" + ragdollsnum + "具尸体");
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
                        player.SetAmmo(ItemType.Ammo12gauge, PluginConfig.ClassDAmmo[0]);
                        player.SetAmmo(ItemType.Ammo9x19, PluginConfig.ClassDAmmo[1]);
                        player.SetAmmo(ItemType.Ammo44cal, PluginConfig.ClassDAmmo[2]);
                        player.SetAmmo(ItemType.Ammo762x39, PluginConfig.ClassDAmmo[3]);
                        player.SetAmmo(ItemType.Ammo556x45, PluginConfig.ClassDAmmo[4]);
                        break;
                    case RoleTypeId.Scientist:
                        player.AddItem(PluginConfig.ScientistItem);
                        player.SetAmmo(ItemType.Ammo12gauge, PluginConfig.ScientistAmmo[0]);
                        player.SetAmmo(ItemType.Ammo9x19, PluginConfig.ScientistAmmo[1]);
                        player.SetAmmo(ItemType.Ammo44cal, PluginConfig.ScientistAmmo[2]);
                        player.SetAmmo(ItemType.Ammo762x39, PluginConfig.ScientistAmmo[3]);
                        player.SetAmmo(ItemType.Ammo556x45, PluginConfig.ScientistAmmo[4]);
                        break;
                    case RoleTypeId.FacilityGuard:
                        player.AddItem(PluginConfig.GuardItem);
                        player.SetAmmo(ItemType.Ammo12gauge, PluginConfig.GuradAmmo[0]);
                        player.SetAmmo(ItemType.Ammo9x19, PluginConfig.GuradAmmo[1]);
                        player.SetAmmo(ItemType.Ammo44cal, PluginConfig.GuradAmmo[2]);
                        player.SetAmmo(ItemType.Ammo762x39, PluginConfig.GuradAmmo[3]);
                        player.SetAmmo(ItemType.Ammo556x45, PluginConfig.GuradAmmo[4]);
                        break;
                    case RoleTypeId.NtfPrivate:
                        player.SetAmmo(ItemType.Ammo12gauge, PluginConfig.NTFAmmo[0]);
                        player.SetAmmo(ItemType.Ammo9x19, PluginConfig.NTFAmmo[1]);
                        player.SetAmmo(ItemType.Ammo44cal, PluginConfig.NTFAmmo[2]);
                        player.SetAmmo(ItemType.Ammo762x39, PluginConfig.NTFAmmo[3]);
                        player.SetAmmo(ItemType.Ammo556x45, PluginConfig.NTFAmmo[4]);
                        break;
                    case RoleTypeId.NtfSergeant:
                        player.SetAmmo(ItemType.Ammo12gauge, PluginConfig.NTFAmmo[0]);
                        player.SetAmmo(ItemType.Ammo9x19, PluginConfig.NTFAmmo[1]);
                        player.SetAmmo(ItemType.Ammo44cal, PluginConfig.NTFAmmo[2]);
                        player.SetAmmo(ItemType.Ammo762x39, PluginConfig.NTFAmmo[3]);
                        player.SetAmmo(ItemType.Ammo556x45, PluginConfig.NTFAmmo[4]);
                        break;
                    case RoleTypeId.NtfSpecialist:
                        player.SetAmmo(ItemType.Ammo12gauge, PluginConfig.NTFAmmo[0]);
                        player.SetAmmo(ItemType.Ammo9x19, PluginConfig.NTFAmmo[1]);
                        player.SetAmmo(ItemType.Ammo44cal, PluginConfig.NTFAmmo[2]);
                        player.SetAmmo(ItemType.Ammo762x39, PluginConfig.NTFAmmo[3]);
                        player.SetAmmo(ItemType.Ammo556x45, PluginConfig.NTFAmmo[4]);
                        break;
                    case RoleTypeId.NtfCaptain:
                        player.AddItem(PluginConfig.CommanderItem);
                        player.SetAmmo(ItemType.Ammo12gauge, PluginConfig.NTFAmmo[0]);
                        player.SetAmmo(ItemType.Ammo9x19, PluginConfig.NTFAmmo[1]);
                        player.SetAmmo(ItemType.Ammo44cal, PluginConfig.NTFAmmo[2]);
                        player.SetAmmo(ItemType.Ammo762x39, PluginConfig.NTFAmmo[3]);
                        player.SetAmmo(ItemType.Ammo556x45, PluginConfig.NTFAmmo[4]);
                        break;
                    case RoleTypeId.ChaosConscript:
                        player.SetAmmo(ItemType.Ammo12gauge, PluginConfig.CIAmmo[0]);
                        player.SetAmmo(ItemType.Ammo9x19, PluginConfig.CIAmmo[1]);
                        player.SetAmmo(ItemType.Ammo44cal, PluginConfig.CIAmmo[2]);
                        player.SetAmmo(ItemType.Ammo762x39, PluginConfig.CIAmmo[3]);
                        player.SetAmmo(ItemType.Ammo556x45, PluginConfig.CIAmmo[4]);
                        break;
                    case RoleTypeId.ChaosMarauder:
                        player.SetAmmo(ItemType.Ammo12gauge, PluginConfig.CIAmmo[0]);
                        player.SetAmmo(ItemType.Ammo9x19, PluginConfig.CIAmmo[1]);
                        player.SetAmmo(ItemType.Ammo44cal, PluginConfig.CIAmmo[2]);
                        player.SetAmmo(ItemType.Ammo762x39, PluginConfig.CIAmmo[3]);
                        player.SetAmmo(ItemType.Ammo556x45, PluginConfig.CIAmmo[4]);
                        break;
                    case RoleTypeId.ChaosRepressor:
                        player.SetAmmo(ItemType.Ammo12gauge, PluginConfig.CIAmmo[0]);
                        player.SetAmmo(ItemType.Ammo9x19, PluginConfig.CIAmmo[1]);
                        player.SetAmmo(ItemType.Ammo44cal, PluginConfig.CIAmmo[2]);
                        player.SetAmmo(ItemType.Ammo762x39, PluginConfig.CIAmmo[3]);
                        player.SetAmmo(ItemType.Ammo556x45, PluginConfig.CIAmmo[4]);
                        break;
                    case RoleTypeId.ChaosRifleman:
                        player.SetAmmo(ItemType.Ammo12gauge, PluginConfig.CIAmmo[0]);
                        player.SetAmmo(ItemType.Ammo9x19, PluginConfig.CIAmmo[1]);
                        player.SetAmmo(ItemType.Ammo44cal, PluginConfig.CIAmmo[2]);
                        player.SetAmmo(ItemType.Ammo762x39, PluginConfig.CIAmmo[3]);
                        player.SetAmmo(ItemType.Ammo556x45, PluginConfig.CIAmmo[4]);
                        break;
                }
            });
        }
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoin(Player player)
        {
            player.SendBroadcast("<color=#00FFFF>欢迎</color><color=#FFFF00>" + player.Nickname + "</color><color=#00FFFF>加入服务器</color>", 5);
        }
        [PluginEvent]
        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            Log.Info("插件已运行");
        }

    }
}
