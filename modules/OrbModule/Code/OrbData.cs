using System;
using System.Collections;
using System.IO;
using System.Reflection;
using OrbsModule.Module;
using ThunderRoad;
using UnityEngine;

namespace OrbsModule
{
    public class OrbData : ItemModule
    {
        public float Circumference;
        public float RadiusRangeLimit;
        public float RotationSpeed;
        public float MovementSpeed;
        public bool EnableAttackModule;
        public float AttackDamage;
        public bool EnableHelpfulModule;
        public float HealthMultiplier;
        public float HealingSpeed;

        public override void OnItemLoaded(Item item)
        {
            Debug.Log(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            item.gameObject.AddComponent<OrbBehavior>().Data = this;
            base.OnItemLoaded(item);
        }
    }
    public class OrbBehavior : MonoBehaviour
    {
        public static OrbBehavior currentOrb;
        public OrbData Data;
        public Item Item;
        public bool isActivate = false, isMoving = false;
        public RagdollPart PartToOrbit;
        private (float min, float radius, float max) allowedRange;
        private Vector3 desiredVector;
        private void Start()
        {
            Item = GetComponent<Item>();

            if (currentOrb != null)
                Item.Despawn();
            currentOrb = this;
            
            Item.disallowDespawn = true;
            UpdateRadius(Data.Circumference);
            if (Player.currentCreature != null)
            {
                PartToOrbit = Player.currentCreature.ragdoll.GetPart(RagdollPart.Type.Torso);
                desiredVector = new Vector3(PartToOrbit.transform.position.x + allowedRange.radius, PartToOrbit.transform.position.y, PartToOrbit.transform.position.z);
            }
            
            if (Data.EnableAttackModule)
                Item.gameObject.AddComponent<AttackModule>().Orb = this;

            if (Data.EnableHelpfulModule)
                Item.gameObject.AddComponent<HelpfulModule>().Orb = this;
        }
        private void Update()
        {
            if (Item.handlers.Count > 0 || Item.isTelekinesisGrabbed || Item.isPooled)
            {
                Item.rb.isKinematic = false;
                isActivate = false; 
            }
            else
            {
                Item.rb.isKinematic = true;
                isActivate = true;

                if (!isMoving)
                {
                    float distanceFromTarget = Vector3.Distance(PartToOrbit.transform.position, transform.position);
                    if (distanceFromTarget < allowedRange.min || distanceFromTarget > allowedRange.max)
                        StartCoroutine(MoveToPosition());
                    else
                        transform.RotateAround(PartToOrbit.transform.position, Vector3.up, Data.RotationSpeed * Time.deltaTime);
                }
            }
        }
        public IEnumerator MoveToPosition()
        {
            isMoving = true;
            Debug.Log("test ran coroutine");
            while (Vector3.Distance(Item.transform.position, desiredVector) > 0f)
            {
                desiredVector = new Vector3(PartToOrbit.transform.position.x + allowedRange.radius, PartToOrbit.transform.position.y, PartToOrbit.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, desiredVector, Data.MovementSpeed * Time.deltaTime);
                yield return null;
            }
            isMoving = false;
        }
        public void UpdateRadius(float circumference)
        {
            allowedRange.radius = circumference/ (2f * 3.1415f);
            allowedRange.min = allowedRange.radius - Data.RadiusRangeLimit;
            allowedRange.max = allowedRange.radius + Data.RadiusRangeLimit;
        }
        public void ResetRadius() => UpdateRadius(Data.Circumference);
    }
}