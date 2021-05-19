using DevExpress.Xpo;
using System;
using System.Runtime.Serialization;

namespace TodoApi.Db.Models
{
    [Serializable]
    public class TodoXp : XPObject
    {
        public TodoXp(Session session)
            : base(session)
        { }

        public int Id
        {
            get { return _id; }
            set { SetPropertyValue(nameof(Id), ref _id, value); }
        }

        public TodoPriority Priority
        {
            get { return _priority; }
            set { SetPropertyValue(nameof(Priority), ref _priority, value); }
        }

        public string Case
        {
            get { return _case; }
            set { SetPropertyValue(nameof(Case), ref _case, value); }
        }

        private int _id;

        private TodoPriority _priority;

        private string _case;
    }
}
