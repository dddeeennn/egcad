using EGCad.Common.Infrastructure;
using EGCad.Common.Model.Data;
using EGCad.Common.Model.Normalize;

namespace EGCad.Core.Normalize
{
    public interface IDataNormalizer
    {
        NormalizeType Type { get; }
        NormalizeData Normalize(Data sourceData);
    }
}
