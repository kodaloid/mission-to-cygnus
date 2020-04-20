using System;
using Microsoft.Xna.Framework;

namespace MTC.Includes.Tweening
{
    public abstract class Tween<T> : ITween
    {
        /// <summary>The Tween mode</summary>
        public TweenInterpolateMode Interpolation { get; set; }

        /// <summary>The time this Tween should begin.</summary>
        public TimeSpan TimeFrom { get; }

        /// <summary>The time this Tween should end.</summary>
        public TimeSpan TimeTo { get; }

        /// <summary>The length of time this Tween should play for.</summary>
        public TimeSpan Duration { get; }

        /// <summary>The linear progress of this Tween, stays at 0 till Tween starts, then clamps to 1 at the end.</summary>
        public float Progress { get; set; }

        /// <summary>The value to interpolate from.</summary>
        public T ValueFrom { get; set; }

        /// <summary>The value to interpolate to.</summary>
        public T ValueTo { get; set; }

        /// <summary>A method to call when the Tween completes.</summary>
        public Action<ITween> OnComplete { get; set; }

        /// <summary>The Tween state, weather it's started and completed.</summary>
        public TweenState State { get; set; }

        /// <summary>Hookable action for updating an object.null</summary>
        public Action<ITween> OnUpdate { get; set; }


        /// <summary>Tween constructor</summary>
        protected Tween(TimeSpan timeFrom, TimeSpan timeTo, TweenInterpolateMode interpolation = 0, Action<ITween> onComplete = null) 
        {
            TimeFrom = timeFrom;
            TimeTo = timeTo;
            Duration = timeTo - timeFrom;
            Interpolation = interpolation;
            OnComplete = onComplete;
            State = TweenState.NotStarted;
        }


        /// <summary>Get the real-time interpolated value.</summary>
        public abstract T CurrentValue { get; }


        public void Update(GameTime gameTime)
        {
            // handle starting.
            if (State == TweenState.NotStarted && gameTime.TotalGameTime >= TimeFrom)
            {
                State = TweenState.Playing;
            }

            // handle playing.
            if (State == TweenState.Playing)
            {
                TimeSpan timeIn = gameTime.TotalGameTime - this.TimeFrom;
                float progress = this.Progress;

                if (Duration > TimeSpan.Zero)
                {
                    // TODO: lazy divide by zero conditional.
                    progress = (float)(timeIn.TotalMilliseconds / Duration.TotalMilliseconds); // TODO: why loose precision?
                }

                Progress = progress = MathHelper.Clamp(progress, 0, 1);

                // fires update action if hooked.
                OnUpdate?.Invoke(this);

                if (progress >= 1.0f)
                {
                    State = TweenState.Complete;
                    OnComplete?.Invoke(this);
                }
            }
        }
    }
}