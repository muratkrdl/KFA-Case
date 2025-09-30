using Unity.Cinemachine;
using UnityEngine;

namespace _Scripts.Extensions
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class LockCinemachineAxis : CinemachineExtension
    {
        [SerializeField] private float yClampValue = 10;
        
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, 
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != CinemachineCore.Stage.Body) return;
            
            var position = state.RawPosition;
            position.y = yClampValue;
            state.RawPosition = position;
        }
    }
}
