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
    }
}