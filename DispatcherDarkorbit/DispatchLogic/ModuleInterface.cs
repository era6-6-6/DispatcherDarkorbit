using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherDarkorbit.DispatchLogic
{
    public abstract class ModuleInterface
    {
        public abstract void Clear();
        public abstract bool IsActive();


        public virtual Task<bool> Tick()
        {
            return Task.FromResult(true);

        }


    }
}
