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
        public DbSet<Agendamento> tfh_agendamentos { get; set; }
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

            modelBuilder.Entity<Agendamento>(c =>
            {
                c.HasOne(u => u.Usuario)
                        .WithMany()
                        .HasForeignKey(u => u.UsuarioId)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.Restrict);
                c.HasOne(u => u.Medico)
                        .WithMany()
                        .HasForeignKey(u => u.MedicoId)
                        .OnDelete(DeleteBehavior.Restrict);
            });         
        }
    }
}
