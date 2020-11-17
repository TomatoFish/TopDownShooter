using UnityEngine;

namespace Assets.Scripts
{
    class PlayerWeaponController : MonoBehaviour
    {
        public LineRenderer LineRenderer;
        public Transform StartPosition;

        public void SetLook(Vector3 target)
        {
            transform.LookAt(target);

            //transform.parent.Rotate(transform.parent.rotation.x, transform.parent.rotation.y, transform.parent.rotation.z);
            //transform.parent.localEulerAngles += new Vector3(0, 0, -90);
        }

        private void Update()
        {
            LineRenderer.SetPosition(0, StartPosition.position);
            LineRenderer.SetPosition(1, StartPosition.position + StartPosition.forward * 10);
        }
    }
}
