using Microsoft.Azure.Mobile.Server;
using System.Collections.Generic;

namespace jlanmobiletestService.DataObjects
{
    public class Author : EntityData
    {
        public string Name { get; set; }

        public virtual ICollection<TodoItem> TodoItems { get; set; }
    }
}