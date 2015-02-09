using EGCad.Common.Infrastructure;
using EGCad.Common.Model.Data;

namespace EGCad.Core.Normalize
{
    public interface IDataNormalizer
    {
        NormalizeType Type { get; }
        Data Normalize(Data sourceData);
    }
}
