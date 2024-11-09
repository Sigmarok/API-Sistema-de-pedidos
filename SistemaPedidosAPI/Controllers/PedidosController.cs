using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPedidosAPI.Data;
using SistemaPedidosAPI.Models;

namespace SistemaPedidosAPI.Controllers
{
    [Route("v1/pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pedido>> GetPedidos()
        {
            return _context.pedidos.Include(p => p.pedidosprodutos).ThenInclude(pp => pp.produto).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Pedido> GetPedido(int id)
        {
            var pedido = _context.pedidos.Include(p => p.pedidosprodutos).ThenInclude(pp => pp.produto).FirstOrDefault(p => p.id == id);
            if (pedido == null) return NotFound();
            return pedido;
        }

        [HttpPost]
        public ActionResult<Pedido> PostPedido(Pedido pedido)
        {
            _context.pedidos.Add(pedido);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPedido), new { id = pedido.id }, pedido);
        }

        [HttpPut("{id}")]
        public IActionResult PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.id) return BadRequest();
            _context.Entry(pedido).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePedido(int id)
        {
            var pedido = _context.pedidos.Find(id);
            if (pedido == null) return NotFound();
            _context.pedidos.Remove(pedido);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
