using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class TaskManager
    {
        public readonly List<MyTask> taskList = new List<MyTask>();

        public void AddTask(MyTask task)
        {
            taskList.Add(task);
        }

        public void RemoveTask(MyTask task)
        {
            if (taskList.Contains(task))
            {
                taskList.Remove(task);
                Console.WriteLine($"Задача \"{task.Name}\" успешно удалена.");
            }
            else
            {
                Console.WriteLine("Такая задача не найдена в списке.");
            }
        }

        public IEnumerable<MyTask> GetTopPriorityTasks()
        {
            var highestPriorityTasks = taskList.Where(task => !task.IsDone)
                                                .OrderByDescending(task => task.Priority)
                                                .Take(1)
                                                .ToList();

            return highestPriorityTasks;
        }

        public IEnumerable<MyTask> GetClosestDeadlineTasks()
        {
            var closestDeadlineTasks = taskList.Where(task => !task.IsDone)
                                               .OrderBy(task => task.Deadline)
                                               .Take(1)
                                               .ToList();

            return closestDeadlineTasks;
        }

        public IEnumerable<MyTask> ShowAllTasks()
        {
            return taskList;
        }

        public IEnumerable<MyTask> FindTasksByPriority(int priority)
        {
            return taskList.Where(task => task.Priority == priority).ToList();
        }

        public IEnumerable<MyTask> AllTasks()
        {
            return taskList;
        }
    }
}
