using System;
using System.Collections.Generic;
using PAOO.Biblioteca;
using PAOO.Biblioteca.Factories;
using PAOO.Main.ModelAdapters;
using PAOO.Main.Models;

namespace PAOO.Main.Services;

public class ItemService : BaseService<Biblioteca.Models.BorrowableItem, BorrowableItem>
{
    private BorrowableItemAdapter _borrowableItemAdapter = new();

    public List<BorrowableItem> GetAll() => ConvertList(BibliotecaContext.GetInstance().GetItems(), _borrowableItemAdapter);

    public List<BorrowableItem> GetAvailable() => ConvertList(BibliotecaContext.GetInstance().GetAvailableItems(), _borrowableItemAdapter);

    public List<BorrowableItem> GetBorrowed() => ConvertList(BibliotecaContext.GetInstance().GetBorrowedItems(), _borrowableItemAdapter);

    public List<BorrowableItem> GetBorrowedByMember(Guid memberId) => ConvertList(BibliotecaContext.GetInstance().GetBorrowedItemsByMember(memberId), _borrowableItemAdapter);

    public BorrowableItem? GetItemById(Guid id) => _borrowableItemAdapter.ConvertToModel(BibliotecaContext.GetInstance().GetItemById(id), true);

    public int CountItems => BibliotecaContext.GetInstance().CountItems();

    public void AddCarte(string titlu, string autor) => BibliotecaContext.GetInstance().AddItem(new CarteProperties(titlu, autor));

    public void AddRevista(string titlu) => BibliotecaContext.GetInstance().AddItem(new RevistaProperties(titlu));

    public void RemoveItem(Guid itemId) => BibliotecaContext.GetInstance().RemoveItem(itemId);

    public void BorrowItem(Guid itemId, Guid memberId) => BibliotecaContext.GetInstance().BorrowItem(itemId, memberId);

    public void ReturnItem(Guid itemId, Guid memberId) => BibliotecaContext.GetInstance().ReturnItem(itemId, memberId);

    public int DaysItemReturnOverdue(Guid itemId, Guid memberId) => BibliotecaContext.GetInstance().DaysItemReturnOverdue(itemId, memberId);
}
