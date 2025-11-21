namespace Gammashine
{
    public interface IMainstreamModulable : IMultipurposeModulable<ModuleManifold>, IPlayable, IShuttable
    {
        public ModuleManifold Changeover { get; set; }

        public void Lightback();
    }
}
