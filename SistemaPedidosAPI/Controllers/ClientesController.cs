using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPedidosAPI.Data;
using SistemaPedidosAPI.Models;

namespace SistemaPedidosAPI.Controllers
{
    [Route("v1/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> GetClientes()
        {
            return _context.clientes.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> GetCliente(int id)
        {
            var cliente = _context.clientes.Find(id);
            if (cliente == null) return NotFound();
            return cliente;
        }

        [HttpPost]
        public ActionResult<Cliente> PostCliente(Cliente cliente)
        {
            _context.clientes.Add(cliente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.id }, cliente);
        }

        [HttpPut("{id}")]
        public IActionResult PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.id) return BadRequest();
            _context.Entry(cliente).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            var cliente = _context.clientes.Find(id);
            if (cliente == null) return NotFound();
            _context.clientes.Remove(cliente);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
