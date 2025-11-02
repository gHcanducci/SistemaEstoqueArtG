using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
using Microsoft.EntityFrameworkCore;
using Repositório.Classes;
using Repositório.Interfaces;
using System;
using System.Linq;
using System.Windows;

namespace SistemaEstoque
{
    public partial class EstoqueWindow : Window
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ProjetoContext _contexto;

        public EstoqueWindow()
        {
            InitializeComponent();

            // Instancia o contexto e o repositório corretamente
            _contexto = new ProjetoContext();
            _produtoRepositorio = new ProdutoRepositorio(_contexto);

            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            try
            {
                var produtos = _produtoRepositorio.ObterTodos()
                    .Select(p => new
                    {
                        p.Id,
                        p.Nome,
                        p.Quantidade,
                        p.Valor,
                        Categoria = p.Categoria != null ? new { p.Categoria.Descricao } : null,
                        Fornecedor = string.IsNullOrWhiteSpace(p.Fornecedor) ? "—" : p.Fornecedor,
                        Reposicao = p.Reposicao == 1 ? "Sim" : "Não"
                    })
                    .ToList();

                dgEstoque.ItemsSource = produtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar o estoque:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Pesquisar(object sender, RoutedEventArgs e)
        {
            try
            {
                string termo = txtBuscarProduto.Text.Trim();
                if (string.IsNullOrEmpty(termo))
                {
                    CarregarProdutos();
                    return;
                }

                var resultados = _produtoRepositorio.ObterPorNome(termo)
                    .Select(p => new
                    {
                        p.Id,
                        p.Nome,
                        p.Quantidade,
                        p.Valor,
                        Categoria = p.Categoria != null ? new { p.Categoria.Descricao } : null,
                        Fornecedor = string.IsNullOrWhiteSpace(p.Fornecedor) ? "—" : p.Fornecedor,
                        Reposicao = p.Reposicao == 1 ? "Sim" : "Não"
                    })
                    .ToList();

                dgEstoque.ItemsSource = resultados;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na pesquisa:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NovaEntrada(object sender, RoutedEventArgs e)
        {
            var  janelaRegistrarEntrada = new RegistrarEntradaWindow();
            janelaRegistrarEntrada.ShowDialog();
            CarregarProdutos(); // recarrega a tabela após fechar
        }

        private void RegistrarSaida(object sender, RoutedEventArgs e)
        {
            var janelaRegistrarSaida = new RegistrarSaidaWindow();
            janelaRegistrarSaida.ShowDialog();

            // Recarrega a tabela de produtos após registrar saída
            CarregarProdutos();
        }

        private int InputDialog(string mensagem)
        {
                var inputBox = Microsoft.VisualBasic.Interaction.InputBox(mensagem, "Controle de Estoque", "0");
                return int.TryParse(inputBox, out int valor) ? valor : 0;
        }

        private void CadastrarProduto(object sender, RoutedEventArgs e)
        {
            var janelaCadastro = new CadastrarProdutoWindow();
            janelaCadastro.ShowDialog(); // abre modal
            CarregarProdutos(); // recarrega a tabela após fechar
        }

        private void CadastrarCategoria(object sender, RoutedEventArgs e)
        {
            var janelaCategoria = new CadastrarCategoriaWindow();
            janelaCategoria.ShowDialog();
            CarregarProdutos(); // recarrega a tabela após fechar
        }

        private void ExcluirProduto_Click(object sender, RoutedEventArgs e)
        {
            if (dgEstoque.SelectedItem == null)
            {
                MessageBox.Show("Selecione um produto para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Confirmação
            var result = MessageBox.Show("Tem certeza que deseja excluir este produto? Todos os registros de entrada e saída relacionados também serão excluídos.",
                                         "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                // Obtém o Id do produto selecionado
                dynamic produtoSelecionado = dgEstoque.SelectedItem;
                int produtoId = produtoSelecionado.Id;

                // Recupera o produto do contexto
                var produto = _contexto.Produtos
                                .Include(p => p.Entradas)
                                .Include(p => p.Saidas)
                                .FirstOrDefault(p => p.Id == produtoId);

                if (produto == null)
                {
                    MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Remove todas as entradas e saídas relacionadas
                if (produto.Entradas != null && produto.Entradas.Any())
                    _contexto.EntradaProduto.RemoveRange(produto.Entradas);

                if (produto.Saidas != null && produto.Saidas.Any())
                    _contexto.SaidaProduto.RemoveRange(produto.Saidas);

                // Remove o produto
                _contexto.Produtos.Remove(produto);

                // Salva alterações
                _contexto.SaveChanges();

                MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                // Atualiza DataGrid
                CarregarProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir produto:\n{ex.InnerException?.Message ?? ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
