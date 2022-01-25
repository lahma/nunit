// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
    /// <summary>
    /// ClassName filter selects tests based on the class FullName
    /// </summary>
    internal sealed class NamespaceFilter : ValueMatchFilter
    {
        /// <summary>
        /// Construct a NamespaceFilter for a single namespace
        /// </summary>
        /// <param name="expectedValue">The namespace the filter will recognize.</param>
        public NamespaceFilter(string expectedValue) : base(expectedValue) { }

        /// <summary>
        /// Match a test against a single value.
        /// </summary>
        public override bool Match(ITest test)
        {
            string containingNamespace = null;

            if (test.TypeInfo != null)
            {
                containingNamespace = test.TypeInfo.Namespace;
            }

            return Match(containingNamespace);
        }

        /// <summary>
        /// Gets the element name
        /// </summary>
        /// <value>Element name</value>
        protected override string ElementName
        {
            get { return "namespace"; }
        }
    }
}
