using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE;

public enum CameraType {
    Track,
    Follow_Track_Pos,
    Follow_Track_Pos_Rot,
    Topdown,
    Follow_Independent,
}

public class ThirdPersonCamera : MonoBehaviour {
    public LayerMask floor;
    public Transform mPlayer;
    TPCBase mThirdPersonCamera;
    public Vector3 mPositionOffset = new Vector3(0.0f, 2.0f, -2.5f);
    public Vector3 mAngleOffset = new Vector3(0.0f, 0.0f, 0.0f);
    public float mDamping = 1.0f;
    public float mMinPitch = -30.0f;
    public float mMaxPitch = 30.0f;
    public float mRotationSpeed = 50.0f;
    public FixedTouchField mTouchField;
    public CameraType mCameraType = CameraType.Follow_Track_Pos;
    Dictionary<CameraType, TPCBase> mThirdPersonCameraDict = new Dictionary<CameraType, TPCBase>();
    void Start() {
        CameraConstants.Damping = mDamping;
        CameraConstants.CameraPositionOffset = mPositionOffset;
        CameraConstants.CameraAngleOffset = mAngleOffset;
        CameraConstants.MinPitch = mMinPitch;
        CameraConstants.MaxPitch = mMaxPitch;
        CameraConstants.RotationSpeed = mRotationSpeed;
        mThirdPersonCameraDict.Add(CameraType.Track, new TPCTrack(transform, mPlayer));
        mThirdPersonCameraDict.Add(CameraType.Follow_Track_Pos, new TPCFollowTrackPosition(transform, mPlayer));
        mThirdPersonCameraDict.Add(CameraType.Follow_Track_Pos_Rot, new TPCFollowTrackPositionAndRotation(transform, mPlayer));
        mThirdPersonCameraDict.Add(CameraType.Topdown, new TPCTopDown(transform, mPlayer));
        #if UNITY_STANDALONE
                mThirdPersonCameraDict.Add(CameraType.Follow_Independent,
                new TPCFollowIndependentRotation(transform, mPlayer));
        #endif
        #if UNITY_ANDROID
                mThirdPersonCameraDict.Add(CameraType.Follow_Independent,
                new TPCFollowIndependentRotation(transform, mPlayer, mTouchField));
        #endif

        mThirdPersonCamera = mThirdPersonCameraDict[mCameraType];

    }
    private void Update() {
        CameraConstants.Damping = mDamping;
        CameraConstants.CameraAngleOffset = mAngleOffset;
        CameraConstants.MinPitch = mMinPitch;
        CameraConstants.MaxPitch = mMaxPitch;
        CameraConstants.RotationSpeed = mRotationSpeed;
        mThirdPersonCamera = mThirdPersonCameraDict[mCameraType];
    }
    void LateUpdate() {
        mThirdPersonCamera.Update();
    }
}
