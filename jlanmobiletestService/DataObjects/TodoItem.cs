using Microsoft.Azure.Mobile.Server;

namespace jlanmobiletestService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }

        public virtual Author Author { get; set; }
    }
}