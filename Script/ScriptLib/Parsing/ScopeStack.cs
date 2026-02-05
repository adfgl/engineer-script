namespace ScriptLib.Parsing
{
    public sealed class ScopeStack<T>
    {
        private readonly Stack<Scope> _scopes = new Stack<Scope>();
        private Scope? _root;

        public IReadOnlyCollection<Scope> Scopes => _scopes;

        public Scope Current =>
            _scopes.Count != 0 ? _scopes.Peek() : throw new InvalidOperationException("No scopes exist.");

        public Scope Global =>
            _root ?? throw new InvalidOperationException("No global scope exists.");

        public void Clear()
        {
            while (_scopes.Count != 0)
                _scopes.Pop().Clear();
            _root = null;
        }

        public Scope Open()
        {
            var parent = _scopes.Count == 0 ? null : _scopes.Peek();
            var scope = new Scope(parent);
            _scopes.Push(scope);
            _root ??= scope;
            return scope;
        }

        public void Close()
        {
            if (_scopes.Count == 0)
                throw new InvalidOperationException("No scopes to close.");

            var popped = _scopes.Pop();
            popped.Clear();

            if (_scopes.Count == 0)
                _root = null;
        }

        public IDisposable Guard() => new ScopeGuard(this);

        sealed class ScopeGuard : IDisposable
        {
            readonly ScopeStack<T> _stack;
            bool _disposed;

            public ScopeGuard(ScopeStack<T> stack)
            {
                _stack = stack;
                _stack.Open();
            }

            public void Dispose()
            {
                if (_disposed) return;
                _stack.Close();
                _disposed = true;
            }
        }

        public bool TryResolve(string name, out Scope scope)
        {
            for (Scope? s = Current; s != null; s = s.Parent)
            {
                if (s.Contains(name))
                {
                    scope = s;
                    return true;
                }
            }
            scope = null!;
            return false;
        }

        public sealed class Scope
        {
            readonly Dictionary<string, T> _variables = new Dictionary<string, T>(StringComparer.Ordinal);

            public Scope(Scope? parent = null) => Parent = parent;

            public Scope? Parent { get; }
            public IReadOnlyDictionary<string, T> Variables => _variables;

            public void Clear() => _variables.Clear();

            public bool Shadows(string name)
            {
                return Parent != null && (Parent.Contains(name) || Parent.Shadows(name));
            }

            public bool Contains(string name)
            {
                return _variables.ContainsKey(name);
            }

            public T this[string name]
            {
                get => _variables[name];
                set => _variables[name] = value;
            }

            public T Declare(string name, T value)
            {
                if (Contains(name))
                {
                    throw new InvalidOperationException($"Variable '{name}' is already declared in this scope.");
                }
                _variables[name] = value;
                return value;
            }
        }
    }
}
