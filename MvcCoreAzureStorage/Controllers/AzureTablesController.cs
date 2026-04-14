using Microsoft.AspNetCore.Mvc;
using MvcCoreAzureStorage.Models;
using MvcCoreAzureStorage.Services;
using System.Threading.Tasks;

namespace MvcCoreAzureStorage.Controllers
{
    public class AzureTablesController : Controller
    {
        private ServiceStorageTables service;

        public AzureTablesController(ServiceStorageTables service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Cliente> clientes =
                await this.service.GetClientesAsync();
            return View(clientes);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string empresa)
        {
            List<Cliente> clientes =
                await this.service.GetClientesEmpresaAsync
                (empresa);
            return View(clientes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            await this.service.CreateClientAsync
                (cliente.IdCliente, cliente.Nombre, cliente.Empresa
                , cliente.Edad, cliente.Salario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete
            (string partitionkey, string rowkey)
        {
            await this.service.DeleteClienteAsync
                (partitionkey, rowkey);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details
            (string partitionkey, string rowkey)
        {
            Cliente cliente = await
                this.service.FindClienteAsync(partitionkey, rowkey);
            return View(cliente);
        }
    }
}
