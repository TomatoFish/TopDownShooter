using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Assets.Scripts
{
    public struct ItemCooldownJob : IJobParallelFor
    {
        public Item.Data ItemData;
        public float Dt;
        
        public void Execute(int index)
        {
            var data = ItemData;
            data.Update(Dt);
            ItemData = data;
        }
    }
    
    public class PlayerItemController : MonoBehaviour
    {
        public Transform LeftHandTransform;
        public LineRenderer LineRenderer;
        public Transform StartPosition;
        public MultiAimConstraint Constraint;
        public Item[] Items;
        
        private PlayerController player;

        private Item CurrentItem => Items[player.Player.CurrentItem];
        public float WeaponHeight => transform.position.y;
        
        public void Init(PlayerController player)
        {
            this.player = player;
        }

        public void SetItem(int id)
        {
            CurrentItem?.gameObject.SetActive(false);
            //CurrentItem = Items[id];
            CurrentItem.gameObject.SetActive(true);
        }
        
        private void ItemChangedHandler(int curtItem)
        {
            player.Animator.SetLayerWeight(CurrentItem.AnimatorLayer, 0);
            SetItem(curtItem);
            player.Animator.SetLayerWeight(CurrentItem.AnimatorLayer, 1);
            player.Animator.SetInteger("Weapon", curtItem);
        }

        public void FireHandler(bool isFiring)
        {
            
            player.Animator.SetBool("IsAttacking", isFiring);
        }

        public void ShootHandler()
        {
            CurrentItem.MuzzleFlash.Play();
            
        }
        
        public void EnableActions()
        {
            player.Player.OnItemChanged += ItemChangedHandler;
            player.Player.OnFire += FireHandler;
        }
        
        public void DisableActions()
        {
            player.Player.OnItemChanged -= ItemChangedHandler;
            player.Player.OnFire -= FireHandler;
        }

        private void Update()
        {
            if (CurrentItem == null) return;
            
            var itemData = new Item.Data(CurrentItem);
            var job = new ItemCooldownJob
            {
                ItemData = itemData,
                Dt = Time.deltaTime
            };
            
            var jobHandle = job.Schedule(Items.Length, 1);
            jobHandle.Complete();
        }

        private void UpdateLineRenderer()
        {
            LineRenderer.enabled = player.IsAiming;
            if (player.IsAiming)
            {
                LineRenderer.SetPosition(0, StartPosition.position);
                LineRenderer.SetPosition(1, StartPosition.position + StartPosition.forward * 10);
            }
        }

        private void LateUpdate()
        {
            Constraint.weight = player.IsAiming ? 1 : 0;

            if (CurrentItem != null)
            {
                LeftHandTransform.position = CurrentItem.LeftHandTarget.position;
                LeftHandTransform.rotation = CurrentItem.LeftHandTarget.rotation;
            }

            UpdateLineRenderer();
        }
    }
}
