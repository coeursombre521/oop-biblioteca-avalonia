using System;
using System.Collections.Generic;
using PAOO.Main.ModelAdapters;

namespace PAOO.Main;

public abstract class BaseService<T, K>
{
    protected List<K> ConvertList(List<T> source, IModelAdapter<T, K> adapter)
    {
        List<K> list = new();
        foreach (var i in source)
        {
            var obj = adapter.ConvertToModel(i, true);
            if (obj != null)
            {
                list.Add(obj);
            }
        }
        return list;
    }
}
