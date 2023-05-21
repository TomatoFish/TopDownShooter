using Cinemachine;
using UnityEngine;

namespace Game.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private float _maxRadiusSqr;

        private UnityEngine.Camera mainCamera;

        public UnityEngine.Camera Camera => mainCamera ?? (mainCamera = UnityEngine.Camera.main);

        public void SetFollowTarget(Transform target)
        {
            _virtualCamera.Follow = target;
        }
    }
}