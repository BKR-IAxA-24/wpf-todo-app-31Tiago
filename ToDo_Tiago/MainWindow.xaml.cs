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
using System.IO;
using System.Text.Json;

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
            LoadTodosFromFile();
            TodoList.ItemsSource = Todos;

        }
        private void RemoveTodo_Click(object sender, RoutedEventArgs e)
        {
            if (TodoList.SelectedItem != null)
            {
                ToDoItem selectedTodo = (ToDoItem)TodoList.SelectedItem;
                Todos.Remove(selectedTodo);
                SaveTodosToFile();
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie ein To-Do zum Entfernen aus.", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddTodo_Click(object sender, RoutedEventArgs e)
        {
            string newTodoText = TodoInput.Text.Trim();

            if (!string.IsNullOrEmpty(newTodoText))
            {
                ToDoItem newTodo = new ToDoItem(newTodoText);
                Todos.Add(newTodo);
                TodoInput.Clear();
                SaveTodosToFile();
            }
            else
            {
                MessageBox.Show("Bitte geben Sie ein To-Do ein.", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SaveTodosToFile()
        {
            string filePath = "todos.json"; 
            string json = JsonSerializer.Serialize(Todos);
            File.WriteAllText(filePath, json);
        }

        private void LoadTodosFromFile()
        {
            string filePath = "todos.json";

            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);

                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        var loadedTodos = JsonSerializer.Deserialize<ObservableCollection<ToDoItem>>(json);

                        if (loadedTodos != null)
                        {
                            Todos = loadedTodos;
                        }
                        else
                        {
                            Todos = new ObservableCollection<ToDoItem>();
                        }
                    }
                    else
                    {
                        Todos = new ObservableCollection<ToDoItem>();
                    }
                }
                catch (JsonException ex)
                {
                    // Fehler bei der Deserialisierung behandeln
                    MessageBox.Show($"Fehler beim Laden der Daten: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    Todos = new ObservableCollection<ToDoItem>();
                }
            }
            else
            {
                Todos = new ObservableCollection<ToDoItem>(); 
            }

            TodoList.ItemsSource = Todos; 
        }


    }
}


