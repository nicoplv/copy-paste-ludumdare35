using UnityEngine;
using System.Collections.Generic;

namespace Players
{
    public class ShapeshiftableObject : MonoBehaviour
    {
        #region Variables

        protected bool inView = false;
        public bool InView { get { return inView; } }

        [SerializeField]
        protected string resource;
        public string Resource { get { return resource; } }

        [SerializeField]
        protected List<Collider> inViewColliders = new List<Collider>();

        [SerializeField]
        protected List<Collider> collisionCollider = new List<Collider>();
        public List<Collider> CollisionCollider { get { return collisionCollider; } }

        [SerializeField]
        protected bool physics = true;

        protected new Rigidbody rigidbody;

        [SerializeField]
        protected Transform lookAt;
        public Transform LookAt { get { return lookAt; } }

        [SerializeField]
        protected float cameraDistance = 5f;
        public float CameraDistance { get { return cameraDistance; } }

        [SerializeField]
        protected float mass = 1f;
        public float Mass { get { return mass; } }

        [SerializeField]
        protected float drag = 1f;
        public float Drag { get { return drag; } }

        [SerializeField]
        protected float angularDrag = 1f;
        public float AngularDrag { get { return angularDrag; } }

        [SerializeField]
        protected List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

        [SerializeField]
        protected Material inViewMaterial;

        [SerializeField]
        protected Transform leftEye;
        public Transform LeftEye { get { return leftEye; } }

        [SerializeField]
        protected Transform rightEye;
        public Transform RightEye { get { return rightEye; } }

        [SerializeField]
        protected Bulle bulle;
        public Bulle Bulle { get { return bulle; } }

        #endregion

        #region Unity Methods

        public void Start()
        {
            if (physics)
            {
                rigidbody = gameObject.AddComponent<Rigidbody>();
                rigidbody.mass = mass;
                rigidbody.drag = drag;
                rigidbody.angularDrag = angularDrag;
            }
        }

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

        public void DisablePhysics()
        {
            foreach(Collider iCollider in inViewColliders)
                iCollider.enabled = false;
            physics = false;
        }

        #endregion
    }
}