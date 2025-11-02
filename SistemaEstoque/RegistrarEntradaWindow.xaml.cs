using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
using System;
using System.Linq;
using System.Windows;

namespace SistemaEstoque
{
    public partial class RegistrarEntradaWindow : Window
    {
        private readonly ProjetoContext _contexto;

        public RegistrarEntradaWindow()
        {
            InitializeComponent();
            _contexto = new ProjetoContext();
            CarregarDados();
        }

        private void CarregarDados()
        {
            cmbProduto.ItemsSource = _contexto.Produtos.ToList();
            cmbProduto.DisplayMemberPath = "Nome";
            cmbProduto.SelectedValuePath = "Id";

        }

        private void RegistrarEntrada_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbProduto.SelectedValue == null || string.IsNullOrWhiteSpace(txtFornecedor.Text))
                {
                    MessageBox.Show("Selecione um produto e informe o fornecedor.");
                    return;
                }

                if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade <= 0)
                {
                    MessageBox.Show("Informe uma quantidade válida.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int produtoId = (int)cmbProduto.SelectedValue;
                string fornecedorNome = txtFornecedor.Text.Trim();

                var produto = _contexto.Produtos.Find(produtoId);
                if (produto == null)
                {
                    MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                produto.Quantidade += quantidade;

                var entrada = new EntradaProduto
                {
                    ProdutoId = produtoId,
                    Fornecedor = fornecedorNome, // novo campo texto
                    Quantidade = quantidade,
                    DataEntrada = DateTime.Now,
                    ResponsavelId = 1
                };

                _contexto.EntradaProduto.Add(entrada);
                _contexto.SaveChanges();

                MessageBox.Show("Entrada registrada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar entrada:\n{ex.InnerException?.Message ?? ex.Message}",
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}