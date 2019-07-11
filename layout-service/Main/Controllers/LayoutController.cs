using Layout.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Services
{
    [Route("layouts")]
    public class LayoutController
    {
        private ILayoutService LayoutService { get; set; }

        public LayoutController(ILayoutService layoutService)
        {
            this.LayoutService = layoutService;
        }

        [HttpGet("{layoutId}")]
        public Page GetLayout(string layoutId)
        {
            return this.LayoutService.GetLayout(layoutId);
        }

        [HttpGet]
        public List<Page> GetLayouts()
        {
            return this.LayoutService.GetLayouts();
        }

        [HttpPost]
        public Page AddLayout(Page pageLayout, int position)
        {
            return this.LayoutService.AddLayout(pageLayout, position);
        }
    }
}
