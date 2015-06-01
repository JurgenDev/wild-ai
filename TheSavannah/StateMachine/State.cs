using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheSavannah
{
    interface State<T>
    {
        void Enter(T entity);
        void Execute(T entity);
        void Exit(T entity);
    }
}
