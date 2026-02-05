namespace ScriptLib.Parsing
{
    public class ScopeGuard<T> : IDisposable
    {
        readonly ScopeStack<T> stack;

        public ScopeGuard(ScopeStack<T> stack)
        {
            this.stack = stack;
            stack.Open();
        }

        public void Dispose()
        {
            stack.Close();
        }
    }
}
