using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class EditarModel : PageModel
    {

        private IConfiguracion _configuracion;

        [BindProperty]
        public ProductoResponse producto { get; set; } = default!;
        [BindProperty]
        public List<SelectListItem> categorias { get; set; } = default!;
        [BindProperty]
        public List<SelectListItem> subCategorias { get; set; } = default!;
        [BindProperty]
        public Guid categoriaSeleccionada { get; set; } = default!;
        [BindProperty]
        public Guid subCategoriaSeleccionada { get; set; } = default!;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }


        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == Guid.Empty)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");
            var cliente = new HttpClient();

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                await ObtenerCategoriasAsync();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);
                if (producto != null)
                {
                    categoriaSeleccionada = Guid.Parse(categorias.Where(m => m.Text == producto.Categoria).FirstOrDefault().Value);
                    subCategorias = (await ObtenerSubCategoriasAsync(categoriaSeleccionada)).Select(a =>
                        new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = a.Nombre.ToString(),
                            Selected = a.Nombre == producto.SubCategoria
                        }).ToList();
                    subCategoriaSeleccionada = Guid.Parse(subCategorias.Where(m => m.Text == producto.SubCategoria).FirstOrDefault().Value);
                }

            }
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (producto.Id == Guid.Empty)
                return NotFound();

            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProducto");
            var cliente = new HttpClient();

            var respuesta = await cliente.PutAsJsonAsync<ProductoRequest>(string.Format(endpoint, producto.Id.ToString()), new ProductoRequest
            {
                IdSubCategoria = subCategoriaSeleccionada,
                Nombre = producto.Nombre,
                CodigoBarras = producto.CodigoBarras,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                Precio = producto.Precio
            });
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }

        private async Task ObtenerCategoriasAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultadoDeserializado = JsonSerializer.Deserialize<List<Categoria>>(resultado, opciones);
                categorias = resultadoDeserializado.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Nombre.ToString()
                                  }).ToList();
            }
        }
        public async Task<JsonResult> OnGetObtenerSubCategorias(Guid categoriaId)
        {
            var subcategorias = await ObtenerSubCategoriasAsync(categoriaId);
            return new JsonResult(subcategorias);
        }

        private async Task<List<SubCategoria>> ObtenerSubCategoriasAsync(Guid categoriaId)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSubCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, categoriaId));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<SubCategoria>>(resultado, opciones);
            }
            return new List<SubCategoria>();
        }
    
    }
}
