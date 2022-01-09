using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public interface IItem : IDroppable
    {

    }

    public interface IActionable
    {
        void DoAction();
    }
    public interface IActiveItem : IItem, IActionable
    {

    }

    public interface IItemHolder : IActionable
    {
        void DropItem();
        void PickItem(IActiveItem item);
        bool IsFree { get; }
        IActiveItem Item { get; }

    }
}

