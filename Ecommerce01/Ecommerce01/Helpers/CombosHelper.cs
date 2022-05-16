using Ecommerce01.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce01.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync()
        {
            //uso el select para poder convertir a un seleclistitem
            List<SelectListItem> Lista = await _context.Categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(c => c.Text).ToListAsync();

            //con esta linea inserto el mensaje o seleccion por defecto del combobox
            Lista.Insert(0, new SelectListItem { Text = "[Seleccione una categoria...]", Value = "0" });
            return Lista;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId)
        {
            List<SelectListItem> Lista = await _context.Cities.Where(s => s.State.Id == stateId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(c => c.Text).ToListAsync();

            //con esta linea inserto el mensaje o seleccion por defecto del combobox
            Lista.Insert(0, new SelectListItem { Text = "[Seleccione una Ciudad...]", Value = "0" });
            return Lista;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCountriesAsync()
        {
            List<SelectListItem> Lista = await _context.Countries.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(c => c.Text).ToListAsync();

            //con esta linea inserto el mensaje o seleccion por defecto del combobox
            Lista.Insert(0, new SelectListItem { Text = "[Seleccione un Pais...]", Value = "0" });
            return Lista;

        }

        public async Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId)
        {
            List<SelectListItem> Lista = await _context.States.Where(s => s.Country.Id == countryId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(c => c.Text).ToListAsync();

            //con esta linea inserto el mensaje o seleccion por defecto del combobox
            Lista.Insert(0, new SelectListItem { Text = "[Seleccione una Provincia...]", Value = "0" });
            return Lista;
        }
    }
}
