using CARWeb.Data;
using CARWeb.Shared.DTOs.CARLabelDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Models.CARLabel;
using CARWeb.Shared.Models.DepartmentSection;
using CARWeb.Shared.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CARWeb.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartmentService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetUserId() => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<int> CreateDepartment(CreateDepartmentDTO request)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) return -1;

                Department payload = new Department
                {
                    Code = request.Code,
                    Name = request.Name,
                    CreatedBy = userId,
                    DateCreated = DateTime.Now,
                };

                foreach (var item in request.Sections)
                {
                    DSection section_item = new DSection
                    {
                        Department = payload,
                        DepartmentId = payload.Id,
                        Name = item.Name,
                        CreatedBy = userId,
                        DateCreated = DateTime.Now,
                    };
                    _context.DSections.Add(section_item);
                }

                _context.Departments.Add(payload);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
      
        public async Task<int> UpdateDepartment(int Id, CreateDepartmentDTO request)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) return -1;

                Department? query = await _context.Departments
                    .Include(q => q.Sections)
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Code = request.Code;
                query.Name = request.Name;
                query.ModifiedBy = userId;
                query.DateModified = DateTime.Now;

                List<DSection>? existingSections = query.Sections;

                foreach (var item in request.Sections)
                {
                    var existingSection = existingSections.FirstOrDefault(q => q.Name == item.Name);

                    if (existingSection == null)
                    {
                        DSection section_item = new DSection
                        {
                            DepartmentId = query.Id,
                            Name = item.Name,
                            CreatedBy = userId,
                            DateCreated = DateTime.Now,
                        };
                        _context.DSections.Add(section_item);
                    }
                }


                List<DSection> removedSections = query.Sections.Where(sec => !request.Sections.Any(q => q.Id == sec.Id)).ToList();

                foreach (var removedSection in removedSections)
                {
                    query.Sections.Remove(removedSection);
                    _context.DSections.Remove(removedSection);
                }

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<PaginatedTableResponse<GetDepartmentDTO>> GetPaginatedDepartments(GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetDepartmentDTO> response = new PaginatedTableResponse<GetDepartmentDTO>();

            try
            {
                IQueryable<Department> query = _context.Departments
                    .Include(q => q.Sections)
                    .OrderByDescending(q => q.Id);

                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    query = query.Where(q => q.Id.ToString() == request.SearchValue
                    || q.Code.Contains(request.SearchValue));
                }

                List<Department> db_result = await query
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

                response.Count = await query.CountAsync();
                response.ResponseData = db_result.Select(q => new GetDepartmentDTO
                {
                    Id = q.Id,
                    Code = q.Code,
                    Name = q.Name,
                    Sections = q.Sections?.Select(sec => new GetSectionDTO
                    {
                        Id = sec.Id,
                        Name = sec.Name,
                    }).ToList() ?? new List<GetSectionDTO>(),
                }).ToList();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

    }
}
