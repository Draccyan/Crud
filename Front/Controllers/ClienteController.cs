using Crud.Services.Commands.GetAll;
using Crud.Services.Models;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Front.Controllers
{
    public class ClienteController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClienteController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7278/api");
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Crud/getAll");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<GetAllCommandResponse>(content);

                var clientesVM = data.Clientes.Select(c => new ClienteVM
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    FechaDeNacimiento = c.FechaDeNacimiento.GetValueOrDefault(),
                    Cuit = c.Cuit,
                    Domicilio = c.Domicilio,
                    Celular = c.Celular,
                    Email = c.Email
                }).ToList();

                return View("Index", clientesVM);
            }

            return View(new List<ClienteVM>());
            
        }
    }
}
