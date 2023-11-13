using Microsoft.EntityFrameworkCore;

namespace Projeto.Models
{
  public class Context : DbContext
  {
    public Context(DbContextOptions<Context> options) : base(options) { }
    public DbSet<CursoCategoria> CursoCategorias { get; set; }
    public DbSet<Categorias> categorias { get; set; }
    public DbSet<Cursos> cursos { get; set; }
    public DbSet<AlunoInformacoes> alunos { get; set; }
    public DbSet<InstrutorInformacoes> instrutors { get; set; }
    public DbSet<Matricula> matriculas { get; set; }
    public DbSet<Aulas> aulas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Matricula>()
          .HasKey(ac => new { ac.AlunoId, ac.CursoId });

      modelBuilder.Entity<Matricula>()
          .HasOne(ac => ac.Aluno)
          .WithMany(a => a.Matriculas)
          .HasForeignKey(ac => ac.AlunoId);

      modelBuilder.Entity<Matricula>()
          .HasOne(ac => ac.Curso)
          .WithMany(c => c.Matriculas)
          .HasForeignKey(ac => ac.CursoId);
      modelBuilder.Entity<CursoCategoria>().HasKey(cc => new { cc.CursoId, cc.CategoriaId });

      // Definir as relações entre as entidades
      modelBuilder.Entity<CursoCategoria>()
          .HasOne(cc => cc.Cursos)
          .WithMany(c => c.CursoCategorias)
          .HasForeignKey(cc => cc.CursoId);

      modelBuilder.Entity<CursoCategoria>()
          .HasOne(cc => cc.Categorias)
          .WithMany(c => c.CursoCategorias)
          .HasForeignKey(cc => cc.CategoriaId);
    }

  }
}
