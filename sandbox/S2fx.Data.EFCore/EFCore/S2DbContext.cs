using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Environment.Extensions;
using S2fx.Model;
using S2fx.Model.Entities;
using S2fx.Data.EFCore.Mapping;

namespace S2fx.Data.EFCore {

    public class S2DbContext : DbContext {
        private readonly IEFCoreModelMapper _modelMapper;

        public S2DbContext(IDbContextOptionsProvider optionsProvider, IEFCoreModelMapper modelMapper) : base(optionsProvider.Options) {
            _modelMapper = modelMapper;

            //TODO FIXME
            this.Database.EnsureCreated();
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            _modelMapper.MapAllEntities(modelBuilder);
        }

    }

}
