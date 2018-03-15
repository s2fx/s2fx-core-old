using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace S2fx.Data.EFCore {

    public interface IDbContextOptionsProvider {
        DbContextOptions<S2DbContext> Options { get; }
    }

    public abstract class AbstractDbContextOptionsProvider : IDbContextOptionsProvider {
        private static S2DbContextOptionsBuilder s_optionsBuilder = null;

        public DbContextOptions<S2DbContext> Options => GetOrCreateOptions();

        public AbstractDbContextOptionsProvider() {

        }

        private DbContextOptions<S2DbContext> GetOrCreateOptions() {
            if (s_optionsBuilder == null) {
                s_optionsBuilder = this.CreateOptionsBuilder();
                return s_optionsBuilder.Options;
            }
            else {
                return s_optionsBuilder.Options;
            }
        }

        protected abstract S2DbContextOptionsBuilder CreateOptionsBuilder();
    }
}
