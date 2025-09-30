using System;
using Unity.Cinemachine;
using UnityEngine;

namespace _Scripts.Extensions
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class ClampCinemachineAxis : CinemachineExtension
    {
        [SerializeField] private BoxCollider areaBounds;

        private Bounds _bounds;
        
        private void Start()
        {
            _bounds = areaBounds.bounds;
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, 
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != CinemachineCore.Stage.Body) return;

            _bounds = areaBounds.bounds;

            var pos = state.RawPosition;
            
            pos.x = Mathf.Clamp(pos.x, _bounds.min.x, _bounds.max.x);
            pos.z = Mathf.Clamp(pos.z, _bounds.min.z, _bounds.max.z);
            
            state.RawPosition = pos;
        }
    }
}