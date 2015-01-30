using EGCad.Common.Infrastructure;

namespace EGCad.Core.Normalize
{
    public interface IDataNormalizer
    {
        NormalizeType Type { get; }
        Data Normalize(Data sourceData);
    }
}
