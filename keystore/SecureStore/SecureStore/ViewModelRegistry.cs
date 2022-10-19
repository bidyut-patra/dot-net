using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStore
{
    public class ViewModelRegistry
    {
        private static Lazy<ViewModelRegistry> _instance = new Lazy<ViewModelRegistry>(() => new ViewModelRegistry());
        public static ViewModelRegistry Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        public Dictionary<string, ViewModelBase> ViewModels { get; private set; }

        private ViewModelRegistry()
        {
            this.ViewModels = new Dictionary<string, ViewModelBase>();
        }

        public void Register(ViewModelBase viewModel)
        {
            var className = viewModel.GetType().FullName;
            this.ViewModels.Add(className, viewModel);
        }

        public void UnRegister(Type type)
        {
            var className = type.FullName;
            if(this.ViewModels.ContainsKey(className))
            {
                this.ViewModels.Remove(className);
            }
        }

        public ViewModelBase Get(Type type)
        {
            var className = type.FullName;
            return this.ViewModels.ContainsKey(className) ? this.ViewModels[className] : null;
        }
    }
}
