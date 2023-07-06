using FlexHealthDomain.Identity;
using FlexHealthDomain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FlexHealthInfrastructure.Context
{
    public class FlexHealthContext: IdentityDbContext<User, Role, int, 
                                    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public FlexHealthContext(DbContextOptions<FlexHealthContext> options)
            : base(options)
        {

        }

        //public DbSet<Usuario> tfh_usuarios { get; set; }
        public DbSet<Consulta> tfh_consultas { get; set; }
        public DbSet<Exame> tfh_exames { get; set; }
        public DbSet<Medico> tfh_medicos { get; set; }
        public DbSet<Estabelecimento> tfh_estabelecimentos { get; set; }
        public DbSet<Resultado> tfh_resultados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.Acessos)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.Acessos)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
            });

            modelBuilder.Entity<Consulta>(c =>
            {
                c.HasOne(u => u.Usuario)
                        .WithMany()
                        .HasForeignKey(u => u.UsuarioId)
                        .OnDelete(DeleteBehavior.Restrict);
            });
                        
        }
    }
}
