using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios;
using System.Net.Http.Json;

namespace Servicios
{

    public class TipoCambioServicio : ITipoCambioServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguracion _configuracion;

        public TipoCambioServicio(HttpClient httpClient, IConfiguracion configuracion)
        {
            _httpClient = httpClient;
            _configuracion = configuracion;
        }

        public async Task<decimal> ObtenerTipoCambioVenta()
        {
            var urlBase = _configuracion.ObtenerValor("ApiEndPointsTipoCambio:UrlBase");
            var token = _configuracion.ObtenerValor("ApiEndPointsTipoCambio:BearerToken");

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var fecha = DateTime.Now.ToString("yyyy/MM/dd");

            var url = $"{urlBase}?fechaInicio={fecha}&fechaFin={fecha}&idioma=ES";

            var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);

            if (response == null ||
                response.Datos == null ||
                !response.Datos.Any() ||
                !response.Datos[0].Indicadores.Any() ||
                !response.Datos[0].Indicadores[0].Series.Any())
            {
                throw new Exception("No se pudo obtener el tipo de cambio");
            }

            return response.Datos[0]
                           .Indicadores[0]
                           .Series[0]
                           .ValorDatoPorPeriodo;
        }
    }
}