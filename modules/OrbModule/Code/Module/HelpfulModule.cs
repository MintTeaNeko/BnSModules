using System;
using ThunderRoad;
using UnityEngine;

namespace OrbsModule.Module
{
    public class HelpfulModule : MonoBehaviour
    {
        public OrbBehavior Orb;
        private Creature targetCreature;
        private void Update()
        {
            targetCreature = Orb.PartToOrbit.ragdoll.creature;
            if (Orb.isActivate && targetCreature.isPlayer)
            {
                if (Player.currentCreature.currentHealth < Player.currentCreature.maxHealth)
                    Player.currentCreature.Heal(Orb.Data.HealingSpeed * Time.deltaTime, Player.currentCreature);

                // TODO: add the things I wanted like buffs.
            }
        }
    }
}