using Microsoft.Xna.Framework;
using System;

namespace MTC.Includes.Tweening
{
    public class FloatTween : Tween<float>
    {
        public override float CurrentValue
        {
            get
            {
                switch (Interpolation)
                {
                    case TweenInterpolateMode.SmoothStep:
                        var result = MathHelper.Clamp(Progress, 0f, 1f);
                        return MathHelper.Hermite(ValueFrom, 0f, ValueTo, 0f, result);
                    default:
                        return MathHelper.Lerp(ValueFrom, ValueTo, Progress); 
                }
            }
        }


        public FloatTween(float from, float to, TimeSpan timeFrom, TimeSpan timeTo, Action<ITween> callback) : base(timeFrom, timeTo, 0, callback)
        {
            this.ValueFrom = from;
            this.ValueTo = to;
        }        
    }
}