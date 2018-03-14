using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Environment.Extensions;
using S2fx.Model;

namespace S2fx.Data.EFCore {

    public class S2DbContext : DbContext {
        private readonly IExtensionProvider _extensionProvider;

        public S2DbContext(IDbContextOptionsProvider optionsProvider, IExtensionProvider extensionProvider) : base(optionsProvider.Options) {
            _extensionProvider = extensionProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            var entityTypes = _extensionProvider.GetAllRelationEntityTypes();
            foreach (var type in entityTypes) {
                var entityTypeBuilder = modelBuilder.Entity(type);
                entityTypeBuilder.HasKey(nameof(IEntity.Id));
            }
        }

    }

}
