using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace AnimationModule
{
    public class AnimationData : ItemModule
    {
        public bool startClosed;
        public string bindToButton;
        public string animationComponent;
        public Dictionary<string, string> animationHandles;

        public override void OnItemLoaded(Item item)
        {
            item.gameObject.AddComponent<AnimationScript>().data = this;
            base.OnItemLoaded(item);
        }
    }
}