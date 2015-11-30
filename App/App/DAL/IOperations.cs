using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL
{
    interface IOperations<Type>
    {
        void Add(Type o);
        void Edit(Type o);
        void Remove(int id);
        ICollection<Type> GetAll();
        Type GetSingle(int id);

        bool Exists(Type t);
    }
}
