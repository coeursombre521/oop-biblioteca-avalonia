using System;
using System.Collections.Generic;
using PAOO.Biblioteca;
using PAOO.Main.ModelAdapters;
using PAOO.Main.Models;

namespace PAOO.Main.Services;

public class RetinereService : BaseService<Biblioteca.Models.Retinere, Retinere>
{
    private RetinereAdapter _retinereAdapter = new();

    public List<Retinere> GetAll() => ConvertList(BibliotecaContext.GetInstance().GetRetineri(), _retinereAdapter);

    public List<Retinere> GetRetineriByMember(Guid memberId) => ConvertList(BibliotecaContext.GetInstance().GetRetineriByMember(memberId), _retinereAdapter);

    public List<Retinere> GetRetineriByItem(Guid itemId) => ConvertList(BibliotecaContext.GetInstance().GetRetineriByItem(itemId), _retinereAdapter);

    public Retinere? GetRetinereById(Guid id) => _retinereAdapter.ConvertToModel(BibliotecaContext.GetInstance().GetRetinereById(id), true);

    public int CountRetineri => BibliotecaContext.GetInstance().CountRetineri();

    public void AddRetinere(Guid itemId, Guid memberId) => BibliotecaContext.GetInstance().AddRetinere(itemId, memberId);

    public void RemoveRetinere(Guid id) => BibliotecaContext.GetInstance().RemoveRetinere(id);

    public int DaysRetinereOverdue(Guid id) => BibliotecaContext.GetInstance().DaysRetinereOverdue(id);
}