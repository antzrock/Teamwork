using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Data.Infrastructure;
using Weekly.Data.Repositiories;
using Weekly.Entities;
using Weekly.Services.Abstract;

namespace Weekly.Services
{
    public class ProductBacklogService : IProductBacklogService
    {
        private readonly IEntityBaseRepository<ProductBacklog> _productBacklogRepository;
        private readonly IProjectService _projectService;
        private readonly IMembershipService _membershipService;
        private readonly IUnitOfWork _unitOfWork;

        public ProductBacklogService(IEntityBaseRepository<ProductBacklog> productBacklogRepository,
                                     IProjectService projectService,
                                     IMembershipService membershipService,
                                     IUnitOfWork unitOfWork)
        {
            _productBacklogRepository = productBacklogRepository;
            _projectService = projectService;
            _membershipService = membershipService;
            _unitOfWork = unitOfWork;
        }

        public ProductBacklog GetProductBacklogForProject(int projectId, int userId)
        {
            ProductBacklog existingProductBacklog = _productBacklogRepository.AllIncluding(pb => pb.Project).Where(pb => pb.Project.ID == projectId).FirstOrDefault();

            if (existingProductBacklog == null)
            {
                // Create product backlog....
                existingProductBacklog = InitializeProductBacklogForProject(projectId, userId);
                
            }

            return existingProductBacklog;
        }

        public ProductBacklog InitializeProductBacklogForProject(int projectId, int userId)
        {
            Project existingProject = _projectService.GetProjectById(projectId);

            if (existingProject == null)
            {
                throw new Exception("Project not found in database during creation of Product Backlog");
            }

            User existingUser = _membershipService.GetUser(userId);

            if (existingUser == null)
                throw new Exception("User not found in database during creation of Product Backlog");

            ProductBacklog newProductBacklog = new ProductBacklog();

            newProductBacklog.ProjectID = existingProject.ID;
            newProductBacklog.Project = existingProject;
            newProductBacklog.CreatedBy = existingUser.ID;
            newProductBacklog.CreatedDate = DateTime.Now;

            _productBacklogRepository.Add(newProductBacklog);
            _unitOfWork.Commit();

            ProductBacklog existingProductBacklog = _productBacklogRepository.AllIncluding(pb => pb.Project).Where(pb => pb.Project.ID == projectId).FirstOrDefault();

            if (existingProductBacklog == null)
                throw new Exception("Product Backlog not initialized");


            return existingProductBacklog;
        }
    }
}
