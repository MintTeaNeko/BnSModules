using System;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace AnimationModule
{
    public class AnimationScript : MonoBehaviour
    {
        public AnimationData data;
        private Item _item;
        private Animation _animation;
        private Interactable.Action _bindedAction;
        public Dictionary<string, HandleData> _handleClass = new Dictionary<string, HandleData>();
        
        private void Start()
        {
            _item = GetComponent<Item>();

            _animation = _item.GetCustomReference(data.animationComponent).GetComponent<Animation>();
            _bindedAction = Utility._GetInput(data.bindToButton);
            _item.OnHeldActionEvent += OnHeld;

            foreach (string handle in data.animationHandles.Keys)
            {
                HandleData _handleData = Utility._LoadData(data.animationHandles[handle]);
                if (_handleData != null)
                
                {
                    if (_handleClass.Values.Count > 0)
                    {
                        foreach (HandleData _handleValue in _handleClass.Values)
                        {
                            if (_handleValue.openAnimationName == _handleData.openAnimationName)
                            {
                                _handleClass.Add(handle, _handleValue);
                            }
                        }
                    }
                    else
                    {
                        _handleClass.Add(handle, _handleData);
                    }
                    
                    Debug.Log(_handleData.currentlyClosed);
                }
            }
        }

        private void OnHeld(RagdollHand ragdollhand, Handle handle, Interactable.Action action)
        {
            if (action == _bindedAction && _handleClass.ContainsKey(handle.name))
            {
                HandleData _handleData = _handleClass[handle.name];
                if (_handleData.currentlyClosed)
                {
                    _handleData.currentlyClosed = false;
                    _animation.Play(_handleData.openAnimationName);
                }
                else
                {
                    _handleData.currentlyClosed = true;
                    _animation.Play(_handleData.closeAnimationName);
                }
            }
        }
    }
}