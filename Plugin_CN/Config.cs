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
    }
}