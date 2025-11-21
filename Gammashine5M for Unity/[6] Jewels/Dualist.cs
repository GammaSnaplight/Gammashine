using System;
using System.Collections.Generic;

namespace Snaplight.Origamma
{
    public class Dualist<L, R>
    {
        // Serializable
        public Dualist(bool isSynchronizationSide)
            => _isSynchronizationSide = isSynchronizationSide;

        // Enumeration
        public enum SideList { Left, Right }
        public SideList Side;

        private bool _isSynchronizationSide;

        public int SyncCount
        {
            get
            {
                SyncCheckout();

                return Left.Count;
            }
        }

        public List<L> Left { get; } = new();
        public List<R> Right { get; } = new();

        public void Synchronization(bool isSynchronizationSide = true)
            => _isSynchronizationSide = isSynchronizationSide;

        public void Add(SideList side, object item)
        {
            SyncCheckout();

            if (side == SideList.Left) LeftAdd((L)item);
            else RightAdd((R)item);
        }

        public void LeftAdd(L l)
        {
            SyncCheckout();

            Left.Add(l);
        }

        public void RightAdd(R r)
        {
            SyncCheckout();

            Right.Add(r);
        }

        public void SyncAdd(L l, R r)
        {
            if (SyncContains(l, r)) return;

            Left.Add(l);
            Right.Add(r);
        }

        public void Remove(SideList side, object item)
        {
            SyncCheckout();

            if (side == SideList.Left) Left.Remove((L)item);
            else Right.Remove((R)item);
        }

        public void SyncRemove(L l, R r)
        {
            if (!SyncContains(l, r)) return;

            Left.Remove(l);
            Right.Remove(r);
        }

        public bool Contains(SideList side, object item)
        {
            SyncCheckout();

            if (side == SideList.Left) return Left.Contains((L)item);
            else return Right.Contains((R)item);
        }

        public bool SyncContains(L l, R r)
            => Left.Contains(l) && Right.Contains(r);

        public void Clear(SideList side)
        {
            SyncCheckout();

            if (side == SideList.Left) Left.Clear();
            Right.Clear();
        }

        public void SyncClear()
        {
            Left.Clear();
            Right.Clear();
        }

        public R SyncCorrespondingRight(L l)
        {
            for (int i = 0; i < SyncCount; i++)
            {
                if (Equals(Left[i], l)) return Right[i];
            }
            throw new Exception("The item was not found...");
        }

        public L SyncCorrespondingLeft(R r)
        {
            for (int i = 0; i < SyncCount; i++)
            {
                if (Equals(Right[i], r)) return Left[i];
            }
            throw new Exception("The item was not found...");
        }

        public T This<T>(bool isLeft, T item)
        {
            SyncCheckout();

            if (isLeft)
            {
                for (int i = 0; i < Left.Count; i++)
                {
                    if (Equals(Left[i], item)) return (T)Convert.ChangeType(Left[i], typeof(T));
                }
            }
            else
            {
                for (int i = 0; i < Right.Count; i++)
                {
                    if (Equals(Right[i], item)) return (T)Convert.ChangeType(Right[i], typeof(T));
                }
            }

            throw new Exception("The item was not found...");
        }

        public string SyncReceives(int index)
        {
            return $"L: {Left[index]} | R: {Right[index]}";
        }

        private void SyncCheckout()
        {
            if (!_isSynchronizationSide) throw new Exception("The class has synchronization of lists. You don't have the option to use methods without Sync...();");
        }
    }
}
