using _Scripts.Objects;
using _Scripts.Utilities;

namespace _Scripts.Systems.ObjectPooling.Pools
{
    public class SFXObjectPool : BaseObjectPool<SFXObject>
    {
        protected override void OnRelease(SFXObject obj)
        {
            base.OnRelease(obj);
            obj.SetPosition(ConstUtilities.Zero3);
        }
    }
}