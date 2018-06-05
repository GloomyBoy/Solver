using System;
using System.Collections.Generic;
using EmPuzzleLogic.Entity;
using EmPuzzleLogic.Enums;

namespace EmPuzzleLogic.Behaviour
{
    public static class ActionFactory
    {
        public static IBehaviour GetBehaviour(CellItem item)
        {
            switch (item.Type)
            {
                case CellType.None:
                    return new EmptyBehaviour(item);
                case CellType.Regular:
                    return new RegularBehavior(item);
                case CellType.Dragon:
                    return new DragonBehaviour(item);
                case CellType.Crystal:
                    return new CrystalBehaviour(item);
            }
            return null;
        }
    }

    public class CrystalBehaviour : ABehaviour
    {
        public CrystalBehaviour(CellItem item) : base(item)
        {
        }

        public override List<CellItem> Action(SwapType action, List<CellItem> processed = null)
        {
            var touch = new List<CellItem>();
            if (processed?.Contains(_item) == true)
            {
                return touch;
            }
            touch.AddRange(processed ?? new List<CellItem>());
            switch (action)
            {
                case SwapType.Down:
                    if (_item.Parent.Height <= _item.Position.Y + 1)
                        throw new MovementException($"Cant move cell {_item} down");
                    Swap(_item, _item.Position.X, _item.Position.Y + 1);
                    break;
                case SwapType.Up:
                    if (_item.Position.Y - 1 < 0)
                        throw new MovementException($"Cant move cell {_item} up");
                    Swap(_item, _item.Position.X, _item.Position.Y - 1);
                    break;
                case SwapType.Right:
                    if (_item.Parent.Width <= _item.Position.X + 1)
                        throw new MovementException($"Cant move cell {_item} right");
                    Swap(_item, _item.Position.X + 1, _item.Position.Y);
                    break;
                case SwapType.Left:
                    if (_item.Position.X - 1 < 0)
                        throw new MovementException($"Cant move cell {_item} left");
                    Swap(_item, _item.Position.X - 1, _item.Position.Y);
                    break;
                case SwapType.Point:
                case SwapType.Kill:
                    touch.Add(_item);
                    for (int i = 0; i < _item.Parent.Width; i++)
                    {
                        for (int j = 0; j < _item.Parent.Height; j++)
                        {
                            if(_item.Parent[i, j]?.Tag == _item.Tag)
                                touch.AddRange(ActionFactory.GetBehaviour(_item.Parent[i,j])
                                    .Action(SwapType.Kill, touch));
                        }
                    }
                    break;
            }
            return touch;
        }
    }

    public class DragonBehaviour : ABehaviour
    {
        public DragonBehaviour(CellItem item) : base(item)
        {
        }

        public override List<CellItem> Action(SwapType action, List<CellItem> processed = null)
        {
            var touch = new List<CellItem>();
            if (processed?.Contains(_item) == true)
            {
                return touch;
            }
            touch.AddRange(processed ?? new List<CellItem>());
            switch (action)
            {
                case SwapType.Down:
                    if (_item.Parent.Height <= _item.Position.Y + 1)
                        throw new MovementException($"Cant move cell {_item} down");
                    Swap(_item, _item.Position.X, _item.Position.Y + 1);
                    break;
                case SwapType.Up:
                    if (_item.Position.Y - 1 < 0)
                        throw new MovementException($"Cant move cell {_item} up");
                    Swap(_item, _item.Position.X, _item.Position.Y - 1);
                    break;
                case SwapType.Right:
                    if (_item.Parent.Width <= _item.Position.X + 1)
                        throw new MovementException($"Cant move cell {_item} right");
                    Swap(_item, _item.Position.X + 1, _item.Position.Y);
                    break;
                case SwapType.Left:
                    if (_item.Position.X - 1 < 0)
                        throw new MovementException($"Cant move cell {_item} left");
                    Swap(_item, _item.Position.X - 1, _item.Position.Y);
                    break;
                case SwapType.Point:
                case SwapType.Kill:
                    touch.Add(_item);
                    if (_item.Position.X > 0)
                        if(_item.Parent[_item.Position.X - 1, _item.Position.Y] != null)
                            touch.AddRange(ActionFactory.GetBehaviour(_item.Parent[_item.Position.X - 1, _item.Position.Y])
                            .Action(SwapType.Kill, touch));
                    if (_item.Position.Y > 0)
                        if (_item.Parent[_item.Position.X, _item.Position.Y - 1] != null)
                            touch.AddRange(ActionFactory.GetBehaviour(_item.Parent[_item.Position.X, _item.Position.Y - 1])
                            .Action(SwapType.Kill, touch));
                    if (_item.Position.X < _item.Parent.Width - 1)
                        if (_item.Parent[_item.Position.X + 1, _item.Position.Y] != null)
                            touch.AddRange(ActionFactory.GetBehaviour(_item.Parent[_item.Position.X + 1, _item.Position.Y])
                            .Action(SwapType.Kill, touch));
                    if (_item.Position.Y < _item.Parent.Height - 1)
                        if (_item.Parent[_item.Position.X, _item.Position.Y + 1] != null)
                            touch.AddRange(ActionFactory.GetBehaviour(_item.Parent[_item.Position.X, _item.Position.Y + 1])
                            .Action(SwapType.Kill, touch));
                    break;
            }
            return touch;
        }
    }

    public class RegularBehavior : ABehaviour
    {
        public RegularBehavior(CellItem item) : base(item)
        {
        }

        public override List<CellItem> Action(SwapType action, List<CellItem> processed = null)
        {
            var touch = new List<CellItem>();
            if (processed?.Contains(_item) == true)
            {
                return touch;
            }
            touch.AddRange(processed ?? new List<CellItem>());
            switch (action)
            {
                case SwapType.Point:
                    return new List<CellItem>();
                case SwapType.Down:
                    if (_item.Parent.Height <= _item.Position.Y + 1)
                        throw new MovementException($"Cant move cell {_item} down");
                    Swap(_item, _item.Position.X, _item.Position.Y + 1);
                    break;
                case SwapType.Up:
                    if (_item.Position.Y - 1 < 0)
                        throw new MovementException($"Cant move cell {_item} up");
                    Swap(_item, _item.Position.X, _item.Position.Y - 1);
                    break;
                case SwapType.Right:
                    if (_item.Parent.Width <= _item.Position.X + 1)
                        throw new MovementException($"Cant move cell {_item} right");
                    Swap(_item, _item.Position.X + 1, _item.Position.Y);
                    break;
                case SwapType.Left:
                    if (_item.Position.X - 1 < 0)
                        throw new MovementException($"Cant move cell {_item} left");
                    Swap(_item, _item.Position.X - 1, _item.Position.Y);
                    break;
                case SwapType.Kill:
                    touch.Add(_item);
                    break;
            }
            return touch;
        }
    }

    public class EmptyBehaviour : ABehaviour
    {
        public override List<CellItem> Action(SwapType action, List<CellItem> processed = null)
        {
            return processed ?? new List<CellItem>();
        }

        public EmptyBehaviour(CellItem item) : base(item)
        {
        }
    }
}