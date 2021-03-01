using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerWeaponController : MonoBehaviour
    {
        public Transform LeftHandTransform;
        public LineRenderer LineRenderer;
        public Transform StartPosition;

        public ItemIK[] items;
        
        private PlayerController player;
        private ItemIK currentItem;
        
        public void Init(PlayerController player)
        {
            this.player = player;
        }

        public void SetItem(int id)
        {
            currentItem?.gameObject.SetActive(false);
            currentItem = items[id];
            currentItem.gameObject.SetActive(true);
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
            if (player.IsAiming)
            {
                var targetLook = player.Player.GetMousePositionRelativeHeight();
                if ((targetLook - transform.position).magnitude < 1)
                {
                    targetLook = player.transform.position + player.GetMouseLookDirection() + Vector3.up * transform.position.y;
                }

                transform.LookAt(targetLook);
            }

            if (currentItem != null)
            {
                LeftHandTransform.position = currentItem.LeftHandTarget.position;
                LeftHandTransform.rotation = currentItem.LeftHandTarget.rotation;
            }

            UpdateLineRenderer();
        }
    }
}
