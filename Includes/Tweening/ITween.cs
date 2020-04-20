using System;
using Microsoft.Xna.Framework;

namespace MTC.Includes.Tweening
{
    public interface ITween
    {
        TweenInterpolateMode Interpolation { get; set; }
        TimeSpan TimeFrom { get; }
        TimeSpan TimeTo { get; }
        TimeSpan Duration { get; }
        float Progress { get; set; }
        TweenState State { get; set; }
        Action<ITween> OnComplete { get; set; }
        Action<ITween> OnUpdate { get; set; }


        void Update(GameTime gameTime);
    }
}