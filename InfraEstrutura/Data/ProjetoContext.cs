using InfraEstrutura.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;


namespace InfraEstrutura.Data
{
    public class ProjetoContext : DbContext
    {
        public ProjetoContext()
        {
        }

        public ProjetoContext(DbContextOptions options) : base(options) { }

        //classes que serão mapeadas com tabelas
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Responsavel> Responsaveis { get; set; }
        public DbSet<EntradaProduto> EntradaProduto { get; set; }
        public DbSet<SaidaProduto> SaidaProduto { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // O nome do banco será EstoquePerfumesDB.db
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SistemaEstoqueArtg;Integrated Security=True;");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Criar o Hash da senha "123"
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes("123");
            var hashedPassword = sha.ComputeHash(asByteArray);

            // Inserir o usuário admin inicial
            modelBuilder.Entity<Responsavel>().HasData(
                new Responsavel
                {
                    Id = 1, // Definimos o ID manualmente para o primeiro usuário
                    Nome = "Administrador",
                    Setor = "Administracao",
                    SenhaHash = Convert.ToBase64String(hashedPassword), // Guardamos o hash como string
                    NivelAcesso = "admin"
                }
            );

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
                entidade.Property(p => p.Reposicao).IsRequired();


                // Relacionamento com Categoria (um para muitos)
                entidade.HasOne(p => p.Categoria)
                        .WithMany()
                        .HasForeignKey(p => p.CategoriaId)
                        .OnDelete(DeleteBehavior.Restrict);

                // Fornecedor

                entidade.HasOne(p => p.Fornecedor)
                        .WithMany(f => f.Produtos)
                        .HasForeignKey(p => p.FornecedorId)
                        .OnDelete(DeleteBehavior.Restrict);

                // EntradaProduto
                entidade.HasOne(p => p.EntradaProduto)
                        .WithMany(e => e.Produtos)
                        .HasForeignKey(p => p.EntradaProdutoId)
                        .OnDelete(DeleteBehavior.Restrict);

                // SaidaProduto
                entidade.HasOne(p => p.SaidaProduto)
                        .WithMany(s => s.Produtos)
                        .HasForeignKey(p => p.SaidaProdutoId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Responsavel>(entidade =>
            {
                entidade.ToTable("Responsáveis");//nome da table
                entidade.HasKey(p => p.Id);//chave primaria
                //qtde maxima de caracteres e preenchimento obrigatorio
                entidade.Property(p => p.Nome).HasMaxLength(150).IsRequired();
                entidade.Property(p => p.Setor).HasMaxLength(150).IsRequired();
                entidade.Property(p => p.NivelAcesso).IsRequired();
                entidade.Property(p => p.SenhaHash).IsRequired();
            });

            modelBuilder.Entity<EntradaProduto>(entidade =>
            {
                entidade.ToTable("EntradasProdutos");
                entidade.HasKey(p => p.Id);
                entidade.Property(p => p.DataEntrada).IsRequired();
                entidade.Property(p => p.Quantidade).IsRequired();

                // Entrada -> Fornecedor
                entidade.HasOne(e => e.Fornecedor)
                        .WithMany(f => f.EntradasProdutos)
                        .HasForeignKey(e => e.FornecedorId)
                        .OnDelete(DeleteBehavior.Restrict);

                // Entrada -> Produto
                entidade.HasOne(e => e.Produto)
                        .WithMany(p => p.Entradas)
                        .HasForeignKey(e => e.ProdutoId)
                        .OnDelete(DeleteBehavior.Restrict);

                // Entrada -> Responsável
                entidade.HasOne(e => e.Responsavel)
                        .WithMany()
                        .HasForeignKey(e => e.ResponsavelId)
                        .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<SaidaProduto>(entidade =>
            {
                entidade.ToTable("SaidasProdutos");
                entidade.HasKey(p => p.Id);
                entidade.Property(p => p.DataSaida).IsRequired();
                entidade.Property(p => p.TipoSaida).HasMaxLength(150).IsRequired();
                entidade.Property(p => p.Quantidade).IsRequired();

                entidade.HasOne(s => s.Produto)
                        .WithMany(p => p.Saidas)
                        .HasForeignKey(s => s.ProdutoId)
                        .OnDelete(DeleteBehavior.Restrict);

                entidade.HasOne(s => s.Responsavel)
                        .WithMany()
                        .HasForeignKey(s => s.ResponsavelId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Fornecedor>(entidade => {
                entidade.ToTable("Fornecedores");//nome da table
                entidade.HasKey(p => p.Id);//chave primaria
                                           //qtde maxima de caracteres e preenchimento obrigatorio
                entidade.Property(p => p.Nome).HasMaxLength(150).IsRequired();
                entidade.Property(p => p.Telefone).HasMaxLength(150).IsRequired();
                entidade.Property(p => p.Email).HasMaxLength(150).IsRequired();
            });

        }

    }
}
