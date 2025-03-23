using ivorya_back.Data;
using ivorya_back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ivorya_back.Controllers;

[ApiController]
[Route("[controller]")]
public class ContatoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ContatoController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> InserirContato([FromBody] Contato contato)
    {
        if (contato == null)
        {
            return BadRequest("Dados de contato inválidos.");
        }
        try
        {
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();

            return Ok(contato);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro interno do servidor:" + ex);
        }
    }

    [HttpDelete("deletar/{idContato}")]
    public async Task<IActionResult> DeletarContato(int idContato)
    {
        var ContatoExistente = await _context.Contatos.FirstOrDefaultAsync(a => a.IdContato == idContato);
        if (ContatoExistente == null)
            return NotFound("Contato não encontrado.");

        _context.Contatos.Remove(ContatoExistente);
        await _context.SaveChangesAsync();
        return Ok("Contato deletado com sucesso.");
    }
    [HttpGet("obter/{idContato}")]
    public async Task<IActionResult> ObterContato(int idContato)
    {
        var contato = await _context.Contatos.FirstOrDefaultAsync(c => c.IdContato == idContato);
        if (contato == null)
            return NotFound("Contato não encontrado.");

        return Ok(contato);
    }
    [HttpGet("listar")]
    public async Task<IActionResult> ListarContatos()
    {
        var contatos = await _context.Contatos.ToListAsync();
        return Ok(contatos);
    }
}
