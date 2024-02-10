using System;

namespace PAOO.Main.ModelAdapters;

public interface IModelAdapter<T, K>
{
    K? ConvertToModel(T? obj, bool includes);
}
