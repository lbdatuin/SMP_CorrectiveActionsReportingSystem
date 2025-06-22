using Azure;
using CARWeb.Data;
using CARWeb.Shared.DTOs.CARLabelDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Models.CARLabel;
using CARWeb.Shared.Models.DepartmentSection;
using CARWeb.Shared.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace CARWeb.Services.CARLabelService
{
    public class CARLabelService : ICARLabelService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CARLabelService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetUserId() => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<int> CreateStandard(CreateStandardDTO request)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) return -1;

                Standard payload = new Standard
                {
                    Code = request.Code,
                    Desciption = request.Desciption,
                    CreatedBy = userId,
                    DateCreated = DateTime.Now,
                };
                _context.Standards.Add(payload);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> UpdateStandard(int Id, CreateStandardDTO request)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) return -1;

                Standard? query = await _context.Standards
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Code = request.Code;
                query.Desciption = request.Desciption;
                query.ModifiedBy = userId;
                query.DateModified = DateTime.Now;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<PaginatedTableResponse<GetStandardDTO>> GetPaginatedStandards(GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetStandardDTO> response = new PaginatedTableResponse<GetStandardDTO>();

            try
            {
                IQueryable<Standard> query = _context.Standards
                    .OrderByDescending(q => q.Id);

                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    query = query.Where(q => q.Id.ToString() == request.SearchValue
                    || q.Code.Contains(request.SearchValue));
                }

                List<Standard> db_result = await query
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

                response.Count = await query.CountAsync();
                response.ResponseData = db_result.Select(q => new GetStandardDTO
                {
                    Id = q.Id,
                    Code = q.Code,
                    Desciption = q.Desciption,
                }).ToList();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<int> CreateCARType(CreateCARTypeDTO request)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) return -1;

                CARType payload = new CARType
                {
                    Code = request.Code,
                    Desciption = request.Desciption,
                    CreatedBy = userId,
                    DateCreated = DateTime.Now,
                };
                _context.CARTypes.Add(payload);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> UpdateCARType(int Id, CreateCARTypeDTO request)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) return -1;

                CARType? query = await _context.CARTypes
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Code = request.Code;
                query.Desciption = request.Desciption;
                query.ModifiedBy = userId;
                query.DateModified = DateTime.Now;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<PaginatedTableResponse<GetCARTypeDTO>> GetPaginatedCARTypes(GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetCARTypeDTO> response = new PaginatedTableResponse<GetCARTypeDTO>();

            try
            {
                IQueryable<CARType> query = _context.CARTypes
                    .OrderByDescending(q => q.Id);

                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    query = query.Where(q => q.Id.ToString() == request.SearchValue
                    || q.Code.Contains(request.SearchValue));
                }

                List<CARType> db_result = await query
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

                response.Count = await query.CountAsync();
                response.ResponseData = db_result.Select(q => new GetCARTypeDTO
                {
                    Id = q.Id,
                    Code = q.Code,
                    Desciption = q.Desciption,
                }).ToList();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<List<GetCarTypeListDTO>> GetCarTypeList()
        {
            List<GetCarTypeListDTO> response = new List<GetCarTypeListDTO> ();

            try
            {
                var query = await _context.CARTypes
                    .ToListAsync();

                response = query.Select(q => new GetCarTypeListDTO
                {
                    Id = q.Id,
                    Code = q.Code
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<int> CreateNonConformity(CreateNonConformityDTO request)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) return -1;

                NonConformity payload = new NonConformity
                {
                    Code = request.Code,
                    Desciption = request.Desciption,
                    NoSeries = request.NoSeries,
                    CARTypeId = request.CARTypeId,
                    CreatedBy = userId,
                    DateCreated = DateTime.Now,
                };
                _context.NonConformities.Add(payload);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> UpdateNonConformity(int Id, CreateNonConformityDTO request)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) return -1;

                NonConformity? query = await _context.NonConformities
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Code = request.Code;
                query.Desciption = request.Desciption;
                query.NoSeries = request.NoSeries;
                query.CARTypeId = request.CARTypeId;
                query.ModifiedBy = userId;
                query.DateModified = DateTime.Now;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<PaginatedTableResponse<GetNonConformityDTO>> GetPaginatedNonConformities(GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetNonConformityDTO> response = new PaginatedTableResponse<GetNonConformityDTO>();

            try
            {
                IQueryable<NonConformity> query = _context.NonConformities
                    .Include(q => q.CARType)
                    .OrderByDescending(q => q.Id);

                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    query = query.Where(q => q.Id.ToString() == request.SearchValue
                    || q.Code.Contains(request.SearchValue));
                }

                List<NonConformity> db_result = await query
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

                response.Count = await query.CountAsync();
                response.ResponseData = db_result.Select(q => new GetNonConformityDTO
                {
                    Id = q.Id,
                    Code = q.Code,
                    Desciption = q.Desciption,
                    NoSeries = q.NoSeries,
                    CARTypeId = q.CARTypeId,
                    CARTypeCode = q.CARType.Code
                }).ToList();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<List<GetStandardListDTO>> GetStandardList()
        {
            List<GetStandardListDTO> response = new List<GetStandardListDTO>();

            try
            {
                var query = await _context.Standards
                    .ToListAsync();

                response = query.Select(q => new GetStandardListDTO
                {
                    Id = q.Id,
                    Code = q.Code
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<List<GetNonConformityListDTO>> GetNonConformityList()
        {
            List<GetNonConformityListDTO> response = new List<GetNonConformityListDTO>();

            try
            {
                var query = await _context.NonConformities
                    .ToListAsync();

                response = query.Select(q => new GetNonConformityListDTO
                {
                    Id = q.Id,
                    Code = q.Code
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
