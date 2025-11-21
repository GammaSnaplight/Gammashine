namespace Gammashine
{
    public interface IMasterable<T> : IPlayableModulable
        where T : IManifoldable<IModulable>
    {
        public T Manifold { get; }
    }
}
