using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPedidosAPI.Data;
using SistemaPedidosAPI.Models;

namespace SistemaPedidosAPI.Controllers
{
    [Route("v1/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            return _context.produtos.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Produto> GetProduto(int id)
        {
            var produto = _context.produtos.Find(id);
            if (produto == null) return NotFound();
            return produto;
        }

        [HttpPost]
        public ActionResult<Produto> PostProduto(Produto produto)
        {
            _context.produtos.Add(produto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProduto), new { id = produto.id }, produto);
        }

        [HttpPut("{id}")]
        public IActionResult PutProduto(int id, Produto produto)
        {
            if (id != produto.id) return BadRequest();
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            var produto = _context.produtos.Find(id);
            if (produto == null) return NotFound();
            _context.produtos.Remove(produto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
