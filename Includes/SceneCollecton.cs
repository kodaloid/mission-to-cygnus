using System.Collections.Generic;

namespace MTC.Includes
{
    /// <summary>
    /// A collecton of scenes.
    /// </summary>
    public class SceneCollection : List<Scene>
    {
        private Scene current;

        public Scene Current { get => current; }

        public void Show(Scene scene)
        {
            if (!this.Contains(scene))
            {
                this.Add(scene);
            }

            this.current = scene;
        }

        public new void Clear()
        {
            current = null;
            base.Clear();
        }

        // TODO: override remove/removeall etc... to also clear current if necessary.
    }
}