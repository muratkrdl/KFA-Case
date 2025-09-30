using _Scripts.Controllers;

namespace _Scripts.Systems.ObjectPooling.Pools
{
    public class ProjectileObjectPool : BaseObjectPool<ProjectileController>
    {

        protected override void OnGet(ProjectileController obj)
        {
            base.OnGet(obj);
            obj.Reset();
        }

    }
}