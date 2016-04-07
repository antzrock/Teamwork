using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Weekly.Data.Infrastructure;
using Weekly.Data.Repositiories;
using Weekly.Entities;
using Weekly.Services.Abstract;
using WEEKLY.Web.Infrastructure.Core;

namespace WEEKLY.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin, User")]
    [RoutePrefix("api/productbacklog")]
    public class ProductBacklogController : ApiControllerBase
    {
        private readonly IProductBacklogService _productBacklogService;

        public ProductBacklogController( IProductBacklogService productBacklogService,
                                         IEntityBaseRepository<Error> _errorsRepository, 
                                         IUnitOfWork _unitOfWork) :
            base(_errorsRepository, _unitOfWork)
        {
            _productBacklogService = productBacklogService;
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetProductBacklogForProject(HttpRequestMessage request, int projectId, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ProductBacklog existingProductBacklog = _productBacklogService.GetProductBacklogForProject(projectId, userId);

                if (existingProductBacklog == null)
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Product Backlog not found!");



                return response;
            });
        }
    }
}