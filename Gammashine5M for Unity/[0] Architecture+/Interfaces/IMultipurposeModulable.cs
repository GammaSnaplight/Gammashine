namespace Gammashine
{
    public interface IMultipurposeModulable<T> : IModulable
        where T : IRawable<T>
    {
    }
}
