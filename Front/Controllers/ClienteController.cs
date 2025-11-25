using Crud.Services.Commands.Get;
using Crud.Services.Commands.GetAll;
using Crud.Services.Commands.Upsert;
using Crud.Services.Models;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

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


        public async Task<IActionResult> Upsert(int id = 0)
        {
            if (id == 0)
            {
                // Nuevo cliente
                return View(new ClienteVM());
            }

            // Editar cliente existente
            var response = await _httpClient.GetAsync($"Crud/get?id={id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "Error al obtener los datos del cliente.";
                return RedirectToAction("Index");
            }

            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<GetCommandResponse>(content);

            var clienteModel = apiResponse?.Cliente;

            var clienteVM = new ClienteVM();
            if (clienteModel != null)
            {
                clienteVM = ToClienteVM(clienteModel);
            }

            return View(clienteVM);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ClienteVM cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            var requestObj = new UpsertCommandRequest
            {
                Id = cliente.Id,
                Data = ToClienteModel(cliente)
            };

            var json = JsonConvert.SerializeObject(requestObj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Crud/upsert", content);

            if (response.IsSuccessStatusCode)
            {
                var respContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UpsertCommandResponse>(respContent);

                TempData["Mensaje"] = result.Result;
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error al guardar el cliente.");
            return View(cliente);
        }



        
        private ClienteModel ToClienteModel(ClienteVM vm)
        {
            if (vm == null) return null;

            return new ClienteModel
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                // Si la API espera DateTime? y tu VM tiene DateTime, castealo:
                FechaDeNacimiento = vm.FechaDeNacimiento,
                Cuit = vm.Cuit,
                Domicilio = vm.Domicilio,
                Celular = vm.Celular,
                Email = vm.Email
            };
        }

        private ClienteVM ToClienteVM(ClienteModel cliente)
        {
            if (cliente == null) return null;

            return new ClienteVM
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                FechaDeNacimiento = cliente.FechaDeNacimiento.GetValueOrDefault(),
                Cuit = cliente.Cuit,
                Domicilio = cliente.Domicilio,
                Celular = cliente.Celular,
                Email = cliente.Email
            };
        }

    }
}
