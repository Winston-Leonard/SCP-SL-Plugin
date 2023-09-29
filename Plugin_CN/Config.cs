using System.Collections.Generic;
using System.ComponentModel;

namespace Plugin_Config_CN
{
    public class Config
    {
        [Description("计时器时长")]
        public float MinutesTillAlphaWarhead { get; set; } = 25;
        [Description("广播文本")]
        public string WarheadAnnouncement { get; set; } = "";
        [Description("广播时长")]
        public ushort WarheadAnnouncementDuration { get; set; } = 5;
        [Description("Class-D额外物品")]
        public ItemType ClassDItem { get; set; } = ItemType.KeycardJanitor;
        [Description("科学家额外物品")]
        public ItemType ScientistItem { get; set; } = ItemType.Painkillers;
        [Description("安保额外物品")]
        public ItemType GuardItem { get; set; } = ItemType.Adrenaline;
        [Description("指挥官额外物品")]
        public ItemType CommanderItem { get; set; } = ItemType.None;
        [Description("Class-D备弹(Ammo12gauge,Ammo9x19,Ammo44cal,Ammo762x39,Ammo556x45)")]
        public List<ushort> ClassDAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("科学家备弹")]
        public List<ushort> ScientistAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("安保备弹")]
        public List<ushort> GuradAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("NTF备弹")]
        public List<ushort> NTFAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("CI备弹")]
        public List<ushort> CIAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
    }
}