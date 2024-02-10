using System.Collections.Generic;
using PAOO.Main.Models;
using PAOO.Biblioteca;
using PAOO.Main.ModelAdapters;
using System;

namespace PAOO.Main.Services;

public class MembruService : BaseService<Biblioteca.Models.Membru, Membru>
{
    private MembruAdapter _membruAdapter = new();

    public List<Membru> GetAll() => ConvertList(BibliotecaContext.GetInstance().GetMembers(), _membruAdapter);

    public Membru? GetOneByIdentifier(Guid id) => _membruAdapter.ConvertToModel(BibliotecaContext.GetInstance().GetMemberById(id), true);

    public void PayPenalty(Guid id) => BibliotecaContext.GetInstance().PayPenalty(id);

    public int CountMembers => BibliotecaContext.GetInstance().CountMembers();

    public void AddMember(string nume, string adresa, string telefon) => BibliotecaContext.GetInstance().AddMember(nume, adresa, telefon);

    public void RemoveMember(Guid id) => BibliotecaContext.GetInstance().RemoveMember(id);
}