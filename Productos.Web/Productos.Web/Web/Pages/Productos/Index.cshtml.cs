using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private IConfiguracion _configuracion;
        public IList<ProductoResponse> productos { get; set; } = default!;
        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProductos");

            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endpoint);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();

                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                productos = JsonSerializer.Deserialize<List<ProductoResponse>>(resultado, opciones);
            }
            else if (respuesta.StatusCode == HttpStatusCode.NoContent)
            {
                productos = new List<ProductoResponse>();
            }
        }
    }
}
