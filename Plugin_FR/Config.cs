using System.Collections.Generic;
using System.ComponentModel;

namespace Plugin_Config_FR
{
    public class Config
    {
        [Description("Durée de la minuterie Alpha Bullet Timer")]
        public float MinutesTillAlphaWarhead { get; set; } = 25;
        [Description("Texte de l'annonce de l'ogive Alpha")]
        public string WarheadAnnouncement { get; set; } = "";
        [Description("Durée de l'annonce de l'ogive Alpha Initiate")]
        public ushort WarheadAnnouncementDuration { get; set; } = 5;
        [Description("Class-D Personnel addendum")]
        public ItemType ClassDItem { get; set; } = ItemType.KeycardJanitor;
        [Description("savant fou addendum")]
        public ItemType ScientistItem { get; set; } = ItemType.Painkillers;
        [Description("monter la garde addendum")]
        public ItemType GuardItem { get; set; } = ItemType.Adrenaline;
        [Description("commandants addendum")]
        public ItemType CommanderItem { get; set; } = ItemType.None;
        [Description("nombre de balles de Class-D(Ammo12gauge,Ammo9x19,Ammo44cal,Ammo762x39,Ammo556x45)")]
        public List<ushort> ClassDAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("nombre de balles de scientfiques")]
        public List<ushort> ScientistAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("nombre de balles de sécurité")]
        public List<ushort> GuradAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("nombre de balles de NTF")]
        public List<ushort> NTFAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
        [Description("nombre de balles de CI")]
        public List<ushort> CIAmmo { get; set; } = new List<ushort> { 200, 200, 200, 200, 200 };
    }
}