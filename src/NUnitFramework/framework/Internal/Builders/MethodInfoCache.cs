// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

using System.Collections.Concurrent;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
    /// <summary>
    ///
    /// </summary>
    internal static class MethodInfoCache
    {
        // we would otherwise do a lot of attribute allocations when repeatedly checking the same method for example
        // in case of building TestCaseAttribute-based tests
        private static readonly ConcurrentDictionary<IMethodInfo, TestMethodMetadata> _methodMetadataCache = new ConcurrentDictionary<IMethodInfo, TestMethodMetadata>();

        /// <summary>
        /// Returns cached metadata for method instance.
        /// </summary>
        internal static TestMethodMetadata Get(IMethodInfo method)
        {
            return _methodMetadataCache.GetOrAdd(method, m => new TestMethodMetadata(m));
        }

        /// <summary>
        /// Memoization of TestMethod information to reduce subsequent allocations from parameter and attribute information.
        /// </summary>
        internal sealed class TestMethodMetadata
        {
            public TestMethodMetadata(IMethodInfo method)
            {
                Parameters = method.GetParameters();
                IsAsyncOperation = AsyncToSyncAdapter.IsAsyncOperation(method.MethodInfo);
                IsVoidOrUnit = Reflect.IsVoidOrUnit(method.ReturnType.Type);

                RepeatTestAttributes = method.GetCustomAttributes<IRepeatTest>(true);
                TestBuilderAttributes = method.GetCustomAttributes<ITestBuilder>(false);
            }

            public IParameterInfo[] Parameters { get; }
            public IRepeatTest[] RepeatTestAttributes { get; }
            public bool IsAsyncOperation { get; }
            public bool IsVoidOrUnit { get; }
            public ITestBuilder[] TestBuilderAttributes { get; }
        }
    }
}
