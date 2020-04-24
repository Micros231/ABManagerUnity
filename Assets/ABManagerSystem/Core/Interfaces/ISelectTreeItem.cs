using System;
public interface ISelectTreeItem<TItem>
{
    event Action<TItem> OnSelectItem;

    void SelectItem(TItem item);
}
