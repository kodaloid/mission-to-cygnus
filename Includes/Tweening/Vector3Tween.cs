using Microsoft.Xna.Framework;
using System;

namespace MTC.Includes.Tweening
{
    public class Vector3Tween : Tween<Vector3>
    {
        public override Vector3 CurrentValue 
        {
            get
            {
                switch (Interpolation)
                {
                    case TweenInterpolateMode.SmoothStep:
                        var result = MathHelper.Clamp(Progress, 0f, 1f);
                        return Vector3.Hermite(ValueFrom, Vector3.Zero, ValueTo, Vector3.Zero, result);
                    default:
                        return Vector3.Lerp(ValueFrom, ValueTo, Progress);
                }
            }
        }


        public Vector3Tween(Vector3 from, Vector3 to, TimeSpan timeFrom, TimeSpan timeTo, Action<ITween> callback = null) : base(timeFrom, timeTo, 0, callback)
        {
            this.ValueFrom = from;
            this.ValueTo = to;
        }
    }
}