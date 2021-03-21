using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Interfaces;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public SectionsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke() {

            var sections = _productData.GetSections().ToArray();

            IEnumerable<Section> parentSections = sections.Where(s => s.ParentId is null);

            List<SectionViewModel> parentSectionViews = parentSections.Select(s => new SectionViewModel() {
                Id = s.Id,
                Name = s.Name,
                Order = s.Order,
                ProductsQuantity = s.ParentId is null ? _productData.GetProducts().Where(p => p.SectionId == s.Id).Count() : 0,
            }).ToList();

            foreach (var parentSection in parentSectionViews) {
                var childs = sections.Where(s => s.ParentId == parentSection.Id);

                parentSection.ChildSections = new List<SectionViewModel>();

                int ChildSections_ProductsCount = parentSection.ProductsQuantity;

                foreach (Section childSection in childs)
                {
                    int productQuantityTemp = _productData.GetProducts().Where(p => p.SectionId == childSection.Id).Count();
                    ChildSections_ProductsCount += productQuantityTemp;

                    parentSection.ChildSections.Add(new SectionViewModel()
                    {
                        Id = childSection.Id,
                        Name = childSection.Name,
                        Order = childSection.Order,
                        ParentSection = parentSection,
                        ProductsQuantity = productQuantityTemp,
                    });
                }
                parentSection.ProductsQuantity = ChildSections_ProductsCount;

                parentSection.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }

            parentSectionViews.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));


            return View(parentSectionViews);
        }
        
    }
}
