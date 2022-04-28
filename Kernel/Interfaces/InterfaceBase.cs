using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticCraft.Kernel
{
    public class InterfaceBase<T> : AutomaticCraftBase where T : InterfaceBase<T>
    {
        public enum InterfaceConnectionMode
        {
            Input,
            Output,
            Interflow
        }

        protected InterfaceBase(InterfaceConnectionMode mode)
        {
            ConnectionMode = mode;
        }

        public InterfaceConnectionMode ConnectionMode { get; private set; }

        public T? Connection { get; private set; }

        public bool IsConnected { get; private set; }

        public void DisConnect()
        {
            if (!IsConnected)
                return;
            if(Connection != null)
            {
                Connection.Connection = null;
                Connection.IsConnected = false;
            }
            Connection = null;
            IsConnected = false;
        }

        public static bool Connect(T a, T b)
        {
            if(a.IsConnected)
                a.DisConnect();
            if(b.IsConnected)
                b.DisConnect();

            if (a.ConnectionMode == InterfaceConnectionMode.Input && b.ConnectionMode == InterfaceConnectionMode.Output)
            {
                a.Connection = b;
                b.Connection = a;
                a.IsConnected = true;
                b.IsConnected = true;
                return true;
            }
            else if (a.ConnectionMode == InterfaceConnectionMode.Output && b.ConnectionMode == InterfaceConnectionMode.Input)
            {
                a.Connection = b;
                b.Connection = a;
                a.IsConnected = true;
                b.IsConnected = true;
                return true;
            }
            else if (a.ConnectionMode == InterfaceConnectionMode.Interflow || b.ConnectionMode == InterfaceConnectionMode.Interflow)
            {
                a.Connection = b;
                b.Connection = a;
                a.IsConnected = true;
                b.IsConnected = true;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
