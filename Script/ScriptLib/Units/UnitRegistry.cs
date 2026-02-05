using ScriptLib.Units.Catalogs;

namespace ScriptLib.Units
{
    public sealed class UnitRegistry
    {
        readonly Dictionary<string, UnitSpec> _defaults = new(StringComparer.Ordinal);
        readonly Dictionary<string, string> _defaultAliases = new(StringComparer.Ordinal);

        readonly Dictionary<string, UnitSpec> _overrides = new(StringComparer.Ordinal);
        readonly Dictionary<string, string> _overrideAliases = new(StringComparer.Ordinal);

        public static UnitRegistry CreateDefault()
        {
            var r = new UnitRegistry();
            r.LoadDefaults(new SiCatalog(), new UsCatalog(), new ImperialCatalog());
            return r;
        }

        public void LoadDefaults(params UnitCatalogBase[] catalogs)
        {
            _defaults.Clear();
            _defaultAliases.Clear();

            foreach (var c in catalogs)
            {
                // 1) base units
                foreach (var u in c.Units)
                    AddDefault(u);

                // 2) derived prefixed units only for SI catalog
                if (c.Standard == UnitStandard.SI)
                    AddSiPrefixedDerivatives(c.Units, c.Prefixes);
            }

            // Optional: accept "u" as micro alias for existing µ-prefixed symbols
            AddMicroUAliases();
        }

        void AddDefault(in UnitSpec u)
        {
            _defaults[u.Symbol] = u;
            if (u.Aliases is not null)
            {
                foreach (var a in u.Aliases)
                    _defaultAliases[a] = u.Symbol;
            }
        }

        void AddSiPrefixedDerivatives(ReadOnlySpan<UnitSpec> units, ReadOnlySpan<Prefix> prefixes)
        {
            foreach (var baseU in units)
            {
                if (!baseU.Prefixable) continue;
                if (baseU.Behavior == UnitBehavior.Affine) continue; // never prefix affine units

                foreach (var p in prefixes)
                {
                    // Skip collisions (explicit units win)
                    string sym = p.Symbol.Length == 0 ? baseU.Symbol : p.Symbol + baseU.Symbol;
                    if (_defaults.ContainsKey(sym)) continue;

                    var derived = baseU with
                    {
                        Symbol = sym,
                        Name = p.Name.Length == 0 ? baseU.Name : (p.Name + baseU.Name),
                        ScaleToSI = baseU.ScaleToSI * p.Factor,
                        Prefixable = false, // don’t allow stacking prefixes
                        Aliases = null
                    };

                    AddDefault(derived);
                }
            }
        }

        void AddMicroUAliases()
        {
            // Maps "uF" -> "µF" etc if µ-form exists.
            foreach (var sym in _defaults.Keys)
            {
                if (sym.Length >= 2 && sym[0] == 'µ')
                {
                    var alt = "u" + sym.Substring(1);
                    if (!_defaultAliases.ContainsKey(alt))
                        _defaultAliases[alt] = sym;
                }
            }
        }

        public bool TryGet(ReadOnlySpan<char> symbolOrAlias, out UnitSpec spec)
        {
            var key = symbolOrAlias.ToString();

            // overrides first
            if (_overrideAliases.TryGetValue(key, out var ovCanonical))
                key = ovCanonical;
            if (_overrides.TryGetValue(key, out spec))
                return true;

            // defaults
            if (_defaultAliases.TryGetValue(key, out var defCanonical))
                key = defCanonical;
            return _defaults.TryGetValue(key, out spec);
        }

        // ---- Override API ----
        public void Override(UnitSpec spec, bool overwrite = true)
        {
            if (!overwrite && _overrides.ContainsKey(spec.Symbol))
                throw new InvalidOperationException($"Override already exists for '{spec.Symbol}'.");

            _overrides[spec.Symbol] = spec;
            if (spec.Aliases is not null)
            {
                foreach (var a in spec.Aliases)
                    _overrideAliases[a] = spec.Symbol;
            }
        }

        public void OverrideAlias(string alias, string canonicalSymbol)
        {
            if (!_overrides.ContainsKey(canonicalSymbol) && !_defaults.ContainsKey(canonicalSymbol))
                throw new InvalidOperationException($"Canonical symbol '{canonicalSymbol}' is not registered.");
            _overrideAliases[alias] = canonicalSymbol;
        }

        public void RollbackToDefaults()
        {
            _overrides.Clear();
            _overrideAliases.Clear();
        }
    }
}
