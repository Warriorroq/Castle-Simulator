using UnityEngine;

namespace PlayerCamera
{
    public class MiniMapCamera : MonoBehaviour
    {
        [SerializeField] private Transform _FollowToTransform;
        [SerializeField] private Vector3 _OffSet;
        private void Awake()
        {
            if (_FollowToTransform is null)
                throw new System.Exception("No Transform to follow");
        }
        private void Update()
        {
            if(!(_FollowToTransform is null))
            {
                transform.position = _FollowToTransform.position + _OffSet;
            }
            else
            {
                throw new System.Exception("No Transform to follow");
            }
        }
    }
}
