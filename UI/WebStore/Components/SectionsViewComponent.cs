using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public SectionsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke(string sectionId) {

            int? childSectionId, parentSectionId;

            if (!string.IsNullOrEmpty(sectionId)) {
                int.TryParse(sectionId, out int id);
                childSectionId = id;
            }
            else {
                childSectionId = null;
            }
                                   
            List<SectionViewModel> parentSectionsViews = GetSections();

            parentSectionId = GetParentIdByChildSectionId(parentSectionsViews, childSectionId);

            ViewData["parentSectionId"] = parentSectionId;
            ViewData["childSectionId"] = childSectionId;

            return View(parentSectionsViews);
        }

        /// <summary>
        /// Возвращает список родительских секций
        /// </summary>
        /// <param name="sectionId">Идентификатор дочерней секции</param>
        /// <param name="parnetSectionId">Идентификатор родительской секции</param>
        /// <returns></returns>
        public List<SectionViewModel> GetSections() {
            var allSections = _productData.GetSections().ToArray();

            //Получаем список родительских секций
            IEnumerable<Section> parentSections = allSections.FromDTO().Where(s => s.ParentId is null);
            
            List<SectionViewModel> parentSectionsViews = parentSections.Select(s => new SectionViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Order = s.Order,
                //Инициализируем ProductsQuantity количеством продуктов SectionId которых связан непосредственно с родительской секцией
                ProductsQuantity = _productData.GetProducts().Where(p => p.FromDTO().SectionId == s.Id).Count(),
                //ProductsQuantity = s.ParentId is null ? _productData.GetProducts().Where(p => p.FromDTO().SectionId == s.Id).Count() : 0,
            }).ToList();

            //Для каждой родительской секции формируем коллекцию дочерних секций
            foreach (var parentSection in parentSectionsViews)
            {
                var childs = allSections.Where(s => s.ParentId == parentSection.Id);

                parentSection.ChildSections = new List<SectionViewModel>();

                int totalProductCountForParentSection = parentSection.ProductsQuantity;

                foreach (var childSection in childs)
                {
                    int productQuantityTemp = _productData.GetProducts().Where(p => p.FromDTO().SectionId == childSection.Id).Count();
                    totalProductCountForParentSection += productQuantityTemp;

                    parentSection.ChildSections.Add(new SectionViewModel()
                    {
                        Id = childSection.Id,
                        Name = childSection.Name,
                        Order = childSection.Order,
                        ParentSection = parentSection,
                        ProductsQuantity = productQuantityTemp,
                    });
                }
                parentSection.ProductsQuantity = totalProductCountForParentSection;

                parentSection.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }

            parentSectionsViews.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

            return parentSectionsViews;
        }

        public int? GetParentIdByChildSectionId(List<SectionViewModel> parentSectionsViews, int? childSectionId) {
            foreach (SectionViewModel parentSection in parentSectionsViews) {
                foreach (SectionViewModel childSection in parentSection.ChildSections) {
                    if (childSection.Id == childSectionId) {
                        return childSection.ParentSection.Id;
                    }
                }
            }

            return null;
        }
    }
}
