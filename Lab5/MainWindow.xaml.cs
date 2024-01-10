
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.ComponentModel;
using System.Windows;
using Lab5;

namespace Lab5
{
    public partial class MainWindow : Window
    {
        private readonly TaskManager taskManager = new TaskManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var task = CreateTask();
            taskManager.AddTask(task);
        }

        private void ShowAllTasks_Click(object sender, RoutedEventArgs e)
        {
            var allTasks = taskManager.ShowAllTasks();

            if (allTasks.Any())
            {
                MessageBox.Show($"Все задачи:\n{string.Join("\n", allTasks.Select(t => $"{t.Name} - Приоритет: {t.Priority}, Дедлайн: {t.Deadline}"))}");
            }
            else
            {
                MessageBox.Show("Нет задач.");
            }
        }

        private void FindTaskByPriority_Click(object sender, RoutedEventArgs e)
        {
            int searchPriority;
            if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Введите приоритет для поиска:"), out searchPriority))
            {
                var foundTasks = taskManager.FindTasksByPriority(searchPriority);

                if (foundTasks.Any())
                {
                    MessageBox.Show($"Найдены задачи с приоритетом {searchPriority}:\n{string.Join("\n", foundTasks.Select(t => $"{t.Name} - {t.Deadline}"))}");
                }
                else
                {
                    MessageBox.Show($"Задачи с приоритетом {searchPriority} не найдены.");
                }
            }
            else
            {
                MessageBox.Show("Приоритет должен быть числом.");
            }
        }

        private void ShowTopPriorityTask_Click(object sender, RoutedEventArgs e)
        {
            var topPriorityTasks = taskManager.GetTopPriorityTasks();

            if (topPriorityTasks.Any())
            {
                MessageBox.Show($"Задачи с наивысшим приоритетом:\n{string.Join("\n", topPriorityTasks.Select(t => $"{t.Name} - {t.Deadline}"))}");
            }
            else
            {
                MessageBox.Show("Нет задач с наивысшим приоритетом.");
            }
        }

        private void ShowClosestDeadlineTask_Click(object sender, RoutedEventArgs e)
        {
            var closestDeadlineTasks = taskManager.GetClosestDeadlineTasks();

            if (closestDeadlineTasks.Any())
            {
                MessageBox.Show($"Задачи с ближайшим дедлайном:\n{string.Join("\n", closestDeadlineTasks.Select(t => $"{t.Name} - {t.Deadline}"))}");
            }
            else
            {
                MessageBox.Show("Нет задач с ближайшим дедлайном.");
            }
        }

        private void RemoveTask_Click(object sender, RoutedEventArgs e)
        {
            string taskNameToRemove = Microsoft.VisualBasic.Interaction.InputBox("Введите название задачи для удаления:");
            var taskToRemove = taskManager.AllTasks().FirstOrDefault(task => task.Name == taskNameToRemove);

            if (taskToRemove != null)
            {
                taskManager.RemoveTask(taskToRemove);
            }
            else
            {
                MessageBox.Show("Задача не найдена.");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DisplayTask(MyTask task)
        {
            MessageBox.Show($"{task.Name} - {task.Deadline}");
        }

        private MyTask CreateTask()
        {
            string taskName = Microsoft.VisualBasic.Interaction.InputBox("Введите название задачи:");
            int priority;
            if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Введите приоритет:"), out priority))
            {
                DateTime deadline;
                if (DateTime.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Введите дедлайн (гггг-мм-дд):"), out deadline))
                {
                    return new MyTask
                    {
                        Deadline = deadline,
                        IsDone = false,
                        Priority = priority,
                        Name = taskName
                    };
                }
                else
                {
                    MessageBox.Show("Неверный формат дедлайна.");
                }
            }
            else
            {
                MessageBox.Show("Приоритет должен быть числом.");
            }

            return null;
        }
    }
}
