using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce01.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync();//devuelve lista de categorias

        Task<IEnumerable<SelectListItem>> GetComboCountriesAsync();//devuelte lista de paises

        Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId);//devuelve los states de cada paises

        Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId);//devulve las ciudades por provincias

    }
}
