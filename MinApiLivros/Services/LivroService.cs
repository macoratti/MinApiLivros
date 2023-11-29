using Microsoft.EntityFrameworkCore;
using MinApiLivros.Context;
using MinApiLivros.Entities;

namespace MinApiLivros.Services;

public class LivroService : ILivroService
{
    private readonly AppDbContext _context;

    public LivroService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Livro> AddLivro(Livro livro)
    {
       if(livro is null)
            throw new ArgumentNullException(nameof(livro));

        _context.Add(livro);
        await _context.SaveChangesAsync();  

        return livro;
    }

    public async Task<Livro> GetLivro(int id)
    {
        var livro = await _context.Livros.
                          FirstOrDefaultAsync(livro => livro.Id == id);

        return livro;
    }

    public async Task<IEnumerable<Livro>> GetLivros()
    {
        var livros = await _context.Livros.ToListAsync();
        return livros;
    }


    public async Task<Livro> DeleteLivro(int id)
    {
        var livro = await _context.Livros.FindAsync(id);

        if (livro is null)
            throw new ArgumentNullException(nameof(livro));

        _context.Remove(livro);
        await _context.SaveChangesAsync();

        return livro;
    }

    public async Task<Livro> UpdateLivro(Livro livro)
    {
        if (livro is null)
            throw new ArgumentNullException(nameof(livro));

        _context.Update(livro);
        await _context.SaveChangesAsync();

        return livro;
    }
}
