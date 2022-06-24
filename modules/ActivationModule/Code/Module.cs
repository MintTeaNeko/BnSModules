using System;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace ActivationModule
{
    public class Module : ItemModule
    {
        public string ButtonBind;
        public bool StartsActivated;
        public string AnimationRefrence;
        public string ActivationAnimName;
        public string DeActivationAnimName;
        public List<string> ActivationSounds;
        public List<string> DeactivationSounds;
        public override void OnItemLoaded(Item item)
        {
            item.gameObject.AddComponent<ActivationBehavior>().jsonData = this;
            base.OnItemLoaded(item);
        }
    }

    public class ActivationBehavior : MonoBehaviour
    {
        public Module jsonData;
        private bool isActivated;
        private Item item;
        private Animation animation;
        private Interactable.Action? bindAction;
        private List<AudioSource> activationSources = new List<AudioSource>();
        private List<AudioSource> deActivationSources = new List<AudioSource>();
        private void Start()
        {
            item = GetComponent<Item>();

            if (jsonData.StartsActivated)
                isActivated = true;
            else
                isActivated = false;

            bindAction = GetInput(jsonData.ButtonBind);
            animation = item.GetCustomReference(jsonData.AnimationRefrence).GetComponent<Animation>();
            
            foreach (string refrenceName in jsonData.ActivationSounds)
                activationSources.Add(item.GetCustomReference(refrenceName).GetComponent<AudioSource>());
            
            foreach (string refrenceName in jsonData.DeactivationSounds)
                activationSources.Add(item.GetCustomReference(refrenceName).GetComponent<AudioSource>());
            
            item.OnHeldActionEvent += _OnHeldEvent;
        }

        private void _OnHeldEvent(RagdollHand ragdollhand, Handle handle, Interactable.Action action)
        {
            if (bindAction != null && action == bindAction)
            {
                if (!isActivated)
                {
                    StopSounds(deActivationSources);
                    PlaySounds(activationSources);
                    animation.Play(jsonData.ActivationAnimName);
                }
                else
                {
                    StopSounds(activationSources);
                    PlaySounds(deActivationSources);
                    animation.Play(jsonData.DeActivationAnimName);
                }

                isActivated = !isActivated;
            }
        }

        private Interactable.Action? GetInput(string _action)
        {
            Interactable.Action? _key;
            switch (_action.ToLower())
            {
                case "trigger":
                    _key = Interactable.Action.UseStart;
                    break;
                
                case "spell":
                    _key = Interactable.Action.AlternateUseStart;
                    break;
                
                default:
                    _key = null;
                    break;
            }

            return _key;
        }

        private void PlaySounds(List<AudioSource> _sources)
        {
            foreach (AudioSource source in _sources)
                source.Play();
        }
        
        private void StopSounds(List<AudioSource> _sources)
        {
            foreach (AudioSource source in _sources)
                source.Stop();
        }
    }
}