using UnityEngine;

namespace Dila
{
    public class ChangeSkyBoxes : MonoBehaviour
    {
        [SerializeField] private float _skyBoxRotateSpeed;
        public void ChangeSkyBox(Material material)
            => RenderSettings.skybox = material;
        private void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * _skyBoxRotateSpeed);
        }
    }
}
