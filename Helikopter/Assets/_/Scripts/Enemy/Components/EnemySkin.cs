using UnityEngine;

namespace EnemyNamespace
{
    public class EnemySkin : MonoBehaviour
    {
        [SerializeField]
        private SkinnedMeshRenderer skinnedMeshRenderer;

        [SerializeField]
        private Texture[] skins;

        private void Awake()
        {
            skinnedMeshRenderer.material = new Material(skinnedMeshRenderer.material);
        }

        private void OnEnable()
        {
            ChangeSkin();
        }

        private void ChangeSkin()
        {
            int index = Random.Range(0, skins.Length);
            skinnedMeshRenderer.material.SetTexture("_MainTex", skins[index]);
        }
    }
}