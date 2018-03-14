using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.UnitOfWork {

    public sealed class UnitOfWorkOptions {
        private static readonly UnitOfWorkOptions s_defaultOptions = new UnitOfWorkOptions(TimeSpan.MaxValue, true);

        public static UnitOfWorkOptions DefaultOptions => s_defaultOptions;

        public TimeSpan Timeout { get; }

        public bool IsTransactional { get; }

        public UnitOfWorkOptions(TimeSpan timeout, bool isTransactional) {
            this.Timeout = timeout;
            this.IsTransactional = isTransactional;
        }

    }
}
