using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.EFCore {

    public interface IDbContextOptionsProvider {
        S2DbContextOptions Options { get; }
    }

    public abstract class AbstractDbContextOptionsProvider : IDbContextOptionsProvider {
        private static S2DbContextOptions s_options = null;

        public S2DbContextOptions Options => GetOrCreateOptions();

        public AbstractDbContextOptionsProvider() {

        }

        private S2DbContextOptions GetOrCreateOptions() {
            if (s_options == null) {
                s_options = this.CreateOptions();
                return s_options;
            }
            else {
                return s_options;
            }
        }

        protected abstract S2DbContextOptions CreateOptions();
    }
}
