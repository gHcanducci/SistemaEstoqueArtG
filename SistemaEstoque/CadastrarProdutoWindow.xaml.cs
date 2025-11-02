using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
using Repositório.Classes;
using System;
using System.Linq;
using System.Windows;

namespace SistemaEstoque
{
    public partial class CadastrarProdutoWindow : Window
    {
        private readonly ProjetoContext _contexto;
        private readonly ProdutoRepositorio _produtoRepositorio;

        public CadastrarProdutoWindow()
        {
            InitializeComponent();
            _contexto = new ProjetoContext();
            _produtoRepositorio = new ProdutoRepositorio(_contexto);

            CarregarCombos();
        }

        private void CarregarCombos()
        {
            try
            {
                cbCategoria.ItemsSource = _contexto.Categorias.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("Informe o nome do produto.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(txtValor.Text, out decimal valor) || valor < 0)
                {
                    MessageBox.Show("Valor inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade < 0)
                {
                    MessageBox.Show("Quantidade inválida.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                if (cbCategoria.SelectedValue == null)
                {
                    MessageBox.Show("Selecione uma categoria.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                // Define a reposição automaticamente
                byte reposicao = quantidade < 1 ? (byte)1 : (byte)0;

                var novoProduto = new Produto
                {
                    Nome = txtNome.Text.Trim(),
                    Valor = valor,
                    Quantidade = quantidade,
                    Reposicao = reposicao,
                    CategoriaId = (int)cbCategoria.SelectedValue,
                    Fornecedor = txtFornecedor.Text?.Trim() ?? string.Empty
                };

                _produtoRepositorio.adicionar(novoProduto);
                _contexto.SaveChanges();

                MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar produto:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}