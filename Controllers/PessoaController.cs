using System.Reflection.Metadata;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private DataContext context;
        
        public PessoaController(DataContext _context)
        {
            this.context = _context;
        }

        [HttpPost("api")]
        public async Task<ActionResult> create([FromBody] Pessoa p)
        {
            context.pessoa.Add(p);
            await context.SaveChangesAsync();

            return Created("Objeto pessoa", p);
        }

        [HttpGet("api")]
        public async Task<ActionResult> read()
        {
            var dados = await context.pessoa.ToListAsync();
            
            return Ok(dados);
        }

        [HttpGet("api/{id}")]
        public Pessoa readOne(int id)
        {
            Pessoa p = context.pessoa.Find(id);

            return p;
        }

        [HttpPut("api")]
        public async Task<ActionResult> update([FromBody] Pessoa p)
        {
            context.pessoa.Update(p);
            await context.SaveChangesAsync();

            return Ok(p);
        }

        [HttpDelete("api/{id}")]
        public async Task<ActionResult> delete(int id)
        {
            Pessoa p = readOne(id);
            if(p == null)
            {
                return NotFound();
            }

            context.Remove(p);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}