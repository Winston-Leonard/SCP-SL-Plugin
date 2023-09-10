namespace Plugin.API
{
    public static class APIStayic
    {
        public static bool IsScpItem(this ItemType type) => type == ItemType.SCP018 || type == ItemType.SCP1853 || type == ItemType.SCP268 || type == ItemType.SCP1576 || type == ItemType.SCP207 || type == ItemType.SCP500 || type == ItemType.SCP2176 || type == ItemType.SCP244a || type == ItemType.SCP244b;
        public static bool IsKeycard(this ItemType type) => type == ItemType.KeycardO5 || type == ItemType.KeycardFacilityManager || type == ItemType.KeycardNTFCommander || type == ItemType.KeycardChaosInsurgency || type == ItemType.KeycardNTFLieutenant || type == ItemType.KeycardNTFOfficer || type == ItemType.KeycardGuard || type == ItemType.KeycardContainmentEngineer || type == ItemType.KeycardResearchCoordinator || type == ItemType.KeycardZoneManager || type == ItemType.KeycardScientist || type == ItemType.KeycardJanitor;
        public static bool IsMedical(this ItemType type) => type == ItemType.Medkit || type == ItemType.Adrenaline;
        public static bool IsGun(this ItemType type) => type == ItemType.GunCom45 || type == ItemType.MicroHID || type == ItemType.ParticleDisruptor || type == ItemType.GunE11SR || type == ItemType.GunFSP9 || type == ItemType.GunCrossvec || type == ItemType.GunCOM15 || type == ItemType.GunCOM18;
        public static bool IsOther(this ItemType type) => type == ItemType.GrenadeHE || type == ItemType.GrenadeFlash || type == ItemType.Radio;
        public static bool IsArmour(this ItemType type) => type == ItemType.ArmorCombat || type == ItemType.ArmorHeavy || type == ItemType.ArmorLight;
        public static bool IsAmmo(this ItemType type) => type == ItemType.Ammo12gauge || type == ItemType.Ammo556x45 || type == ItemType.Ammo9x19 || type == ItemType.Ammo762x39 || type == ItemType.Ammo44cal;
    }
}
