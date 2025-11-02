using InfraEstrutura.Entidades;
using Repositório.Classes;
using System.Text;
using InfraEstrutura.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaEstoque
{
    public partial class MainWindow : Window
    {

        private readonly ProdutoRepositorio _produtoRepositorio;
        public MainWindow()
        {
            InitializeComponent();

            // Cria o contexto do banco e o repositório
            var contexto = new ProjetoContext();
            _produtoRepositorio = new ProdutoRepositorio(contexto);

            // Se quiser, pode carregar o relatório semanal e pré-visualização do estoque aqui
            CarregarRelatorioSemanal();
            CarregarPreviewEstoque();
        }

        // Botão: Gerenciar Responsáveis
        private void AbrirResponsaveis_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResponsaveisWindow janelaResponsaveis = new ResponsaveisWindow();
                janelaResponsaveis.Owner = this; // Define a MainWindow como dona
                janelaResponsaveis.ShowDialog(); // Modal

                CarregarRelatorioSemanal();
                CarregarPreviewEstoque();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao abrir a janela de responsáveis:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AbrirEstoque_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EstoqueWindow janelaEstoque = new EstoqueWindow();
                janelaEstoque.Owner = this; // Define a MainWindow como dona
                janelaEstoque.ShowDialog(); // Modal

                CarregarRelatorioSemanal();
                CarregarPreviewEstoque();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao abrir a janela de estoque:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AbrirGerenciarEntradasSaidas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GerenciarEntradasSaidasWindow janela = new GerenciarEntradasSaidasWindow();
                janela.Owner = this;
                janela.ShowDialog();

                // Atualiza o relatório e preview após o fechamento da janela
                CarregarRelatorioSemanal();
                CarregarPreviewEstoque();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir a janela de Gerenciamento de Entradas/Saídas:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CarregarRelatorioSemanal()
        {
            try
            {
                var produtosReposicao = _produtoRepositorio.ObterProdutosParaReposicao()
                    .Select(p => new
                    {
                        Nome = p.Nome,
                        QuantidadeAtual = p.Quantidade,
                        Fornecedor = p.Fornecedor,
                        Reposicao = p.Reposicao == 1 ? "Sim" : "Não"

                    })
                    .ToList();

                dgRelatorioSemanal.ItemsSource = produtosReposicao;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar relatório: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CarregarPreviewEstoque()
        {
            try
            {
                var produtos = _produtoRepositorio.ObterTodos()
                    .Select(p => new
                    {
                        p.Nome,
                        p.Quantidade
                    })
                    .ToList();

                lvPreviewEstoque.ItemsSource = produtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar pré-visualização: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}