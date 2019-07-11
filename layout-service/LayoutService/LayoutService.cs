using System;
using System.Collections.Generic;

namespace Layout.Service
{
    public class LayoutService : ILayoutService
    {
        private ILayoutRepository Repository { get; set; }

        public LayoutService(ILayoutRepository repo)
        {
            this.Repository = repo;
        }

        public Page AddLayout(Page layout, int position)
        {
            return layout;
        }

        public Component AddComponent(Component component, int position, string layoutId)
        {
            return component;
        }

        public Page GetLayout(string layoutId)
        {
            var layout = this.Repository.GetOneAsync(l => l.Id.Equals(layoutId)).Result;
            return layout;
        }

        public List<Page> GetLayouts()
        {
            var layouts = this.Repository.GetAsync().Result;
            return layouts;
        }
    }
}
