using LiteDB;
using System.Collections.Generic;
using System.Linq;
using TaskBender.Models;

namespace TaskBender.Services
{
    public class DatabaseService
    {
        private const string DbPath = "tasks.db";

        public void AddTask(string description)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var col = db.GetCollection<TaskItem>("tasks");
                var newTask = new TaskItem
                {
                    Description = description
                };
                col.Insert(newTask);
            }
        }

        public List<TaskItem> GetActiveTasks()
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var col = db.GetCollection<TaskItem>("tasks");
                return col.Find(x => !x.IsCompleted).OrderByDescending(x => x.CreatedAt).ToList();
            }
        }

        public void CompleteTask(int id)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var col = db.GetCollection<TaskItem>("tasks");
                var task = col.FindById(id);
                if (task != null)
                {
                    task.IsCompleted = true;
                    col.Update(task);
                }
            }
        }
    }
}
