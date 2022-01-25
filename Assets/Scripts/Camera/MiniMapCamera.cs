using UnityEngine;

namespace PlayerCamera
{
    public class MiniMapCamera : MonoBehaviour
    {
        [SerializeField] private Transform _FollowToTransform;
        private void Awake()
        {
            if (_FollowToTransform is null)
                throw new System.Exception("No Transform to follow");
        }
        private void Update()
        {
            if(!(_FollowToTransform is null))
            {
                transform.position = _FollowToTransform.position;
            }
            else
            {
                throw new System.Exception("No Transform to follow");
            }
        }
    }
}
