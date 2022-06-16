using System;
using System.Collections;
using ThunderRoad;
using UnityEngine;

namespace NaturalRegeneration
{
    public class RegenerationClass : LevelModule
    {
        public float RegenHealingSpeed;
        public float RegenWaitDelay;
        
        public override IEnumerator OnLoadCoroutine()
        {
            EventManager.onPossess += OnPossesEvent;
            return base.OnLoadCoroutine();
        }

        private void OnPossesEvent(Creature creature, EventTime eventtime)
        {
            if (eventtime == EventTime.OnEnd)
                creature.gameObject.AddComponent<RegenerationBehavior>().jsonData = this;
        }
    }

    public class RegenerationBehavior : MonoBehaviour
    {
        public RegenerationClass jsonData;
        private bool canHeal = true;
        private float timer = 0;
        private void Start() => EventManager.onCreatureHit += OnCreatureHit;

        private void Update()
        {
            if (!canHeal)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                    canHeal = true;
            }
            else if (Player.currentCreature.currentHealth < Player.currentCreature.maxHealth)
                Player.currentCreature.Heal(jsonData.RegenHealingSpeed * Time.deltaTime, Player.currentCreature);
        }

        private void OnCreatureHit(Creature creature, CollisionInstance collisioninstance)
        {
            if (creature.isPlayer)
            {
                canHeal = false;
                timer = jsonData.RegenWaitDelay;
            }
        }
    }
}