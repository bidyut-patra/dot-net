using System;
using System.Collections.Generic;
using System.Text;

namespace Layout.Service
{
    public interface ILayoutService
    {
        Page AddLayout(Page layout, int position);
        Component AddComponent(Component component, int position, string layoutId);
        Page GetLayout(string layoutId);
        List<Page> GetLayouts();
    }
}
