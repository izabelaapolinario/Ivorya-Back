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
    public async Task<IActionResult> InserirContato(Contato Contato)
    {
        _context.Contatos.Add(Contato);
        await _context.SaveChangesAsync();
        return Ok(Contato);
    }

    [HttpDelete("deletar/{id}")]
    public async Task<IActionResult> DeletarContato(int id)
    {
        var ContatoExistente = await _context.Contatos.FirstOrDefaultAsync(a => a.IdContato == id);
        if (ContatoExistente == null)
            return NotFound("Contato não encontrado.");

        _context.Contatos.Remove(ContatoExistente);
        await _context.SaveChangesAsync();
        return Ok("Contato deletado com sucesso.");
    }

}
