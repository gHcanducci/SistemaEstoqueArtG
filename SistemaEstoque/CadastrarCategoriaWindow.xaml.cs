using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
using System;
using System.Linq;
using System.Windows;

namespace SistemaEstoque
{
    public partial class CadastrarCategoriaWindow : Window
    {
        private readonly ProjetoContext _contexto;

        public CadastrarCategoriaWindow()
        {
            InitializeComponent();
            _contexto = new ProjetoContext();
            CarregarCategorias();
        }

        private void CarregarCategorias()
        {
            try
            {
                dgCategorias.ItemsSource = _contexto.Categorias.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar categorias:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string descricao = txtDescricao.Text.Trim();

                if (string.IsNullOrWhiteSpace(descricao))
                {
                    MessageBox.Show("Informe a descrição da categoria.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var novaCategoria = new Categoria
                {
                    Descricao = descricao
                };

                _contexto.Categorias.Add(novaCategoria);
                _contexto.SaveChanges();

                MessageBox.Show("Categoria cadastrada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                txtDescricao.Clear();
                CarregarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar categoria:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}