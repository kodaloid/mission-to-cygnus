using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MTC.Includes.Tweening
{
    /// <summary>
    /// Represents a sequence of Tween.
    /// </summary>
    public class TweenPlayer
    {
        private readonly List<ITween> TweenPool;


        public TweenPlayer(int initialCapacity = 256)
        {
            TweenPool = new List<ITween>(initialCapacity);
        }


        public void Update(GameTime gameTime)
        {
            // update pool.
            TweenPool.ForEach(t => t.Update(gameTime));

            // prune complete (loop backwards to allow removal).
            for (int i = TweenPool.Count - 1; i >= 0; i--)
            {
                // do we need to free the slot?
                if (TweenPool[i].State == TweenState.Complete)
                {
                    TweenPool.RemoveAt(i);
                }
            }
        }        


        public ITween Add(ITween Tween)
        {
            TweenPool.Add(Tween);
            return Tween;
        }


        public ITween Add(float from, float to, TimeSpan timeFrom, TimeSpan timeTo, Action<ITween> callback = null)
            => Add(new FloatTween(from, to, timeFrom, timeTo, callback));


        public ITween Add(Vector3 from, Vector3 to, TimeSpan timeFrom, TimeSpan timeTo, Action<ITween> callback = null)
            => Add(new Vector3Tween(from, to, timeFrom, timeTo, callback));
    }
}