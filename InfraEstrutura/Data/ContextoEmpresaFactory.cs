using InfraEstrutura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ContextoEmpresaFactory : IDesignTimeDbContextFactory<ProjetoContext>
{
    public ProjetoContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProjetoContext>();


        // Defina a string de conexão de forma que o EF possa usar durante o processo de migração.
        optionsBuilder.UseSqlServer(@"Server=localhost;
                DataBase=dbSistemaDeEstoque;
                integrated security=true;TrustServerCertificate=True;");
        return new ProjetoContext(optionsBuilder.Options);
    }
}
