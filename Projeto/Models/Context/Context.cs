using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projeto.Models.Authentication;


namespace Projeto.Models
{
    public class Context : IdentityDbContext<UserIdentity, UserRole, string>
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        //Relação nominal das entidades do projeto.
        public DbSet<CursoCategoria> CursoCategorias { get; set; }
        public DbSet<Categorias> categorias { get; set; }
        public DbSet<Cursos> cursos { get; set; }
        public DbSet<AlunoInformacoes> alunos { get; set; }
        public DbSet<InstrutorInformacoes> instrutors { get; set; }
        public DbSet<Matricula> matriculas { get; set; }
        public DbSet<Modulos> modulos { get; set; }
        public DbSet<Aulas> aulas { get; set; }
        public DbSet<Carrinho> carrinhos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configurando relacionamentos
            modelBuilder.Entity<Matricula>()
                .HasKey(ac => new { ac.AlunoId, ac.CursoId });

            modelBuilder.Entity<Matricula>()
                .HasOne(ac => ac.Aluno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(ac => ac.AlunoId).IsRequired(false);

            modelBuilder.Entity<Matricula>()
                .HasOne(ac => ac.Curso)
                .WithMany(c => c.Matriculas)
                .HasForeignKey(ac => ac.CursoId).IsRequired(false);

            modelBuilder.Entity<CursoCategoria>().HasKey(cc => new { cc.CursoId, cc.CategoriaId });

            // Definir as relações entre as entidades
            modelBuilder.Entity<CursoCategoria>()
                .HasOne(cc => cc.Cursos)
                .WithMany(c => c.CursoCategorias)
                .HasForeignKey(cc => cc.CursoId).IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CursoCategoria>()
                .HasOne(cc => cc.Categorias)
                .WithMany(c => c.CursoCategorias)
                .HasForeignKey(cc => cc.CategoriaId).IsRequired(false);

            modelBuilder.Entity<Cursos>()
                .HasOne(i => i.Instrutor)
                .WithMany(c => c.Cursos)
                .HasForeignKey(f => f.InstrutorId).IsRequired(false);

            modelBuilder.Entity<Cursos>()
                .HasMany(c => c.Modulos)
                .WithOne(m => m.Curso)
                .HasForeignKey(m => m.CursoId).IsRequired(false);

            modelBuilder.Entity<Modulos>()
                .HasMany(m => m.Aulas)
                .WithOne(a => a.Modulo)
                .HasForeignKey(a => a.Moduloid).IsRequired(false);
            modelBuilder.Entity<AlunoInformacoes>()
                   .HasMany(a => a.Carrinhos)
                   .WithOne(c => c.Aluno)
                   .HasForeignKey(c => c.AlunoId)
                   .IsRequired();

            modelBuilder.Entity<Cursos>()
                .HasMany(c => c.Carrinhos)
                .WithOne(c => c.Curso)
                .HasForeignKey(c => c.CursoId)
                .IsRequired();

            modelBuilder.Entity<Carrinho>()
                .HasKey(c => new { c.AlunoId, c.CursoId });
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Name = "Aluno", NormalizedName = "ALUNO" },
                new UserRole { Name = "Instrutor", NormalizedName = "INSTRUTOR" }
            );


        }
    }

}

