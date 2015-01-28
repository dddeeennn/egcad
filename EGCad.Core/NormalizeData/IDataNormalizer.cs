using EGCad.Common.Infrastructure;

namespace EGCad.Core.NormalizeData
{
    public interface IDataNormalizer
    {
        NormalizeType Type { get; }
        Data Normalize(Data sourceData);
    }
}
