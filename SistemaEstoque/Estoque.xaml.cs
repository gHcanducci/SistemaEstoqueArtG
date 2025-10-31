using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
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
                var produtos = _produtoRepositorio.ObterTodos();
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

                var resultados = _produtoRepositorio.ObterPorNome(termo);
                dgEstoque.ItemsSource = resultados;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na pesquisa:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NovaEntrada(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgEstoque.SelectedItem is Produto produtoSelecionado)
                {
                    int qtdEntrada = InputDialog("Informe a quantidade de entrada:");
                    if (qtdEntrada <= 0)
                    {
                        MessageBox.Show("Informe uma quantidade válida.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Atualiza a quantidade total do produto
                    produtoSelecionado.Quantidade += qtdEntrada;

                    // Cria registro de entrada
                    var entrada = new EntradaProduto
                    {
                        ProdutoId = produtoSelecionado.Id,
                        Quantidade = qtdEntrada,
                        DataEntrada = DateTime.Now,
                        FornecedorId = 1, // exemplo
                        ResponsavelId = 1 // exemplo
                    };

                    _contexto.EntradaProduto.Add(entrada);
                    _produtoRepositorio.alterar(produtoSelecionado);
                    _contexto.SaveChanges(); // <-- Adicione esta linha

                    MessageBox.Show("Entrada registrada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    CarregarProdutos();
                }
                else
                {
                    MessageBox.Show("Selecione um produto para registrar a entrada.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Erro ao registrar entrada:\n{ex.InnerException?.Message ?? ex.Message}",
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                //MessageBox.Show($"Erro ao registrar entrada:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegistrarSaida(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgEstoque.SelectedItem is Produto produtoSelecionado)
                {
                    int qtdSaida = InputDialog("Informe a quantidade de saída:");
                    if (qtdSaida <= 0)
                    {
                        MessageBox.Show("Informe uma quantidade válida.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (qtdSaida > produtoSelecionado.Quantidade)
                    {
                        MessageBox.Show("Quantidade de saída maior que o estoque atual!", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Atualiza a quantidade total
                    produtoSelecionado.Quantidade -= qtdSaida;

                    // Cria registro de saída
                    var saida = new SaidaProduto
                    {
                        ProdutoId = produtoSelecionado.Id,
                        Quantidade = qtdSaida,
                        DataSaida = DateTime.Now
                    };

                    _contexto.SaidaProduto.Add(saida);
                    _produtoRepositorio.alterar(produtoSelecionado);
                    _contexto.SaveChanges(); // <-- aqui também

                    MessageBox.Show("Saída registrada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    CarregarProdutos();
                }
                else
                {
                    MessageBox.Show("Selecione um produto para registrar a saída.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Erro ao registrar entrada:\n{ex.InnerException?.Message ?? ex.Message}",
                  "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                //                MessageBox.Show($"Erro ao registrar saída:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        }
    }
}