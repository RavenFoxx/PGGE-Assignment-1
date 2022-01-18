using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGGE
{
    // The base class for all third-person camera controllers
    public abstract class TPCBase
    {
        protected Transform mCameraTransform;
        protected Transform mPlayerTransform;
        RaycastHit hit;
        LayerMask wall = LayerMask.GetMask("Walls");

        public Transform CameraTransform
        {
            get
            {
                return mCameraTransform;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                return mPlayerTransform;
            }
        }

        public TPCBase(Transform cameraTransform, Transform playerTransform)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
        }

        public void RepositionCamera()
        {
            Vector3 playerTransformTemp = PlayerTransform.position + new Vector3(0, CameraTransform.position.y, 0);
            if (Physics.Linecast(playerTransformTemp, CameraTransform.position, out hit, wall)) {
                CameraTransform.position = Vector3.Lerp(CameraTransform.position, playerTransformTemp - CameraTransform.position, Time.deltaTime);
            }
        }

        public abstract void Update();
    }
}
