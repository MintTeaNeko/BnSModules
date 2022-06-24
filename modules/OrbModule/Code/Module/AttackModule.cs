using System;
using System.Collections;
using ThunderRoad;
using UnityEngine;

namespace OrbsModule.Module
{
    public class AttackModule : MonoBehaviour
    {
        public OrbBehavior Orb;
        private void Start() => EventManager.onCreatureHit += OnCreatureHit;
        private void OnCreatureHit(Creature creature, CollisionInstance collisioninstance)
        {
            if (Orb.isActivate && !creature.isPlayer)
            {
                Orb.PartToOrbit = creature.ragdoll.GetPart(RagdollPart.Type.Torso);
                StartCoroutine(DamageOverTime());
            }
        }
        public IEnumerator DamageOverTime()
        {
            Creature target = Orb.PartToOrbit.ragdoll.creature;
            while (!target.isKilled)
            {
                DamageStruct d = new DamageStruct(DamageType.Pierce, Orb.Data.AttackDamage * Time.deltaTime);
                d.hitRagdollPart = Orb.PartToOrbit;
                target.Damage(new CollisionInstance(d));
                yield return null;
            }
            if (!Orb.PartToOrbit.ragdoll.creature.isPlayer)
                Orb.PartToOrbit = Player.currentCreature.ragdoll.GetPart(RagdollPart.Type.Torso);
        }
    }
}