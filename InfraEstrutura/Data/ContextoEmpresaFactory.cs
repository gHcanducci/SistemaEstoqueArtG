using InfraEstrutura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ContextoEmpresaFactory : IDesignTimeDbContextFactory<ProjetoContext>
{
    public ProjetoContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProjetoContext>();


        // Defina a string de conexão de forma que o EF possa usar durante o processo de migração.
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SistemaEstoqueArtg;Integrated Security=True;");
        return new ProjetoContext(optionsBuilder.Options);
    }
}
