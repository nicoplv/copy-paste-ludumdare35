using UnityEngine;
using System.Collections.Generic;

namespace ShapeshiftableObjects
{
    public class ShapeshiftableObject : MonoBehaviour
    {
        #region Variables

        protected bool inView = false;
        public bool InView { get { return inView; } }

        [SerializeField]
        protected ShapeshiftableObject original;
        public ShapeshiftableObject Original { get { return original; } }

        [SerializeField]
        protected string resource;
        public string Resource { get { return resource; } }

        [SerializeField]
        protected Collider inViewCollider;

        [SerializeField]
        protected List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

        [SerializeField]
        protected Material inViewMaterial;

        #endregion

        #region Unity Methods

        #endregion

        #region In View Methods

        public void InViewStart()
        {
            inView = true;
            foreach (MeshRenderer iMeshRenderer in meshRenderers)
            {
                Material[] materialsBuffer = iMeshRenderer.materials;
                Material[] materials = new Material[materialsBuffer.Length + 1];
                materials[0] = inViewMaterial;
                for (int i = 0; i < materialsBuffer.Length; i++)
                    materials[i + 1] = materialsBuffer[i];
                iMeshRenderer.materials = materials;
            }
        }

        public void InViewEnd()
        {
            inView = false;
            foreach (MeshRenderer iMeshRenderer in meshRenderers)
            {
                Material[] materialsBuffer = iMeshRenderer.materials;
                Material[] materials = new Material[materialsBuffer.Length - 1];
                for (int i = 1; i < materialsBuffer.Length; i++)
                    materials[i - 1] = materialsBuffer[i];
                iMeshRenderer.materials = materials;
            }
        }

        public void DisableInView()
        {
            inViewCollider.enabled = false;
        }

        #endregion
    }
}