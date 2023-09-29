using PluginAPI.Roles;
using System.Collections.Generic;
using System.ComponentModel;

namespace Plugin_Config_EN
{
    public class Config
    {
        [Description("Countdown to Detonation")]
        public float MinutesTillAlphaWarhead { get; set; } = 25;
        [Description("Text of the announcement")]
        public string WarheadAnnouncement { get; set; } = "";
        [Description("Announcement length")]
        public ushort WarheadAnnouncementDuration { get; set; } = 5;
        [Description("Class-D item")]
        public ItemType ClassDItem { get; set; } = ItemType.KeycardJanitor;
        [Description("Scientist item")]
        public ItemType ScientistItem { get; set; } = ItemType.Painkillers;
        [Description("Guard item")]
        public ItemType GuardItem { get; set; } = ItemType.Adrenaline;
        [Description("Commander item")]
        public ItemType CommanderItem { get; set; } = ItemType.None;
        [Description("Class-D ammo(Ammo12gauge,Ammo9x19,Ammo44cal,Ammo762x39,Ammo556x45)")]
        public List<ushort> ClassDAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("Scientist ammo")]
        public List<ushort> ScientistAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("Guard ammo")]
        public List<ushort> GuradAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("NTF ammo")]
        public List<ushort> NTFAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("CI ammo")]
        public List<ushort> CIAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
    }
}