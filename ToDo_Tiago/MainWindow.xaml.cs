using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDo_Tiago
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ToDoItem> Todos;
        public MainWindow()
        {
            InitializeComponent();
            Todos = new ObservableCollection<ToDoItem>();
            TodoList.ItemsSource = Todos;
        }

        private void AddTodo_Click(object sender, RoutedEventArgs e)
        {
            string newTodoText = TodoInput.Text.Trim();

            if (!string.IsNullOrEmpty(newTodoText))
            {
                ToDoItem newTodo = new ToDoItem(newTodoText);
                Todos.Add(newTodo);
                TodoInput.Clear();
            }
            else
            {
                MessageBox.Show("Bitte geben Sie ein To-Do ein.", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}


