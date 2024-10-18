﻿namespace Snaplight.Gen3
{
    public interface IEnterable<T>
    {
        public void Enterfold(T fold);
        public void Enterfolds(params T[] folds);
    }
}
