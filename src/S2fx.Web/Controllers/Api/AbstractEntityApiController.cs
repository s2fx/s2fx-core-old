using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using S2fx.Model;

namespace S2fx.Web.Controllers.Api {

    public abstract class AbstractEntityApiController<TEntity> : AbstractApiController
        where TEntity : IEntity {

        public AbstractEntityApiController() {

        }

        [HttpGet]
        public virtual async Task<TEntity[]> GetMany([FromQuery] string queryString) {
            throw new NotImplementedException();
        }

        [HttpGet]
        public virtual async Task<TEntity> GetSingle(long id, string queryString = null) {
            throw new NotImplementedException();
        }

    }

}
