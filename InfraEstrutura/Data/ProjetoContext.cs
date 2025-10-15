using InfraEstrutura.Entidades;
using Microsoft.EntityFrameworkCore;


namespace InfraEstrutura.Data
{
    public class ProjetoContext : DbContext
    {

        public ProjetoContext(DbContextOptions options) : base(options) { }

        //classes que serão mapeadas com tabelas
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Responsavel> Responsaveis { get; set; }
        public DbSet<EntradaProduto> EntradaDeProdutos { get; set; }
        public DbSet<SaidaProduto> SaidaProdutos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //configurar string de conexao
            optionsBuilder.UseSqlServer(@"Server=localhost;
                DataBase=dbSistemaDeEstoque;
                integrated security=true;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configurações para criação da tabela
            modelBuilder.Entity<Categoria>(entidade => {
                entidade.ToTable("Categorias");//nome da table
                entidade.HasKey(p => p.Id);//chave primaria
                //qtde maxima de caracteres e preenchimento obrigatorio
                entidade.Property(p => p.Descricao).HasMaxLength(150).IsRequired();
                // --------- ATUALIZAR PARA APARECER O NOME DO PRODUTO
            });


            modelBuilder.Entity<Produto>(entidade => {
                entidade.ToTable("Produtos");//nome da table
                entidade.HasKey(p => p.Id);//chave primaria
                entidade.Property(p => p.Nome).HasMaxLength(150).IsRequired();
                entidade.Property(p => p.Quantidade).IsRequired();
                entidade.Property(p => p.Valor).HasPrecision(8, 2).IsRequired();
                entidade.Property(p => p.CategoriaId);// FOREIGN KEY
                entidade.Property(p => p.Reposicao).IsRequired();
                entidade.Property(p => p.EntradaProdutoId);// FOREIGN KEY
                entidade.Property(p => p.SaidaProdutoId);// FOREIGN KEY
                entidade.Property(p => p.FornecedorId);// FOREIGN KEY
            });

            modelBuilder.Entity<Responsavel>(entidade =>
            {
                entidade.ToTable("Responsáveis");//nome da table
                entidade.HasKey(p => p.Id);//chave primaria
                //qtde maxima de caracteres e preenchimento obrigatorio
                entidade.Property(p => p.Nome).HasMaxLength(150).IsRequired();
                entidade.Property(p => p.Setor).HasMaxLength(150).IsRequired();
            });

            modelBuilder.Entity<EntradaProduto>(entidade => {
                entidade.ToTable("EntradasProdutos");//nome da table
                entidade.HasKey(p => p.Id);//chave primaria
                //qtde maxima de caracteres e preenchimento obrigatorio
                entidade.Property(p => p.DataEntrada).IsRequired();
                entidade.Property(p => p.ResponsavelId);// FOREIGN KEY
                entidade.Property(p => p.ProdutoId);// FOREIGN KEY
                entidade.Property(p => p.Quantidade).IsRequired();
                entidade.Property(p => p.FornecedorId);// FOREIGN KEY
            });

            modelBuilder.Entity<SaidaProduto>(entidade => {
                entidade.ToTable("SaidasProdutos");//nome da table
                entidade.HasKey(p => p.Id);//chave primaria
                //qtde maxima de caracteres e preenchimento obrigatorio
                entidade.Property(p => p.DataSaida).IsRequired();
                entidade.Property(p => p.TipoSaida).HasMaxLength(150).IsRequired();
                entidade.Property(p => p.ResponsavelId);// FOREIGN KEY
                entidade.Property(p => p.ProdutoId);// FOREIGN KEY
                entidade.Property(p => p.Quantidade).IsRequired();
                entidade.Property(p => p.FornecedorId);// FOREIGN KEY
            });

            modelBuilder.Entity<Fornecedor>(entidade => {
                entidade.ToTable("Fornecedores");//nome da table
                entidade.HasKey(p => p.Id);//chave primaria
                                           //qtde maxima de caracteres e preenchimento obrigatorio
                entidade.Property(p => p.Nome).HasMaxLength(150).IsRequired();
                entidade.Property(p => p.Telefone).HasMaxLength(150).IsRequired();
                entidade.Property(p => p.Email).HasMaxLength(150).IsRequired();
                entidade.Property(p => p.EntradaProdutoId);// FOREIGN KEY
            });

        }

    }
}
