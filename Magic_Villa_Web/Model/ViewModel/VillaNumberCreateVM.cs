using Magic_Villa_Web.Models.Dto.VillaNumberDto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Magic_Villa_Web.Model.ViewModel
{
    public class VillaNumberCreateVM
    {
        public VillaNumberCreateDto villaNumber { get; set; }

        public VillaNumberCreateVM()
        {
            villaNumber = new VillaNumberCreateDto();
        }
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
