using System;
using System.IO;
using ThunderRoad;
using UnityEngine;
using Newtonsoft.Json;

namespace AnimationModule
{
    public class Utility : MonoBehaviour
    {
        public static Interactable.Action _GetInput(string inputName)
        {
            switch (inputName.ToLower())
            {
                case "trigger":
                    return Interactable.Action.UseStart;
                
                case "grab":
                    return Interactable.Action.Grab;
            }

            return Interactable.Action.AlternateUseStart;
        }

        public static HandleData _LoadData(string jsonName)
        {
            return JsonConvert.DeserializeObject<HandleData>(File.ReadAllText(Application.streamingAssetsPath + "/Mods/AnimationsJson/" + jsonName + ".json"));
        }
    }
}