using System;
using System.Collections.Generic;
using System.Text;

namespace Knitter
{
    public class Scene
    {
        private List<GameObject> _gameObjects = new List<GameObject>();

        internal void Update()
        {
            _gameObjects.ForEach(go => go.Update());
        }

        public void AddGameObject(GameObject gameObject) => _gameObjects.Add(gameObject);
        public void RemoveScene(GameObject gameObject) => _gameObjects.Remove(gameObject);
    }
}
