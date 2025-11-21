namespace Gammashine
{
    public class ModuleManifold
    {
        public InitializeControllable Initialize = InitializeControllable.Start;
        public ModuleUndertaking Undertaking = ModuleUndertaking.Playback;
        public ModuleLiabilities Liabilities = ModuleLiabilities.Regular;
        public PerfomanceControllable Perfomance = PerfomanceControllable.Non;
        public UpdateControllable Updating = UpdateControllable.Update;
    }
}
